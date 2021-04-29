using System;
using DIKUArcade.GUI;
using LevelLoading;
using System.Collections.Generic;

namespace Breakout
{
    class Program
    {
        static void Main(string[] args)
        {
            var winArgs = new WindowArgs(); 
            var game = new Game(winArgs);
            game.Run();
        }
    }
}
