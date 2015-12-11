using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame5.Actors
{
    class Brick : Actor
    {
        Brick()
        {
            type = ActorTypes.Brick;
            Health = 100000;
        }
    }
}
