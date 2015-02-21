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

        public GameEvent(QuantumGame game, double deltaTime, Graphics graphics)
        {
            this.game = game;
            this.deltaTime = deltaTime;
            this.graphics = graphics;
            this.model = game.model;
            this.mousePosition = game.mousePosition;
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

        private DroneController droneControler = new DroneController();
        private GlobalRender globalRender = new GlobalRender();
        private GeneralController generalController = new GeneralController();
        
        private void initialize() {
            Outpost outpost = new Outpost();
            
            outpost.Position = new Vector(200, 200);
            outpost.id = model.generateID();

            model.Outposts.Add(outpost);

            Drone drone = new Drone();

            drone.TargetOutpost = outpost.id;
            drone.Order = DroneOrder.MoveToOutpost;

            model.currentGeneral.Drones.Add(drone);
        }

        public void playNext(Graphics g)
        {
            long currentTime = System.DateTime.Now.Ticks;
            long deltaTime   = currentTime - lastExecution;
            lastExecution = currentTime;

            if (firstExecution)
            {
                initialize();
                firstExecution = false;
                return;
            }

            GameEvent gameEvent = new GameEvent(this, deltaTime/100000.0, g);

            g.FillRectangle(backgroundBrush, g.ClipBounds);

            droneControler.execute(gameEvent);
            globalRender.execute(gameEvent);
            generalController.execute(gameEvent);

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
