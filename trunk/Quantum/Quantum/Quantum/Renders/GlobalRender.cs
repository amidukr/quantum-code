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
            General general = gameEvent.model.currentGeneral;
            Image generalImage;

            if (general.CurrentTeam == Team.green)
                generalImage = greenGeneralImage;
            else
                generalImage = blueGeneralImage;

            Matrix mat = new Matrix();
            double angle = Math.Atan2(general.Velocity.Y, general.Velocity.X);
            mat.RotateAt(90 + (float)(180 * angle / Math.PI), new PointF((float)general.Position.X, (float)general.Position.Y));
            mat.Translate(-30, -55);
            gameEvent.graphics.Transform = mat;           
            gameEvent.graphics.DrawImage(generalImage, new Point((int)general.Position.X, (int)general.Position.Y));
            gameEvent.graphics.Transform = new Matrix();
        }
        public void drawOutposts(GameEvent gameEvent) 
        {
            List<Outpost> Outposts = gameEvent.model.Outposts;
            foreach (Outpost outpost in Outposts) 
            {
                System.Drawing.Point centerOfOutpost = new System.Drawing.Point( (int)(-outpost.Position.X/2), (int)(-outpost.Position.Y/2));
                if (outpost.id == 1)
                    gameEvent.graphics.DrawImage(greyOutpostImage, centerOfOutpost);
                if (outpost.id == 2)
                    gameEvent.graphics.DrawImage(blueOutpostImage, centerOfOutpost);
                if (outpost.id == 3)
                    gameEvent.graphics.DrawImage(greenOutpostImage, centerOfOutpost);
            }
        }
        public void drawDrone(GameEvent gameEvent) 
        {
            //Drone drone = gameEvent.model.
        }
        private void SimpleRotation(Image image, General general)
        {
            
        }
        private void RotateImage(Image image, General general)
        {
            Graphics graph = Graphics.FromImage(image);
            double rotateAngel = Math.Atan2(general.Velocity.Y, general.Velocity.X) * (180 / Math.PI);
            graph.RotateTransform((float)rotateAngel);     
            //gameEvent.graphics.DrawEllipse(grayPen, general.Position.X - 10, general.Position.Y - 10, 20, 20);

        }
    }
}
