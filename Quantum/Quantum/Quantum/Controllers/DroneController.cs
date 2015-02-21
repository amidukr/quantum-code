using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows;

namespace Quantum.Quantum.Controllers
{   
    class DroneController: GameController
    {
        private Pen grenPen = new Pen(Color.Green, 3);
        private Pen grayPen = new Pen(Color.Gray, 3);
        private Random random = new Random();

        private bool isDroneMovingIntoCloud(Drone drone)
        {
            return  drone.Order == DroneOrder.MoveToGeneral 
                 || drone.Order ==  DroneOrder.MoveToOutpost;
        }

        private Vector getCloudCenterPosition(QuantumModel model, General general, Drone drone)
        {
            if (drone.Order == DroneOrder.MoveToGeneral)
            {
                return general.Position;
            }
            else if (drone.Order == DroneOrder.MoveToOutpost)
            {
                return model.findOutpostById(drone.TargetOutpost).Position;
            }

            throw new Exception("Unable to found cloud center position");

        }

        private void moveDroneInCloud(GameEvent gameEvent, Drone drone, Vector targetCloudCenter)
        {
            QuantumModel model = gameEvent.model;
            double cloudRadius = model.cloudRadius;
            Vector distanceToCenter = Vector.Subtract(targetCloudCenter, drone.Position);
            Vector directionToCenter = distanceToCenter;


            double positionChange = model.dronSpeedConstant * gameEvent.deltaTime;
            directionToCenter.Normalize();

            Vector droneMovement = Vector.Multiply(positionChange, directionToCenter);

            double precision = 0.9;
            double randomValue = random.NextDouble()*precision*2 + 1 - precision;

            //double randomValue = random.NextDouble();
            
            if (distanceToCenter.Length > cloudRadius + positionChange)
            {
                drone.Position = Vector.Add(drone.Position, Vector.Multiply(droneMovement, randomValue));
            }
            else
            {
                drone.Position = Vector.Add(drone.Position, new Vector(randomValue * droneMovement.Y, -randomValue*droneMovement.X));
            }
        }

        public void execute(GameEvent gameEvent)
        {
            QuantumModel model = gameEvent.model;
            General general = model.currentGeneral;


            foreach (Drone drone in general.Drones)
            {
                if (isDroneMovingIntoCloud(drone))
                {
                    moveDroneInCloud(gameEvent, drone, getCloudCenterPosition(gameEvent.model, general, drone));
                }

            }

            foreach(Drone drone in gameEvent.model.currentGeneral.Drones) {
                gameEvent.graphics.DrawEllipse(grenPen, (int)drone.Position.X - 10, (int)drone.Position.Y - 10, 20, 20);
            }

            foreach (Outpost outpost in gameEvent.model.Outposts)
            {
                gameEvent.graphics.DrawEllipse(grayPen, (int)outpost.Position.X - 10, (int)outpost.Position.Y - 10, 20, 20);
            }
        }
    }
}
