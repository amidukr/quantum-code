using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantum.Quantum.Factory
{
    class SinglePlayerGameFactory
    {
        public QuantumGame create(int screenWidth, int screenHeight)
        {
            QuantumGame game = new QuantumGame();
            game.start(screenWidth, screenHeight);
            return game;
        }
    }
}
