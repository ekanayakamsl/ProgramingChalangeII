using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame5.Actors;
namespace WindowsGame5.Map
{
    class Map
    {

        private List<Tank> tanks;
        private List<Brick> bricks;
        private List<LifePack> lifePacks;
        private List<Coin> coinPiles;
        private List<Stone> stones;
        private List<Water> waters;
        private List<Bullet> bullets;
        public const int MAP_WIDTH = 10, MAP_HEIGHT = 10;
        private bool updatedForAI, updatedForView;
        private int clientID;
        int nextActorID;
        public const int NO_OF_PLAYERS = 5;
        bool joined;
        private Vector2 clientPosition;
        WindowsGame5.Actors.Actor.DirectionConstants clientDirection;



        public int ClientID
        {
            get
            {
                return clientID;
            }
            set
            {
                if (value >= 0)
                    clientID = value;
            }
        }
        
        public WindowsGame5.Actors.Actor.DirectionConstants ClientDirection { get; set; }

        public Map()
        {
            tanks = new List<Tank>();
            bricks = new List<Brick>();
            lifePacks = new List<LifePack>();
            coinPiles = new List<Coin>();
            stones = new List<Stone>();
            waters = new List<Water>();
            bullets = new List<Bullet>();
            nextActorID = 20;
            clientID = -1;// starts with an invalid client ID

            setUpdated();
        }

        public void clearTanks()
        {
            tanks.Clear();
        }
        public void clearBricks()
        {
            bricks.Clear();
        }
        public Bullet[] GetBulletArray()
        {
            return bullets.ToArray();
        }

        public Vector2 ClientPosition { get; set; }

