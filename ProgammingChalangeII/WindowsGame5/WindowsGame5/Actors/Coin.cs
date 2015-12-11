using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame5.Actors
{
    class Coin : Actor
    {
        private long lifeTime;
        private int value;  //woth
        private System.DateTime endTime;
        
        public int Value { get; set; }
        public Coin()
        {
            type = ActorTypes.Coin;

        }
        public void setLifeTime(int lifeTime)
        {
            endTime = System.DateTime.Now.AddMilliseconds(lifeTime);
            this.lifeTime = lifeTime;
        }

        public System.DateTime EndTime { get; set; }


    }
}
