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
            var general = gameEvent.model.currentGeneral;
            Image generalImage;

            if (general.Team == Team.green)
                generalImage = greenGeneralImage;
            else
                generalImage = blueGeneralImage;

            Matrix mat = new Matrix();
            //Matrix mat = new Matrix(1, -1, 1, -1, 0, 0);

            double angle = Math.Atan2(general.Velocity.Y, general.Velocity.X);

            mat.RotateAt(90 + (float)(180 * angle / Math.PI), new PointF((float)general.Position.X, (float)general.Position.Y));
            mat.Translate(-30, -55);
           // mat.Translate(10f, 10f);

            gameEvent.graphics.Transform = mat;

            gameEvent.graphics.DrawImage(generalImage, new Point((int)general.Position.X, (int)general.Position.Y));

            gameEvent.graphics.Transform = new Matrix();
            //gameEvent.graphics.DrawEllipse(grayPen, general.Position.X - 10, general.Position.Y - 10, 20, 20);

        }
    }
}
