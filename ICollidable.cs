using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortenSurvivor
{
    public interface ICollidable
    {


        public Rectangle CollisionBox { get; }


        public bool CheckCollision(ICollidable other)
        {

            if (CollisionBox.Intersects(other.CollisionBox))
                return true;
            else 
                return false;

        }


        public void OnCollision();


    }
}
