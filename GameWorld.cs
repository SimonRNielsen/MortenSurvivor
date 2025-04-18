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
        public static Dictionary<Enum, Texture2D[]> Sprites = new Dictionary<Enum, Texture2D[]>();
        public static Dictionary<Sound, SoundEffect> Sounds = new Dictionary<Sound, SoundEffect>();
        public static Dictionary<MusicTrack, Song> Music = new Dictionary<MusicTrack, Song>();


        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {

            LoadSprites();
            LoadSounds();
            LoadMusic();

            base.Initialize();

        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        #region LoadAssets

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

            Texture2D[] fastGoose = new Texture2D[8];
            for (int i = 0; i < fastGoose.Length; i++)
            {
                fastGoose[i] = Content.Load<Texture2D>($"Sprites\\Enemy\\aggro{i}");
            }
            Sprites.Add(EnemyType.Fast, fastGoose);

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
            Sprites.Add(PlayerType.UndercoverMorten, playerWalk);

            #endregion

        }


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


        private void LoadMusic()
        {

            Music.Add(MusicTrack.BattleMusic, Content.Load<Song>("Music\\battleMusic"));
            Music.Add(MusicTrack.BackgroundMusic, Content.Load<Song>("Music\\bgMusic"));

        }

        #endregion

    }
}
