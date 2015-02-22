using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Quantum.Quantum.Controllers
{
    class DronsFighting : GameController
    {
        public void execute(GameEvent gameEvent)
        {
            General gen1 = gameEvent.model.FindGeneralByTeam(Team.blue);
            General gen2 = gameEvent.model.FindGeneralByTeam(Team.green);

            double minFightDistance = 150;

            List<Drone> list1 = gen1.Drones;
            List<Drone> list2 = gen2.Drones;

            List<Beam> beamList = new List<Beam>();

            list1.ForEach(p => p.Attacking = false);
            list2.ForEach(p => p.Attacking = false);

            foreach (Drone dron1 in list1)
            {
                foreach (Drone dron2 in list2)
                {
                    double distance = Vector.Subtract(dron1.Position, dron2.Position).Length;
                    if (distance < minFightDistance)
                    {
                        if (!dron1.Attacking)
                        {
                            gameEvent.model.Beams.Add(new Beam(dron1.Position, dron2.Position, gen2.Team));
                            dron2.Health--;
                            dron1.Attacking = true;
                        }
                        if (!dron2.Attacking)
                        {
                            gameEvent.model.Beams.Add(new Beam(dron1.Position, dron2.Position, gen1.Team));
                            dron1.Health--;
                            dron2.Attacking = true;
                        }
                    }
                }
            }

            foreach (General general in gameEvent.model.Generals)
            {
                general.Drones.RemoveAll(p => p.Health <= 0);
            }
            
        }
    }
}
