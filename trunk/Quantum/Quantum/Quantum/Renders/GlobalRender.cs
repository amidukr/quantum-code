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
            drawOutposts(gameEvent);
            drawGeneral(gameEvent);
        }

        private void drawGeneral(GameEvent gameEvent)
        {
            General general = gameEvent.model.currentGeneral;
            Image generalImage;

            if (general.CurrentTeam == Team.green)
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
                Point centerOfOutpost = CenterOfObject(new Point((int)outpost.Position.X, (int)outpost.Position.Y));
                if (outpost.id == 1)
                    gameEvent.graphics.DrawImage(greyOutpostImage, centerOfOutpost);
                if (outpost.id == 2)
                    gameEvent.graphics.DrawImage(blueOutpostImage, centerOfOutpost);
                if (outpost.id == 3)
                    gameEvent.graphics.DrawImage(greenOutpostImage, centerOfOutpost);
            }
        }

        private Point CenterOfObject(System.Windows.Vector vector)
        {
            throw new NotImplementedException();
        }

        public void drawDrone(GameEvent gameEvent)
        {
            List<Drone> drones = gameEvent.model.currentGeneral.Drones;
            Image droneImage;
            if (drones.Count != 0)
            {
                if (gameEvent.model.currentGeneral.CurrentTeam == Team.green)
                    droneImage = greenDroneImage;
                else
                    droneImage = blueDroneImage;
                foreach (Drone drone in drones)
                {
                    gameEvent.graphics.DrawImage(greenDroneImage, CenterOfObject(drone.Position));
                }
            }
        }
        private void SimpleRotation(Image image, General general)
        {

        }
        private void RotateImage(Image image, General general, GameEvent gameEvent)
        {
            Matrix mat = new Matrix();
            double angle = Math.Atan2(general.Velocity.Y, general.Velocity.X);
            mat.RotateAt(90 + (float)(180 * angle / Math.PI), new PointF((float)general.Position.X, (float)general.Position.Y));
            mat.Translate(-30, -55);
            gameEvent.graphics.Transform = mat;
            gameEvent.graphics.DrawImage(image, new Point((int)general.Position.X, (int)general.Position.Y));
            gameEvent.graphics.Transform = new Matrix();
        }

        private Point CenterOfObject(Point objCordinates)
        {
            return new Point((int)(-objCordinates.X / 2), (int)(-objCordinates.Y / 2));
        }
    }
}
