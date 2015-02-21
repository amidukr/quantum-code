using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Quantum.Quantum.Controllers
{
    class DroneOrderingController: GameController
    {
        private double accumulatedTime = 0;
        public void execute(GameEvent gameEvent)
        {
            QuantumModel model = gameEvent.model;
            double cloudRadius = model.cloudRadius;
            General general = model.currentGeneral;
            Vector targetPosition = gameEvent.mousePosition;
            

            if (gameEvent.isButtonPressed(MouseButtons.Left)) {
                giveOrderToDrones(gameEvent, model, general, targetPosition, cloudRadius);
            }
            else
            {
                accumulatedTime = 0;
            }

            if(gameEvent.isButtonPressed(Keys.Q)) {

            }
               
        }

        public void giveOrderToDrones(GameEvent gameEvent, QuantumModel model, General general, Vector targetPosition, double cloudRadius)
        {
             Vector distanceToPoint = Vector.Subtract(targetPosition, general.Position);

            double milsForDrone = distanceToPoint.Length * distanceToPoint.Length * distanceToPoint.Length / model.milsOrderFactor;

            int amountOfDroneToSend = (int)((accumulatedTime + gameEvent.deltaTime) / milsForDrone);
            accumulatedTime += gameEvent.deltaTime - amountOfDroneToSend*milsForDrone;

            foreach (Drone dron in general.Drones)
            {
                if (amountOfDroneToSend <= 0) break;

                if (dron.Order == DroneOrder.MoveToGeneral 
                   && Vector.Subtract(dron.Position, general.Position).Length < cloudRadius * 1.5)
                {
                    amountOfDroneToSend--;

                    dron.TargetPosition = targetPosition;
                    dron.Order = DroneOrder.MoveToPosition;
                }
            }
        }
    }
}