        /// <summary>
        /// Automatically move the bullets within the map and change there existence accordingly.
        /// </summary>
        public void proceedBullets()
        {
            foreach (Tank tank in tanks)
            {
                if (tank.isShot())
                {
                    Bullet bullet = new Bullet();
                    bullet.Position = tank.Position;
                    bullet.Direction = tank.Direction;
                    bullet.Alive = true;
                    bullet.Id = nextActorID++;
                    bullets.Add(bullet);
                }
            }
            foreach (Bullet bullet in bullets)
            {
                if (!bullet.Alive)
                {
                    bullets.Remove(bullet);
                }
                else
                {
                    Vector2 firstPosition;
                    Vector2 endPosition;
                    firstPosition = bullet.Position;
                    bullet.Move();
                    endPosition = bullet.Position;
                    
                    if (firstPosition.X == endPosition.X)// moving in Y axis
                    {
                        if (firstPosition.Y > endPosition.Y) // move down
                        {
                            for (int i = (int)endPosition.Y; i < (int)firstPosition.Y; i++)
                            {
                                Actor actor = getActor ((int)firstPosition.X, i);
                                System.Type type = actor.GetType();
                                if (type == typeof(Tank) || type == typeof(Brick) || type == typeof(Stone))
                                {
                                    bullet.die();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int i = (int)endPosition.Y; i > (int)firstPosition.Y; i--)
                            {
                                Actor actor = getActor((int)firstPosition.X, i);
                                System.Type type = actor.GetType();
                                if (type == typeof(Tank) || type == typeof(Brick) || type == typeof(Stone))
                                {
                                    bullet.die();
                                    break;
                                }
                            }
                        }
                    }
                    else if (firstPosition.Y == endPosition.Y)// moving in x axis
                    {
                        if (firstPosition.X > endPosition.X)
                        {
                            for (int i = (int)endPosition.X; i < (int)firstPosition.X; i++)
                            {
                                Actor actor = getActor(i, (int)firstPosition.Y);
                                System.Type type = actor.GetType();
                                if (type == typeof(Tank) || type == typeof(Brick) || type == typeof(Stone))
                                {
                                    bullet.die();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int i = (int)endPosition.X; i > (int)firstPosition.X; i--)
                            {
                                Actor actor = getActor(i, (int)firstPosition.Y);
                                System.Type type = actor.GetType();
                                if (type == typeof(Tank) || type == typeof(Brick) || type == typeof(Stone))
                                {
                                    bullet.die();
                                    break;
                                }
                            }
                        }
                    }

                }
            }
        }

        /// Add an actor to the map
        
        public void setActor(Actor actor)
        {
            if (actor.GetType() == typeof(Tank))
            {
                Tank updattedTank = (Tank)actor;

                foreach (Tank tank in tanks)
                {
                    if (tank.Id == updattedTank.Id)
                    {
                        tank.Coins = updattedTank.coins;
                        tank.Direction = updattedTank.Direction;
                        tank.Health = updattedTank.Health;
                        tank.Position = updattedTank.Position;
                        tank.Points = updattedTank.Points;
                        
                        if (updattedTank.isShot())
                        {
                            tank.isShot();
                        }
                        setUpdated();
                        return;
                    }
                }

                tanks.Add(updattedTank);
                setUpdated();
            }
            else if (actor.GetType() == typeof(Brick))
            {
                Brick update = (Brick)actor;
                foreach (Brick brick in bricks)
                {
                    if (brick.Position.X == update.Position.X && brick.Position.Y == update.Position.Y)
                    {
                        if (brick.Health < 10000)
                        {
                            brick.Health= update.Health;
                        }
                        setUpdated();
                        return;
                    }
                }
                update.Id = nextActorID++;
                update.Health = 100;
                bricks.Add(update);
                setUpdated();
            }
            else if (actor.GetType() == typeof(LifePack))
            {
                actor.Id = nextActorID++;
                lifePacks.Add((LifePack)actor);
            }
            else if (actor.GetType() == typeof(Coin))
            {
                actor.Id = nextActorID++;
                coinPiles.Add((Coin)actor);
            }
            else if (actor.GetType() == typeof(Stone))
            {
                actor.Id = nextActorID++;
                stones.Add((Stone)actor);
            }
            else if (actor.GetType() == typeof(Water))
            {
                actor.Id = nextActorID++;
                waters.Add((Water)actor);
            }
            setUpdated();
        }
        /// <summary>
        /// Gets the actor at given coords
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Actor getActor(int x, int y)
        {
            foreach (Tank actor in tanks)
            {
                if (actor.Position.X == x && actor.Position.Y == y)
                {
                    return actor;
                }
            }
            foreach (Brick actor in bricks)
            {
                if (actor.Position.X == x && actor.Position.Y == y)
                {
                    return actor;
                }
            }
            foreach (Coin actor in coinPiles)
            {
                if (actor.Position.X == x && actor.Position.Y == y)
                {
                    return actor;
                }
            }
            foreach (LifePack actor in lifePacks)
            {
                if (actor.Position.X == x && actor.Position.Y == y)
                {
                    return actor;
                }
            }
            foreach (Stone actor in stones)
            {
                if (actor.Position.X == x && actor.Position.Y == y)
                {
                    return actor;
                }
            }
            foreach (Water actor in waters)
            {
                if (actor.Position.X == x && actor.Position.Y == y)
                {
                    return actor;
                }
            }
            return null;
        }
        public bool isUpdatedForView()
        {
            if (updatedForView)
            {
                updatedForView = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isUpdatedForAI()
        {
            if (updatedForAI)
            {
                updatedForAI = false;
                return updatedForAI;
            }
            else
            {
                return false;
            }
        }

        public void setUpdated()
        {
            updatedForView = true;
            updatedForAI = true;

        }
        private long lastTime = System.DateTime.Now.Ticks / 10000;

        public void setClientID(int clientID)
        {
            this.ClientID = clientID;
        }
        public int getClientID()
        {
            return ClientID;
        }
        public Tank[] getTanks()
        {
            return tanks.ToArray();
        }
        public bool Joined
        {
            set
            {
                joined = value;
            }
            get
            {
                return joined;
            }
        }
        public List<Actor> getActors()
        {
            List<Actor> actors = new List<Actor>();
            actors.AddRange(tanks);
            actors.AddRange(bricks);
            actors.AddRange(bullets);
            actors.AddRange(coinPiles);
            actors.AddRange(lifePacks);
            actors.AddRange(stones);
            actors.AddRange(waters);

            return actors;
        }

        internal Coin[] GetCoins()
        {
            return coinPiles.ToArray();
        }

        internal LifePack[] GetLifePacks()
        {
            return lifePacks.ToArray();
        }

        internal Brick[] GetBricks()
        {
            return bricks.ToArray();
        }

        /// <summary>
        /// Clears the dead actors in the map
        /// </summary>
        /// <returns></returns>
        public int clearDead()
        {
            int count = 0;
            count += tanks.RemoveAll(searchDead);
            count += coinPiles.RemoveAll(searchDead);
            count += lifePacks.RemoveAll(searchDead);
            count += bricks.RemoveAll(searchDead);
            count += bullets.RemoveAll(searchDead);
            count += coinPiles.RemoveAll(searchAquiredDead);
            count += lifePacks.RemoveAll(searchAquiredDead);
            System.Console.WriteLine("Deleted Actors : " + count);
            System.Console.WriteLine("No of Bricks : " + bricks.Count);
            return count;
        }
        /// <summary>
        /// Predicate for search the dead
        /// </summary>
        /// <returns></returns>
        private static bool searchDead(Actor actor)
        {
            //System.Console.WriteLine("Health : " + actor.Health);
            return !actor.Alive;
        }
        /// <summary>
        /// Search for the coinpiles and lifepacks which have been run over by tanks
        /// </summary>
        /// <returns></returns>
        private bool searchAquiredDead(Actor actor)
        {
            if (actor.type == Actor.ActorTypes.Coin || actor.type == Actor.ActorTypes.Lifepack)
            {
                foreach (Tank tank in tanks)
                {
                    if (actor.Position.X == tank.Position.X && actor.Position.Y == tank.Position.Y)
                    {
                        actor.Alive = false;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}