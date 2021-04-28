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
            //var loader = new Loader();
            game.Run();

            //loader.Reader("level1.txt");
            //loader.Printer(loader.map);
            

        }
    }
}
