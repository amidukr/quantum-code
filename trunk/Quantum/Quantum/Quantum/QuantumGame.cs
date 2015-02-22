using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quantum.Quantum.Controllers;
using Quantum.Quantum;
using System.Windows;

namespace Quantum.Quantum
{

    interface GameController
    {
        void execute(GameEvent gameEvent);
    }

    class GameEvent
    {
        public readonly QuantumModel model;
        public readonly double deltaTime;
        public readonly QuantumGame game;
        public readonly Graphics graphics;
        public readonly Vector mousePosition;
        public readonly double width, height;

        public GameEvent(QuantumGame game, double deltaTime, Graphics graphics, double width, double height)
        {
            this.game = game;
            this.deltaTime = deltaTime;
            this.graphics = graphics;
            this.model = game.model;
            this.mousePosition = game.mousePosition;
            this.width = width;
            this.height = height;
        }

        public Boolean isButtonPressed(Keys key)
        {
            return game.isButtonPressed(key);
        }

        public Boolean isButtonPressed(MouseButtons key)
        {
            return game.isButtonPressed(key);
        }
    }


    class QuantumGame
    {


        private readonly Dictionary<object, bool> keyTable = new Dictionary<object, bool>();
        public Vector mousePosition {get; set;}
        public readonly QuantumModel model = new QuantumModel();

        private long lastExecution;
        private bool firstExecution = true;


        private Brush backgroundBrush = new SolidBrush(Color.FromArgb(unchecked((int)0xff444465)));


        private List<GameController> controllers = new List<GameController>();

        private QuantumMapBuilder mapBuilder = new QuantumMapBuilder();

        private void initialize(double width, double height)
        {


            controllers.Add(new OutpostConquestController());
            controllers.Add(new GeneralController(Keys.W,  Keys.S,    Keys.A,    Keys.D,     Team.green));
            controllers.Add(new GeneralController(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Team.blue));
            controllers.Add(new DroneOrderingController(Team.green, Keys.Q, MouseButtons.Left));
            controllers.Add(new DroneOrderingController(Team.blue, Keys.ShiftKey, MouseButtons.Right));
            controllers.Add(new DroneController());
            controllers.Add(new DrownRespawnerController());
            controllers.Add(new GlobalRender());

            mapBuilder.initializeMap(this, width, height);
            
        }

        public void playNext(Graphics g, double width, double height)
        {
            long currentTime = System.DateTime.Now.Ticks;
            long deltaTime   = currentTime - lastExecution;
            lastExecution = currentTime;

            if (firstExecution)
            {
                initialize(width, height);
                firstExecution = false;
                return;
            }

            GameEvent gameEvent = new GameEvent(this, deltaTime / 100000.0, g, width, height);

            foreach (GameController controller in controllers)
            {
                controller.execute(gameEvent);
            }
        }



        public Boolean isButtonPressed(Object key)
        {
            return !keyTable.ContainsKey(key) ? false : keyTable[key];
        }

        public void changeInputState(Object key, Boolean pressed) {
            keyTable[key] = pressed;
        }
    }
}
