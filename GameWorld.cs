﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MortenSurvivor.Commands;
using MortenSurvivor.CreationalPatterns.Factories;
using MortenSurvivor.CreationalPatterns.Pools;
using MortenSurvivor.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MortenSurvivor
{
    public class GameWorld : Game, ISubject
    {
        #region Singelton

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

        #endregion

        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera camera;
        private Random random;
        private Status status;

        public Dictionary<Enum, Texture2D[]> Sprites = new Dictionary<Enum, Texture2D[]>();
        public Dictionary<Sound, SoundEffect> Sounds = new Dictionary<Sound, SoundEffect>();
        public Dictionary<MusicTrack, Song> Music = new Dictionary<MusicTrack, Song>();
        public SpriteFont GameFont;
        public Vector2 Screensize = new Vector2(1920, 1080);

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> newGameObjects = new List<GameObject>();
        private List<IObserver> listeners = new List<IObserver>();
        public List<Menu> GameMenu = new List<Menu>();

        private float deltaTime;
        private bool gamePaused = true;

        private float lastSpawnEnemy = 2f; //Spawner en gås, når man starter op for spillet 
        private float spawnEnemyTime = 1f;
        private float lastSpawnGoosifer;
        private float spawnGoosiferTime = 10f;

        #endregion
        #region Properties

        /// <summary>
        /// Angiver tid passeret siden sidste update-loop
        /// </summary>
        public float DeltaTime { get => deltaTime; set => deltaTime = value; }


        public bool GamePaused { get => gamePaused; set => gamePaused = value; }


        public Camera Camera { get => camera; }


        public Random Random { get => random; }
        public List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }

        #endregion
        #region Constructor

        private GameWorld()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

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
            camera = new Camera(GraphicsDevice, GameWorld.Instance.Screensize / 2);
            random = new Random();

            gameObjects.Add(Player.Instance);
            InputHandler.Instance.AddUpdateCommand(Keys.A, new MoveCommand(Player.Instance, new Vector2(-1, 0))); //Move left
            InputHandler.Instance.AddUpdateCommand(Keys.D, new MoveCommand(Player.Instance, new Vector2(1, 0))); //Move right
            InputHandler.Instance.AddUpdateCommand(Keys.W, new MoveCommand(Player.Instance, new Vector2(0, -1))); //Move  up
            InputHandler.Instance.AddUpdateCommand(Keys.S, new MoveCommand(Player.Instance, new Vector2(0, 1))); //Move down
            InputHandler.Instance.AddOncePerCountdownCommand(MouseKeys.LeftButton, new ShootCommand(Player.Instance)); //Shoot on mouseclick or hold
            InputHandler.Instance.AddButtonDownCommand(Keys.Escape, new ExitCommand());
            InputHandler.Instance.AddButtonDownCommand(MouseKeys.LeftButton, new SelectCommand());
            InputHandler.Instance.AddButtonDownCommand(Keys.P, new PauseCommand());
            InputHandler.Instance.AddButtonDownCommand(Keys.M, new MuteCommand());
#if DEBUG
            InputHandler.Instance.AddButtonDownCommand(Keys.U, new UpgradeCommand());
#endif

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Music[MusicTrack.BattleMusic]);

            base.Initialize();

        }


        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Menu.CreateMenus();
            status = new Status();

            #region Environment
            //Midt
            GameObjects.Add(new Environment(EnvironmentTile.Center, Screensize / 2));
            GameObjects.Add(new Environment(EnvironmentTile.Left, new Vector2(-Screensize.X / 2, Screensize.Y / 2)));
            GameObjects.Add(new Environment(EnvironmentTile.Right, new Vector2(Screensize.X * 1.5f, Screensize.Y / 2)));

            //Top
            GameObjects.Add(new Environment(EnvironmentTile.TopLeft, -Screensize / 2));
            GameObjects.Add(new Environment(EnvironmentTile.TopRight, new Vector2(Screensize.X * 1.5f, -Screensize.Y / 2)));
            GameObjects.Add(new Environment(EnvironmentTile.Top, new Vector2(Screensize.X / 2, -Screensize.Y / 2)));

            //Bottom
            GameObjects.Add(new Environment(EnvironmentTile.BottomLeft, new Vector2(-Screensize.X / 2, Screensize.Y * 1.5f)));
            GameObjects.Add(new Environment(EnvironmentTile.BottomRight, new Vector2(Screensize.X * 1.5f, Screensize.Y * 1.5f)));
            GameObjects.Add(new Environment(EnvironmentTile.Bottom, new Vector2(Screensize.X / 2, Screensize.Y * 1.5f)));

            //AvSurface
            GameObjects.Add(new Environment(EnvironmentTile.AvSurface, new Vector2(-900, Screensize.Y * 2f - 20)));
            GameObjects.Add(new Environment(EnvironmentTile.AvSurface, new Vector2(-900 + 3586 * 0.6f, Screensize.Y * 2f - 20)));
            GameObjects.Add(new Environment(EnvironmentTile.AvSurface, new Vector2(-900 + 3586 * 2 * 0.6f, Screensize.Y * 2f - 20)));

            //Firepit
            GameObjects.Add(new Environment(EnvironmentTile.Firepit, Vector2.Zero)); //Kan spawne gæs her
            GameObjects.Add(new Environment(EnvironmentTile.Firepit, Screensize * 1.2f));
            GameObjects.Add(new Environment(EnvironmentTile.Firepit, new Vector2(2000, 1700)));
            GameObjects.Add(new Environment(EnvironmentTile.Firepit, new Vector2(400, 1800)));
            GameObjects.Add(new Environment(EnvironmentTile.Firepit, new Vector2(-165, 940)));


            //Hay
            GameObjects.Add(new Environment(EnvironmentTile.Hay, new Vector2(420, 1030)));
            GameObjects.Add(new Environment(EnvironmentTile.Hay, new Vector2(-1230, 1095)));
            GameObjects.Add(new Environment(EnvironmentTile.Hay, new Vector2(1775, -360)));
            GameObjects.Add(new Environment(EnvironmentTile.Hay, new Vector2(575, -390)));
            GameObjects.Add(new Environment(EnvironmentTile.Hay, new Vector2(940, 1775)));


            //Hay stack
            gameObjects.Add(new Environment(EnvironmentTile.HayStack, new Vector2(89, 1335)));
            gameObjects.Add(new Environment(EnvironmentTile.HayStack, new Vector2(-1175, 154)));
            gameObjects.Add(new Environment(EnvironmentTile.HayStack, new Vector2(3000, 625)));
            gameObjects.Add(new Environment(EnvironmentTile.HayStack, new Vector2(-770, -885)));
            gameObjects.Add(new Environment(EnvironmentTile.HayStack, new Vector2(3570, 1075)));


            //Stone
            GameObjects.Add(new Environment(EnvironmentTile.Stone, new Vector2(-710, 1860)));
            GameObjects.Add(new Environment(EnvironmentTile.Stone, new Vector2(825, 540)));
            GameObjects.Add(new Environment(EnvironmentTile.Stone, new Vector2(2940, 109)));
            GameObjects.Add(new Environment(EnvironmentTile.Stone, new Vector2(-1420, -245)));
            GameObjects.Add(new Environment(EnvironmentTile.Stone, new Vector2(1280, 1310)));
            GameObjects.Add(new Environment(EnvironmentTile.Stone, new Vector2(3210, 15355)));
            GameObjects.Add(new Environment(EnvironmentTile.Stone, new Vector2(3535, -215)));


            //Nest
            GameObjects.Add(new Environment(EnvironmentTile.Nest, new Vector2(-50, 160))); 
            GameObjects.Add(new Environment(EnvironmentTile.Nest, new Vector2(Screensize.X * 1.2f, 900)));
            GameObjects.Add(new Environment(EnvironmentTile.Nest, new Vector2(1590, 134))); //Kan spawne gæs her også
            GameObjects.Add(new Environment(EnvironmentTile.Nest, new Vector2(-1455, 1670)));
            GameObjects.Add(new Environment(EnvironmentTile.Nest, new Vector2(2940, 0)));
            GameObjects.Add(new Environment(EnvironmentTile.Nest, new Vector2(3205, 1905)));

            //gameObjects.Add(new Item(ItemType.XPCrystal, Vector2.Zero));
            #endregion

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameObject gameObject in GameObjects)
                gameObject.Load();

        }


        protected override void Update(GameTime gameTime)
        {

            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            status.Update(gameTime);

            InputHandler.Instance.Execute();

            if (!gamePaused)
            {
                foreach (GameObject gameObject in GameObjects)
                {
                    gameObject.Update(gameTime);
                    DoCollisionCheck(gameObject);
                }

                //Spawne nye gæs
                SpawnEnemies();

                CleanUp();

            }
            else
                foreach (Menu item in GameMenu)
                    if (item.IsActive)
                        item.Update();

            base.Update(gameTime);

        }


        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Camera.GetTransformation(), samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

            foreach (GameObject gameObject in GameObjects)
                gameObject.Draw(_spriteBatch);

            status.Draw(_spriteBatch);
            InputHandler.Instance.Draw(_spriteBatch);

            if (gamePaused)
                foreach (Menu item in GameMenu)
                    if (item.IsActive)
                        item.Draw(_spriteBatch);

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

            Texture2D[] debug = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\DEBUG\\pixel") };
            Sprites.Add(DEBUGItem.DEBUGPixel, debug);

            #endregion
