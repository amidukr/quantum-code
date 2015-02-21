using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using Point = System.Drawing.Point;

namespace Quantum.Quantum
{
    class QuantumModel
    {
        public const int radiusMin = 10;
        public const int radiusMax = 13;

        public General currentGeneral = new General(Team.red);
    }

    class General
    {
        public readonly Team Team;

        public General(Team team)
        {
            this.Team = team;
            Health = 3; //default value            
        }
        
        public Point  Position {get; set;}
        public Vector Velocity {get; set;}

        public int Health { get; set; }
        public List<string> Drones { get; set; }
    }

    public enum Team { red, blue}
}
