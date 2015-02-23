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
            QuantumMapBuilder mapBuilder = new QuantumMapBuilder();
            game.start(mapBuilder.initializeMap(screenWidth, screenHeight), screenWidth, screenHeight);

            return game;
        }
    }
}
