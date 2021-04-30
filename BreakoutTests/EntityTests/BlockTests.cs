using NUnit.Framework; 
using DIKUArcade.Entities;
using Breakout;
using Breakout.LevelLoading;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using System.Collections.Generic;
using System;
using DIKUArcade.GUI;

namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class BlockTests {
        public Block BlockTesting1;
        public Block BlockTesting2;
        public Game newGame;
        public Loader loader;
        public WindowArgs winArgs;
        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs(); 
            newGame = new Game(winArgs);
            newGame.maploader.Reader("..\\..\\..\\Assets\\Levels\\level1.txt"); //76 blocks
        }

        [Test]
        public void BlockDelete () {
            newGame.maploader.blocks.Iterate(block => 
                  block.HitPoint = 0);
            newGame.IterateBlocks();
            Assert.IsTrue(newGame.maploader.blocks.CountEntities() == 0);

        }
    }
}

