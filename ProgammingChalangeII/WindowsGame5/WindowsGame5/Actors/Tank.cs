using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame5.Actors
{
    class Tank :Actor
    {
        public bool shot;
        public int coins;
        public int points;


        public Tank()
        {
            type = ActorTypes.Tank;
        }

        public bool Shot { get; set; }
        
        public bool isShot()
        {
            return shot;
        }
        public int Coins { get; set; }
        public int Points { get; set; }
    }
}
