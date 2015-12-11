using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame5.Actors
{
    
    class Bullet : Actor
    {
        private int id;
        public Bullet()
        {
        type = ActorTypes.Bullet;
        }
        public int Id { get; set; }
        public const int BULLET_MOVE = 3;

        public void Move()
        {
            float temp;
            switch (Direction)
            {
                case DirectionConstants.Down:
                    temp = Position.X + BULLET_MOVE;
                    if (temp > Map.MAP_HEIGHT)   // hight of the map
                    {
                        die();
                        break;
                    }
                    Position = new Vector2(Position.X, temp);
                    break;
                case DirectionConstants.Up:
                    temp = Position.Y - BULLET_MOVE;
                    if (temp < 0)
                    {
                        die();
                        break;
                    }
                    Position = new Vector2(Position.X, temp);
                    break;
                case DirectionConstants.Left:
                    temp = Position.X - BULLET_MOVE;
                    if (temp < 0)
                    {
                        die();
                        break;
                    }
                    Position = new Vector2(temp, Position.X);
                    break;
                case DirectionConstants.Right:
                    temp = Position.X + BULLET_MOVE;
                    if (temp > Map.MAP_WIDTH)
                    {
                        die();
                        break;
                    }
                    Position = new Vector2(temp, Position.X);
                    break;
            }
        }
    }
}
