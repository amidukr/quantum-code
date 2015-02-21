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

        private bool isDroneMovingIntoCloud(Drone drone)
        {
            return  drone.Order == DroneOrder.MoveToGeneral 
                 || drone.Order ==  DroneOrder.MoveToGeneral;
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

        private void moveDroneInCloud(QuantumModel model, Drone drone, Vector targetCloudCenter)
        {

        }

        public void execute(GameEvent gameEvent)
        {
            QuantumModel model = gameEvent.model;
            General general = model.currentGeneral;


            foreach (Drone drone in general.Drones)
            {
                if (isDroneMovingIntoCloud(drone))
                {
                    moveDroneInCloud(model, drone, getCloudCenterPosition(gameEvent.model, general, drone));
                }

            }

            foreach(Drone drone in gameEvent.model.currentGeneral.Drones) {
                gameEvent.graphics.DrawEllipse(grenPen, drone.Position.X - 10, drone.Position.Y - 10, 20, 20);
            }

            foreach (Outpost outpost in gameEvent.model.Outposts)
            {
                gameEvent.graphics.DrawEllipse(grayPen, (int)outpost.Position.X - 10, (int)outpost.Position.Y - 10, 20, 20);
            }
        }
    }
}