#endif
            #region Enemy

            Texture2D[] walkingGoose = new Texture2D[8];
            Texture2D[] fastGoose = new Texture2D[8];
            for (int i = 0; i < 8; i++)
            {
                walkingGoose[i] = Content.Load<Texture2D>($"Sprites\\Enemy\\gooseWalk{i}");
                fastGoose[i] = Content.Load<Texture2D>($"Sprites\\Enemy\\aggro{i}");
            }
            Sprites.Add(EnemyType.Slow, walkingGoose);
            Sprites.Add(EnemyType.SlowChampion, walkingGoose);
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

            Texture2D[] room = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\room_single") };
            Sprites.Add(EnvironmentTile.Room, room);

            Texture2D[] TopLeft = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile1") };
            Sprites.Add(EnvironmentTile.TopLeft, TopLeft);

            Texture2D[] Top = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile2") };
            Sprites.Add(EnvironmentTile.Top, Top);

            Texture2D[] TopRight = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile3") };
            Sprites.Add(EnvironmentTile.TopRight, TopRight);

            Texture2D[] Left = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile4") };
            Sprites.Add(EnvironmentTile.Left, Left);

            Texture2D[] Center = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile5") };
            Sprites.Add(EnvironmentTile.Center, Center);

            Texture2D[] Right = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile6") };
            Sprites.Add(EnvironmentTile.Right, Right);

            Texture2D[] BottomLeft = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile7") };
            Sprites.Add(EnvironmentTile.BottomLeft, BottomLeft);

            Texture2D[] Bottom = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile8") };
            Sprites.Add(EnvironmentTile.Bottom, Bottom);

            Texture2D[] BottomRight = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\tile9") };
            Sprites.Add(EnvironmentTile.BottomRight, BottomRight);

            Texture2D[] firepit = new Texture2D[4];
            for (int i = 0; i < firepit.Length; i++)
            {
                firepit[i] = Content.Load<Texture2D>($"Sprites\\Environment\\firepit{i}");
            }
            Sprites.Add(EnvironmentTile.Firepit, firepit);

            Texture2D[] Stone = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\coal") };
            Sprites.Add(EnvironmentTile.Stone, Stone);

            Texture2D[] HayStack = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\hay stack") };
            Sprites.Add(EnvironmentTile.HayStack, HayStack);

            Texture2D[] Hay = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\hay") };
            Sprites.Add(EnvironmentTile.Hay, Hay);

            Texture2D[] Nest = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Environment\\nest") };
            Sprites.Add(EnvironmentTile.Nest, Nest);

            #endregion
            #region Status

            Texture2D[] barViolet = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\barViolet") };
            Sprites.Add(StatusType.BarViolet, barViolet);

            Texture2D[] barBottom = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\barBottom") };
            Sprites.Add(StatusType.BarBottom, barBottom);

            Texture2D[] healthBottom = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\healthBot") };
            Sprites.Add(StatusType.HealthBottom, healthBottom);

            Texture2D[] healthTop = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\healthTop") };
            Sprites.Add(StatusType.HealthTop, healthTop);
            #endregion
            #region Menu

            Texture2D[] winScreen = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Menu\\winScreen") };
            Sprites.Add(MenuItem.Win, winScreen);

            Texture2D[] loseScreen = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Menu\\loseScreen") };
            Sprites.Add(MenuItem.Loss, loseScreen);

            Texture2D[] introScreen = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Menu\\intro_Screen") };
            Sprites.Add(MenuItem.Start, introScreen);
            Sprites.Add(MenuItem.Pause, introScreen);

            Texture2D[] button = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Menu\\button") };
            Sprites.Add(MenuItem.SingleButton, button);

            Texture2D[] stackableButton = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Menu\\menuButton") };
            Sprites.Add(MenuItem.StackableButton, stackableButton);

            Texture2D[] mouseCursor = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Menu\\sword") };
            Sprites.Add(MenuItem.MouseCursor, mouseCursor);

            Texture2D[] upgradeScreen = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Menu\\upgradeMenu") };
            Sprites.Add(MenuItem.Upgrade, upgradeScreen);

            #endregion
            #region Objects

            Texture2D[] bible = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\bible") };
            Sprites.Add(ItemType.Bible, bible);

            Texture2D[] mitre = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\mitre") };
            Sprites.Add(UpgradeType.Mitre, mitre);

            Texture2D[] potion = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\potion") };
            Sprites.Add(UpgradeType.GeesusBlood, potion);

            Texture2D[] healBoost = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\wallTurkey") };
            //Sprites.Add(UpgradeType.GeesusBlood, healBoost);
            Sprites.Add(ItemType.HealBoost, healBoost);

            Texture2D[] rosary = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\rosary") };
            Sprites.Add(ItemType.Rosary, rosary);

            Texture2D[] scepter = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\scepter") };
            Sprites.Add(UpgradeType.PopeStaff, scepter);

            Texture2D[] egg = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\egg1") };
            Sprites.Add(ProjectileType.Eggs, egg);

            Texture2D[] geasterEgg = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\Objects\\egg2") };
            Sprites.Add(ProjectileType.GeasterEgg, geasterEgg);
            Sprites.Add(UpgradeType.GeasterEgg, geasterEgg);

            Texture2D[] halo = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\objects\\glorie2") };
            Sprites.Add(ProjectileType.Magic, halo);

            Texture2D[] XPcrystal = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\objects\\crystal") };
            Sprites.Add(ItemType.XPCrystal, XPcrystal);

            Texture2D[] speedBoost = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\objects\\boots") };
            Sprites.Add(ItemType.SpeedBoost, speedBoost);

            Texture2D[] deadEnemy = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\enemy\\deadEnemy") };
            Sprites.Add(StatusType.EnemiesKilled, deadEnemy);

            Texture2D[] holyWater = new Texture2D[1] { Content.Load<Texture2D>("Sprites\\objects\\holyWater") };
            Sprites.Add(UpgradeType.HolyWater, holyWater);

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

            int remove = GameObjects.RemoveAll(x => !x.IsAlive);
            if (remove > 0)
                Debug.WriteLine($"{remove} objects removed from gameObjects");

            if (newGameObjects.Count > 0)
            {

                foreach (GameObject gameObject in newGameObjects)
                    gameObject.Load();

                GameObjects.AddRange(newGameObjects);
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

            foreach (GameObject other in GameObjects)
            {

                if (gameObject == other || collisions.Contains((gameObject, other)) || (gameObject is Enemy && other is Enemy))
                    continue;

                if (((gameObject is Player || gameObject is Projectile) && other is Enemy) || (gameObject is Player && other is Item))
                {
                    if (gameObject.CollisionBox.Intersects(other.CollisionBox))
                    {

                        bool handledCollision = false;
                        if (gameObject is Player && other is Enemy)
                            foreach (RectangleData rect1 in ((Character)gameObject).Rectangles)
                            {

                                foreach (RectangleData rect2 in ((Character)other).Rectangles)
                                {
                                    if (rect1.Rectangle.Intersects(rect2.Rectangle))
                                    {
                                        handledCollision = true;
                                        break;
                                    }
                                }

                                if (handledCollision)
                                    break;
                            }
                        else if (gameObject is Projectile && other is Enemy)
                        {
                            foreach (RectangleData rect1 in ((Character)other).Rectangles)
                            {
                                if (rect1.Rectangle.Intersects(gameObject.CollisionBox))
                                {
                                    handledCollision = true;
                                    break;
                                }
                            }
                        }
                        else if (gameObject is Player && other is Item)
                        {
                            foreach (RectangleData rect1 in ((Character)gameObject).Rectangles)
                            {
                                if (rect1.Rectangle.Intersects(other.CollisionBox))
                                {
                                    handledCollision = true;
                                    break;
                                }
                            }
                        }

                        if (handledCollision)
                        {
                            gameObject.OnCollision(other);
                            other.OnCollision(gameObject);
                            collisions.Add((gameObject, other));
                        }

                    }
                }

            }

        }



        /// <summary>
        /// Spawner enemies, hvor Goosifer bliver spawnet i et andet tidsinterval end de andre
        /// </summary>
        private void SpawnEnemies()
        {
            lastSpawnEnemy += GameWorld.Instance.DeltaTime;
            lastSpawnGoosifer += GameWorld.Instance.DeltaTime;

            if (lastSpawnEnemy > spawnEnemyTime)
            {
                //Tilføje en ny enemy til gameObjects
                SpawnObject(EnemyPool.Instance.GetObject());

                //Nulstiller timer
                lastSpawnEnemy = 0f;

                //Debug.WriteLine("Spawner enemy");
            }

            //Spawner Goosifer med sin egen timer
            if (lastSpawnGoosifer > spawnGoosiferTime)
            {
                SpawnObject(EnemyPool.Instance.CreateGoosifer());

                lastSpawnGoosifer = 0f;

                //Debug.WriteLine("Spawn goosifer");
            }
        }

        public void SpawnItem(Vector2 spawnPosition)
        {
            SpawnObject(ItemFactory.Instance.Create(spawnPosition));
        }

        public void Pause()
        {
            if (gamePaused)
            {
                gamePaused = false;
                MediaPlayer.Play(Music[MusicTrack.BattleMusic]);
            }
            else
            {
                gamePaused = true;
                MediaPlayer.Play(Music[MusicTrack.BackgroundMusic]);
            }

        }

        public void Mute()
        {
            if (MediaPlayer.IsMuted)
            {
                SoundEffect.MasterVolume = 1;
                MediaPlayer.IsMuted = false;
            }
            else
            {
                SoundEffect.MasterVolume = 0;
                MediaPlayer.IsMuted = true;
            }
        }

        public void Restart()
        {

            gameObjects.Clear();
            newGameObjects.Clear();
            GameMenu.Clear();
            SpawnObject(Player.Instance);
            ResetObservers();
            LoadContent();
        }


        public void ActivateMenu(MenuItem menu)
        {

            Pause();

            GameMenu.Find(x => x.Type == menu).Activate();

        }

        #region Observer
        public void Attach(IObserver observer)
        {
            listeners.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            listeners.Remove(observer);
        }

        public void Notify(StatusType statusType)
        {
            foreach (IObserver observer in listeners)
            {
                observer.OnNotify(statusType);
            }
        }

        public void ResetObservers()
        {
            listeners.Clear();
        }


        #endregion


        #endregion

    }
}
