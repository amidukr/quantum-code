using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quantum.Quantum;
using System.Windows;

namespace Quantum
{
    public partial class QuantumForm : Form
    {
        private QuantumGame game = new QuantumGame();
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        private readonly int canvasWidth, canvasHeight;

        public QuantumForm()
        {
            InitializeComponent();

            canvasWidth  = 1024;
            canvasHeight = 1024;

            context = BufferedGraphicsManager.Current;
            grafx = context.Allocate(this.CreateGraphics(),
                new Rectangle(0, 0, this.Width, this.Height));
        }

        private void onTimer(object sender, EventArgs e)
        {
            restartButton.Visible = game.playNext(null, Width, Height);
            restartButton.Enabled = restartButton.Visible;
            this.Refresh();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            game.changeInputState(e.KeyCode, true);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            game.changeInputState(e.KeyCode, false);
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            game.changeInputState(e.Button, true);

            Console.WriteLine("Mouse Down: " + e.X + "; " + e.Y);
        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {
            game.changeInputState(e.Button, false);
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            game.mousePosition = new Vector(e.X, e.Y);
        }

        private void QuantumForm_Paint(object sender, PaintEventArgs e)
        {
            game.playNext(e.Graphics, Width, Height);
        }

        private void onRestart(object sender, EventArgs e)
        {
            game = new QuantumGame();
            this.ActiveControl = null;
        }
    }
}
