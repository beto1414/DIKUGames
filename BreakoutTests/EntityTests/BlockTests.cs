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
using Breakout.BreakoutStates;
using DIKUArcade.Events;

namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class BlockTests {
        public Game newGame;
        public WindowArgs winArgs;
        public GameRunning state;

        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs(); 
            newGame = new Game(winArgs);
            state = new GameRunning();
            CreateLevel.ReadLevelFile(Path.Combine("Assets","Levels","level1.txt"));
            state.blocks = CreateLevel.DrawMap();
        }

        [Test]
        public void BlockDelete () {
            state.blocks.Iterate(block => 
                  block.hitPoint = 0);
            state.IterateBallz();
            Assert.IsTrue(state.blocks.CountEntities() == 0);

        }
    }
}