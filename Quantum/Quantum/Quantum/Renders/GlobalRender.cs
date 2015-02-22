using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Quantum.Quantum
{
    class GlobalRender : GameController
    {
        private Image background = Image.FromFile(@"Resources\Background.jpg");
        private Image blueGeneralImage = Image.FromFile(@"Resources\blue-general.png");
        private Image greenGeneralImage = Image.FromFile(@"Resources\green-general.png");
        private Image blueDroneImage = Image.FromFile(@"Resources\blue-drone.png");
        private Image blueOutpostImage = Image.FromFile(@"Resources\blue-outpost.png");
        private Image greenDroneImage = Image.FromFile(@"Resources\green-drone.png");
        private Image greenOutpostImage = Image.FromFile(@"Resources\green-outpost.png");
        private Image greyOutpostImage = Image.FromFile(@"Resources\grey-outpost.png");

        private Pen grayPen = new Pen(Color.Gray, 3);

        public void execute(GameEvent gameEvent)
        {
            if (gameEvent.graphics == null) return;

            gameEvent.graphics.DrawImage(background, 0, 0, (float)gameEvent.width, (float)gameEvent.height);

            drawOutposts(gameEvent);

            List<General> generals = gameEvent.model.Generals;

            foreach (General general in generals)
            {
                drawGeneral(gameEvent, general);
            }

            foreach (General general in generals)
            {
                drawDrone(gameEvent, general);
            }
        }

        private void drawGeneral(GameEvent gameEvent, General general)
        {
            Image generalImage;

            if (general.Team == Team.green)
                generalImage = greenGeneralImage;
            else
                generalImage = blueGeneralImage;
            RotateImage(generalImage, general, gameEvent);
        }

        private void drawOutposts(GameEvent gameEvent)
        {
            List<Outpost> Outposts = gameEvent.model.Outposts;
            foreach (Outpost outpost in Outposts)
            {
                if (outpost.Team == Team.neutral)
                    gameEvent.graphics.DrawImage(greyOutpostImage, (int)outpost.Position.X - 50, (int)outpost.Position.Y - 50, 100, 100);
                else if (outpost.Team == Team.blue)
                    gameEvent.graphics.DrawImage(blueOutpostImage, (int)outpost.Position.X - 50, (int)outpost.Position.Y - 50, 100, 100);
                else if (outpost.Team == Team.green)
                    gameEvent.graphics.DrawImage(greenOutpostImage, (int)outpost.Position.X - 50, (int)outpost.Position.Y - 50, 100, 100);
            }
        }
        public void drawDrone(GameEvent gameEvent, General general)
        {
            List<Drone> drones = general.Drones;
            Image droneImage;
            if (drones.Count != 0)
            {
                if (general.Team == Team.green)
                    droneImage = greenDroneImage;
                else
                    droneImage = blueDroneImage;
                foreach (Drone drone in drones)
                {
                    gameEvent.graphics.DrawImage(droneImage, (int)drone.Position.X - 8, (int)drone.Position.Y - 8, 16, 16);
                }
            }
        }

        private void RotateImage(Image image, General general, GameEvent gameEvent)
        {
            Matrix mat = new Matrix();
            double angle = Math.Atan2(general.Velocity.Y, general.Velocity.X);
            mat.RotateAt(90 + (float)(180 * angle / Math.PI), new PointF((float)general.Position.X, (float)general.Position.Y));
            mat.Translate(-16, -28);
            gameEvent.graphics.Transform = mat;
            gameEvent.graphics.DrawImage(image, (int)general.Position.X, (int)general.Position.Y, 32, 55);
            gameEvent.graphics.Transform = new Matrix();
        }

    }
}
