using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Quantum.Quantum
{
    class GlobalRender : GameController
    {
        private Image blueGeneralImage = Image.FromFile(@"Resources\blue-general.png");
        private Image greenGeneralImage = Image.FromFile(@"Resources\green-general.png");
        private Image blueDroneImage = Image.FromFile(@"Resources\blue-drone.png");
        private Image blueOutpostImage = Image.FromFile(@"Resources\blue-outpost.png");
        private Image greenDroneImage = Image.FromFile(@"Resources\green-drone.png");
        private Image greenOutpostImage = Image.FromFile(@"Resources\green-outpost.png");
        private Image greyOutpostImage = Image.FromFile(@"Resources\grey-outpost.png");

        public void execute(GameEvent gameEvent)
        {
            var general = gameEvent.model.currentGeneral;
            Image generalImage;

            if (general.Team == Team.green)
                generalImage = greenGeneralImage;
            else
                generalImage = blueGeneralImage;


            string pathToImage = string.Empty;
            gameEvent.graphics.DrawImage(generalImage, general.Position);

        }
    }
}
