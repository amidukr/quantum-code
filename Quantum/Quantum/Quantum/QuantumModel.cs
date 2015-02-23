using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using Point = System.Drawing.Point;

namespace Quantum.Quantum
{

    

    public enum Team { green, blue, neutral }
    public enum DroneOrder { MoveToGeneral, MoveToPosition, MoveToOutpost}

    class QuantumModel
    {
        private int nextUniqueId = 1;

        public readonly int maxRespawnAmount = 100;
        public readonly double milsPerDronRespawn = 60;

        public readonly int cloudRadius = 90;
        public readonly int moveToPositionRadius = 5;

        public readonly double milsOrderFactor = 100000000;
        public readonly double milsPerDronToRecruite = 0.2;

        public readonly double speedConstant = 1.0;
        public readonly double dronSpeedConstant = 1.8;
        public readonly double outpostConquestTime = 300;

        public readonly List<General> Generals = new List<General>();
        public readonly List<Outpost> Outposts = new List<Outpost>();
        public readonly List<Beam> Beams = new List<Beam>();

        public Outpost findOutpostById(int id)
        {
            foreach(Outpost outpost in this.Outposts) {
                if(outpost.id == id) return outpost;
            }
            
            return null;
        }

        public Outpost findOutpostByPosition(Vector position, double cloudRadius)
        {
            foreach (Outpost outpost in this.Outposts)
            {
                if (Vector.Subtract(position, outpost.Position).Length < cloudRadius)
                {
                    return outpost;
                }
            }

            return null;
        }

        public int generateID()
        {
            return nextUniqueId++;
        }

        internal General FindGeneralByTeam(Team team)
        {
            foreach (General general in Generals)
            {
                if (general.Team == team) return general;
            }

            return null;
        }
    }

    class Outpost
    {   
        public Outpost  ()
        {
            this.Team = Team.neutral;
        }

        public int id { get; set; }
        public Vector Position { get; set; }
        public Team Team { get; set; }
        public double respawnTimerAccumulator { get; set; }
    }

    class Drone
    {
        public Drone()
        {
            this.Health = 3; //default value  
            this.id = Guid.NewGuid().ToString();
        }

        public readonly string id;
        public Team Team;
        public int   Health   { get; set; }
        public Vector Position { get; set; }

        public Vector TargetPosition { get; set; }
        public int        TargetOutpost {get; set;}
        public DroneOrder Order{ get; set; }
    }

    class General
    {
        public readonly Team Team;

        public General(Team team)
        {
            this.Team = team;
            Health = 3; //default value   
            Position = new Vector(500, 500);
        }
        
        public Vector Position {get; set;}
        public Vector Velocity {get; set;}

        public int Health { get; set; }
        public readonly List<Drone> Drones = new List<Drone>();

        public Vector PrevSpeed { get; set;}

        public void AddDrone(Drone drone)
        {
            this.Drones.Add(drone);
            drone.Team = this.Team;
        }

        public Drone FindDroneCloseToOutpost(Outpost outpost, double radius) {
            Vector outpostPosition = outpost.Position;

            foreach (Drone drone in Drones)
            {
                if (1.3 * radius > Vector.Subtract(outpostPosition, drone.Position).Length)
                {
                    return drone;
                }
            }

            return null;
        }
    }
    class Beam
    {
        public readonly int TimeToLive_Iterations = 10;
        public Vector position1;
        public Vector position2;
        public Team   team;

        public Beam(Vector position1, Vector position2, Team team)
        {
            this.position1 = position1;
            this.position2 = position2;
            this.team = team;
        }
    }
}
