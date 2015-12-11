using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame5.Actors
{
    class Actor
    {
        private int health;
        private int id;
        private Vector2 position;
        private bool alive;
        public enum ActorTypes {Tank,Brick,Stone,Water,Bullet,Coin,Lifepack,Empty}
        internal ActorTypes type;
        
        public enum DirectionConstants{Up = 0, Right = 1, Down = 2, Left = 3,}
        DirectionConstants direction;

        public Actor()
        {
            alive = true;
            id = -1;
        }
        public DirectionConstants  Direction { get; set; }
        public int Health { get; set; }
        public int Id { get; set; }
        public Vector2 Position { get; set; }
        public bool Alive {
            get
            {
                if (type == ActorTypes.Tank || type == ActorTypes.Brick)
                {
                    if (health <= 0)
                    {
                        return false;
                    }
                }
                else if (type == ActorTypes.Lifepack)
                {
                    LifePack pack = (LifePack)this;

                    if (pack.EndTime.CompareTo(System.DateTime.Now) <= 0)
                    {
                        return false;
                    }
                }
                else if (type == ActorTypes.Coin)
                {
                    Coin coin = (Coin)this;
                    if (coin.EndTime.CompareTo(System.DateTime.Now) <= 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            set
            {
                alive = value;
            }

        }
        public void die()
        {
            this.alive = false;
        }

    }
}
