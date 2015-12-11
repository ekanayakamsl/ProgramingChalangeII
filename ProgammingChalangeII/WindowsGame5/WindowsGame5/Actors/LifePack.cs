using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame5.Actors
{
    class LifePack : Actor
    {
        private long lifeTime;
        private int value;
        private System.DateTime endTime;

        LifePack()
        {
            type = ActorTypes.Lifepack;
        }

        public int Volue { get; set; }
        public long LifeTime { get; set; }

        public void setLifeTime(int lifeTime)
        {
            endTime = System.DateTime.Now.AddMilliseconds(lifeTime);
            this.LifeTime = lifeTime;
        }
        public long getLifeTime()
        {
            return LifeTime;
        }
        public long EndTime { get; set; }
    }
}

