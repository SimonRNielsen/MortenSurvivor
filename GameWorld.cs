using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using MortenSurvivor.Commands;
using MortenSurvivor.Commands.States;
using MortenSurvivor.CreationalPatterns.Factories;
using MortenSurvivor.CreationalPatterns.Pools;
using MortenSurvivor.ObserverPattern;

namespace MortenSurvivor
{
    public class GameWorld : Game
    {

        #region Fields & SingleTon

        private static GameWorld instance;


        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameWorld();

                return instance;
            }
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Dictionary<Enum, Texture2D[]> Sprites = new Dictionary<Enum, Texture2D[]>();
        public Dictionary<Sound, SoundEffect> Sounds = new Dictionary<Sound, SoundEffect>();
        public Dictionary<MusicTrack, Song> Music = new Dictionary<MusicTrack, Song>();
        public SpriteFont GameFont;
        public Vector2 Screensize = new Vector2(1920, 1080);

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> newGameObjects = new List<GameObject>();

        private float deltaTime;

        #endregion
        #region Properties

        /// <summary>
        /// Angiver tid passeret siden sidste update-loop
        /// </summary>
        public float DeltaTime { get => deltaTime; set => deltaTime = value; }

        #endregion
        #region Constructor

        private GameWorld()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        #endregion
        #region Methods

