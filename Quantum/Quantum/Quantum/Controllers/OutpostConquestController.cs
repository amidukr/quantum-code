using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantum.Quantum.Controllers
{
    class OutpostConquestController : GameController
    {
        double usedTime;

        public void execute(GameEvent gameEvent)
        {
            QuantumModel model = gameEvent.model;
            usedTime += gameEvent.deltaTime;

            if (usedTime < model.outpostConquestTime)
            {
                return;
            }

            Console.WriteLine("Check outposts");

            usedTime -= model.outpostConquestTime;

            

            foreach(Outpost outpost in gameEvent.model.Outposts) {
                List<General> generalsParticipators = new List<General>();

                foreach(General general in model.Generals) {
                    if(general.FindDroneCloseToOutpost(outpost, model.cloudRadius) != null) {
                        generalsParticipators.Add(general);
                    }
                }

                if (generalsParticipators.Count == 1)
                {
                    Team newTeam = generalsParticipators[0].Team;
                    if (outpost.Team != newTeam)
                    {
                        Console.WriteLine("Outpost was conquested by " + newTeam);
                    }
                        
                    outpost.Team = generalsParticipators[0].Team;
                }
            }
        }
    }
}
