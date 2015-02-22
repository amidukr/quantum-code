using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Quantum.Quantum
{
    class QuantumMapBuilder
    {
        public Outpost createOutpost(QuantumModel model, Vector position)
        {
            Outpost outpost = new Outpost();

            outpost.Position = position;
            outpost.id = model.generateID();

            model.Outposts.Add(outpost);

            return outpost;
        }

        public General createGeneral(QuantumModel model, Vector position, Team team)
        {
            General general = new General(team);

            general.Position = position;

            model.Generals.Add(general);

            return general;
        }

        public Drone createDrone(QuantumModel model, General general, Vector position)
        {
            Drone drone = new Drone();

            drone.Position = position;

            drone.Order = DroneOrder.MoveToGeneral;

            general.Drones.Add(drone);

            return drone;
        }

        public void fillGeneralWithDrones(QuantumModel model, General general, Vector position, int n)
        {
            for (int i = 0; i < n; i++)
            {
                createDrone(model, general, position);
            }   
        }

        public void initializeMap(QuantumGame game, double width, double height)
        {
            QuantumModel model = game.model;

            General greenGeneral = createGeneral(model, new Vector(50,         height / 2 - 50), Team.green);
            General blueGeneral  = createGeneral(model, new Vector(width - 50, height / 2 - 50), Team.blue);


            Outpost outpostGreen = createOutpost(model, new Vector(300, 300));
            Outpost outpostBlue  = createOutpost(model, new Vector(width-300, height-300));



            Drone droneGreen = createDrone(model, greenGeneral, new Vector(0, 0));
            Drone droneBlue  = createDrone(model, blueGeneral,  new Vector(width, height));

            droneGreen.TargetOutpost = outpostGreen.id;
            droneGreen.Order = DroneOrder.MoveToOutpost;

            droneBlue.TargetOutpost = outpostBlue.id;
            droneBlue.Order = DroneOrder.MoveToOutpost;


            fillGeneralWithDrones(model, greenGeneral, new Vector(0,     0),      1000);
            fillGeneralWithDrones(model, blueGeneral,  new Vector(width, height), 1000);
        }
    }
}
