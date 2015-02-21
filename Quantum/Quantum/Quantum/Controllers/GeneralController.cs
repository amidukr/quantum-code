using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace Quantum.Quantum.Controllers
{
    class GeneralController : GameController
    {
        //private Pen blackPen = new Pen(Color.Black, 3);

        public void execute(GameEvent gameEvent)
        {
            double angle = -10;
            Vector prev = gameEvent.model.currentGeneral.Velocity;
            Direction direction = Direction.noChange;
            if (gameEvent.isButtonPressed(Keys.W) && gameEvent.isButtonPressed(Keys.D))
            {
                angle = -Math.PI/4;
                direction = Direction.right_up;
            }
            else if (gameEvent.isButtonPressed(Keys.S) && gameEvent.isButtonPressed(Keys.D))
            {
                angle = Math.PI/4;
                direction = Direction.right_down;
            }
            else if (gameEvent.isButtonPressed(Keys.S) && gameEvent.isButtonPressed(Keys.A))
            {   
                angle = 3*Math.PI/4;
                direction = Direction.left_down;
            }
            else if (gameEvent.isButtonPressed(Keys.A) && gameEvent.isButtonPressed(Keys.W))
            {
                angle = -(3*Math.PI/4);
                direction = Direction.left_up;
            }
            else if (gameEvent.isButtonPressed(Keys.W))
            {
                angle = -Math.PI/2;
                direction = Direction.up;
            }
            else if (gameEvent.isButtonPressed(Keys.D))
            {
                angle = 0;
                direction = Direction.right;
            }
            else if (gameEvent.isButtonPressed(Keys.S))
            {
                angle = Math.PI/2;
                direction = Direction.down;
            }
            else if (gameEvent.isButtonPressed(Keys.A))
            {
                angle = -Math.PI;
                direction = Direction.left;
            }
            Console.WriteLine(string.Format("New direction: {0}.", direction.ToString()));

            if (angle != -10)
                gameEvent.model.currentGeneral.Velocity = new Vector(Math.Cos(angle) * gameEvent.model.speedConstant, Math.Sin(angle) * gameEvent.model.speedConstant);
            else
                gameEvent.model.currentGeneral.Velocity = new Vector(0, 0);

            gameEvent.model.currentGeneral.Position = new System.Drawing.Point((int)(gameEvent.model.currentGeneral.Position.X + (gameEvent.model.currentGeneral.Velocity.X * gameEvent.deltaTime)), (int)(Math.Round(gameEvent.model.currentGeneral.Position.Y + (gameEvent.model.currentGeneral.Velocity.Y * gameEvent.deltaTime))));
        }
        private enum Direction { up, right_up, right, right_down, down, left_down, left, left_up, noChange}
    }
}