        protected override void Initialize()
        {

            LoadSprites();
            LoadSounds();
            LoadMusic();
            GameFont = Content.Load<SpriteFont>("gameFont");
            SetScreenSize(Screensize);

            gameObjects.Add(new Enemy(EnemyType.Slow, Screensize / 3));
            gameObjects.Add(Player.Instance);

            base.Initialize();

        }


        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameObject gameObject in gameObjects)
                gameObject.Load();

        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
                DoCollisionCheck(gameObject);
            }

            CleanUp();

            base.Update(gameTime);

        }


        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(/*transformMatrix: Camera.GetTransformation(),*/ samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);

        }

        #region LoadAssets

        /// <summary>
        /// Tilføjer alle sprites til Sprites dictionary
        /// </summary>
        private void LoadSprites()
        {

#if DEBUG 
            #region DEBUGITEMS

            Texture2D[] debug = new Texture2D[1];
            debug[0] = Content.Load<Texture2D>("Sprites\\DEBUG\\pixel");
            Sprites.Add(DEBUGItem.DEBUGPixel, debug);

            #endregion
#endif
            #region Enemy

            Texture2D[] walkingGoose = new Texture2D[8];
            for (int i = 0; i < walkingGoose.Length; i++)
            {
                walkingGoose[i] = Content.Load<Texture2D>($"Sprites\\Enemy\\gooseWalk{i}");
            }
            Sprites.Add(EnemyType.Slow, walkingGoose);
            Sprites.Add(EnemyType.SlowChampion, walkingGoose);

            Texture2D[] fastGoose = new Texture2D[8];
            for (int i = 0; i < fastGoose.Length; i++)
            {
                fastGoose[i] = Content.Load<Texture2D>($"Sprites\\Enemy\\aggro{i}");
            }
            Sprites.Add(EnemyType.Fast, fastGoose);
            Sprites.Add(EnemyType.FastChampion, fastGoose);

            Texture2D[] goosifer = new Texture2D[3];
            for (int i = 0; i < goosifer.Length; i++)
            {
                goosifer[i] = Content.Load<Texture2D>($"Sprites\\Enemy\\goosifer{i}");
            }
            Sprites.Add(EnemyType.Goosifer, goosifer);

            #endregion
            #region Environment

            Texture2D[] avSurface = new Texture2D[4];
            for (int i = 0; i < avSurface.Length; i++)
            {
                avSurface[i] = Content.Load<Texture2D>($"Sprites\\Environment\\avsurfaceILD{i + 1}");
            }
            Sprites.Add(EnvironmentTile.AvSurface, avSurface);

            Texture2D[] room = new Texture2D[1];
            room[0] = Content.Load<Texture2D>("Sprites\\Environment\\room_single");
            Sprites.Add(EnvironmentTile.Room, room);

            #endregion
            #region Menu

            Texture2D[] button = new Texture2D[1];
            button[0] = Content.Load<Texture2D>("Sprites\\Menu\\button");
            Sprites.Add(MenuItem.SingleButton, button);

            Texture2D[] stackableButton = new Texture2D[1];
            stackableButton[0] = Content.Load<Texture2D>("Sprites\\Menu\\menuButton");
            Sprites.Add(MenuItem.StackableButton, stackableButton);

            Texture2D[] mouseCursor = new Texture2D[1];
            mouseCursor[0] = Content.Load<Texture2D>("Sprites\\Menu\\sword");
            Sprites.Add(MenuItem.MouseCursor, mouseCursor);

            #endregion
            #region Objects

            Texture2D[] bible = new Texture2D[1];
            bible[0] = Content.Load<Texture2D>("Sprites\\Objects\\bible");
            Sprites.Add(UpgradeType.Bible, bible);

            Texture2D[] mitre = new Texture2D[1];
            mitre[0] = Content.Load<Texture2D>("Sprites\\Objects\\mitre");
            Sprites.Add(UpgradeType.Mitre, mitre);

            Texture2D[] healBoost = new Texture2D[1];
            healBoost[0] = Content.Load<Texture2D>("Sprites\\Objects\\potion");
            Sprites.Add(ItemType.HealBoost, healBoost);

            Texture2D[] rosary = new Texture2D[1];
            rosary[0] = Content.Load<Texture2D>("Sprites\\Objects\\rosary");
            Sprites.Add(UpgradeType.Rosary, rosary);

            Texture2D[] scepter = new Texture2D[1];
            scepter[0] = Content.Load<Texture2D>("Sprites\\Objects\\scepter");
            Sprites.Add(UpgradeType.PopeStaff, scepter);

            #endregion
            #region Player

            Texture2D[] playerWalk = new Texture2D[4];
            for (int i = 0; i < playerWalk.Length; i++)
            {
                playerWalk[i] = Content.Load<Texture2D>($"Sprites\\Player\\underCoverMortenSling{i}");
            }
            Sprites.Add(PlayerType.UndercoverMortenWalk, playerWalk);

            #endregion

        }

        /// <summary>
        /// Tilføjer alle lyde til Sounds dictionary
        /// </summary>
        private void LoadSounds()
        {

            Sounds.Add(Sound.EnemyHonk, Content.Load<SoundEffect>("Sounds\\Enemy\\gooseSound_Short"));

            Sounds.Add(Sound.PowerUpSound, Content.Load<SoundEffect>("Sounds\\Misc\\powerUp_Sound"));

            Sounds.Add(Sound.PlayerTakeDamage, Content.Load<SoundEffect>("Sounds\\Player\\morten_Av"));
            Sounds.Add(Sound.PlayerHeal, Content.Load<SoundEffect>("Sounds\\Player\\playerHeal"));
            Sounds.Add(Sound.PlayerShoot, Content.Load<SoundEffect>("Sounds\\Player\\shootSound"));
            Sounds.Add(Sound.PlayerWalk1, Content.Load<SoundEffect>("Sounds\\Player\\walkSound"));
            Sounds.Add(Sound.PlayerWalk2, Content.Load<SoundEffect>("Sounds\\Player\\walkSound2"));

            Sounds.Add(Sound.ProjectileSmashed, Content.Load<SoundEffect>("Sounds\\Projectile\\eggSmashSound"));
            Sounds.Add(Sound.MagicShoot, Content.Load<SoundEffect>("Sounds\\Projectile\\magicShoot"));

        }

        /// <summary>
        /// Tilføjer alt musik til Music dictionary
        /// </summary>
        private void LoadMusic()
        {

            Music.Add(MusicTrack.BattleMusic, Content.Load<Song>("Music\\battleMusic"));
            Music.Add(MusicTrack.BackgroundMusic, Content.Load<Song>("Music\\bgMusic"));

        }

        #endregion

        /// <summary>
        /// Sætter skærmstørrelsen til at være de angivne dimensioner i vektor'en
        /// </summary>
        /// <param name="screenSize">Angiver skærmstørrelse i form af x- og y-akser</param>
        private void SetScreenSize(Vector2 screenSize)
        {

            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            _graphics.ApplyChanges();

        }

        /// <summary>
        /// Bruges eksternt som "GameWorld.Instance.SpawnObject(obj)" til at tilføje nye aktive objekter, og udskriver til Debugkonsollen hvad der er blevet tilføjet ud fra enum'et der er anvendt i konstruktøren
        /// </summary>
        /// <param name="gameObject"></param>
        public void SpawnObject(GameObject gameObject)
        {

            newGameObjects.Add(gameObject);
            Debug.WriteLine(gameObject.ToString() + " added to spawnlist");

        }

        /// <summary>
        /// Fjerner først objekter fra "gameobjects" hvor "IsAlive" er "false", skriver hvor mange der er det ud til Debug-konsollen, og tilføjer derefter alle objekter i "newGameObjects" efter at have kørt deres "Load" metode, og skriver hvor mange der er tilføjet i Debug-konsollen
        /// </summary>
        private void CleanUp()
        {

            int remove = gameObjects.RemoveAll(x => !x.IsAlive);
            if (remove > 0)
                Debug.WriteLine($"{remove} objects removed from gameObjects");

            if (newGameObjects.Count > 0)
            {

                foreach (GameObject gameObject in newGameObjects)
                    gameObject.Load();

                gameObjects.AddRange(newGameObjects);
                Debug.WriteLine($"{newGameObjects.Count} objects added to gameObjects");
                newGameObjects.Clear();

            }

        }

        /// <summary>
        /// Sørger for at tjekke om det primære objekt har en kollision med øvrige objekter
        /// </summary>
        /// <param name="gameObject">Primære objekt der skal tjekkes op mod</param>
        private void DoCollisionCheck(GameObject gameObject)
        {

            HashSet<(GameObject, GameObject)> collisions = new HashSet<(GameObject, GameObject)>();

            foreach (GameObject other in gameObjects)
            {

                if (gameObject == other || collisions.Contains((gameObject, other)))
                    continue;

                if (((gameObject is Player || gameObject is Projectile) && other is Enemy) || (gameObject is Player && other is Item))
                {
                    if (gameObject.CollisionBox.Intersects(other.CollisionBox))
                    {
                        //Pixelperfect goes here
                        gameObject.OnCollision(other);
                        other.OnCollision(gameObject);
                        collisions.Add((gameObject, other));
                    }
                }

            }

        }

        #endregion

    }
}
