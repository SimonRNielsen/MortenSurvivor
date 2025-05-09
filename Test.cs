using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenSurvivor
{
    public class Test : GameObject, IAnimate
    {
        public Test(Enum type, Vector2 spawnPos) : base(type, spawnPos)
        {

            Sprites = GameWorld.Instance.Sprites[type];

        }

        public float FPS { get; set; } = 6;
        public Texture2D[] Sprites { get; set; }
        public float ElapsedTime { get; set; }
        public int CurrentIndex { get; set; }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            (this as IAnimate).Animate();

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Sprites[CurrentIndex], Position, null, drawColor, Rotation, origin, scale, spriteEffect, layer);

        }

    }
}
