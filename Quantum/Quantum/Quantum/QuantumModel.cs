using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using Point = System.Drawing.Point;

namespace Quantum.Quantum
{

    

    public enum Team { green, blue }
    public enum DroneOrder { MoveToGeneral, MoveToPosition, MoveToOutpost}

    class QuantumModel
    {
        public const int radiusMin = 10;
        public const int radiusMax = 13;
        private int nextUniqueId = 1;
        public readonly double speedConstant = 1.0;

        public General currentGeneral          = new General(Team.green);
        public readonly List<Outpost> Outposts = new List<Outpost>();

        public Outpost findOutpostById(int id)
        {
            foreach(Outpost outpost in this.Outposts) {
                if(outpost.id == id) return outpost;
            }
            
            return null;
        }

        public int generateID()
        {
            return nextUniqueId++;
        }
    }

    class Outpost
    {   
        public int id { get; set; }
        public Vector Position { get; set; }
    }

    class Drone
    {

        public Drone()
        {
            Health = 3; //default value  
        }

        public int   Health   { get; set; }
        public Point Position { get; set; }

        public Point      TargetPosition{get; set;}
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

        public Quantum.Team CurrentTeam { get; set; }
        public Vector Speed { get; set;}
    }
}
