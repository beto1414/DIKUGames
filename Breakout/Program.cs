using System;
using DIKUArcade.GUI;
using Breakout.LevelLoading;
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
            //loader.Reader("level1.txt");
            //Console.WriteLine(loader.listofMeta.Count);
            game.Run();
        }
    }
}
