using NUnit.Framework; 
using DIKUArcade.Entities;
using Breakout;
using Breakout.LevelLoading;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using System;
using System.Reflection;
using DIKUArcade.GUI;

namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class BlockTests {
        public Block BlockTesting1;
        public Block BlockTesting2;
        public Game newGame;
        //public Loader loader;
        public WindowArgs winArgs;

        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs(); 
            newGame = new Game(winArgs);
            //Loader.Reader("Assets\\Levels\\level1.txt"); //76 blocks
            // DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(
            //             Assembly.GetExecutingAssembly().Location));
            //     dir = dir.Parent;
            // var buildDir = 
            // Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // var filePath = buildDir + @"Assets/Levels/level1.txt";
            //string filePath = Path.Combine(DIKUArcade.Utilities.FileIO.GetProjectPath(), "Assets","Levels","level1.txt");
            //Loader.Reader(Path.Combine(dir.FullName.ToString(),"Assets","Levels","level1.txt"));
            //Loader.Reader("/home/daniel/Documents/Datalogi/Softwareudvikling/Afleveringer/uge8/DIKUGames/BreakoutTests/EntityTests/Assets");
            Loader.Reader(Path.Combine("Assets","Levels","level1.txt"));
        }

        [Test]
        public void BlockDelete () {
            newGame.blocks.Iterate(block => 
                  block.HitPoint = 0);
            newGame.IterateBlocks();
            Assert.IsTrue(newGame.blocks.CountEntities() == 0);

        }
    }
}