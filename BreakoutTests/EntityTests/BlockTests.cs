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
        EntityContainer<Block> BlockEntities;
        public Game newGame;
        public Loader loader;
        

        [SetUp]

        public void SetUp() {
            BlockTesting1 = new Block(new StationaryShape(new Vec2F(0.05f, 0.05f), new Vec2F(0.05f, 0.05f)),
                            new Image(Path.Combine("Assets","Images","purple-block.png")),0);
            BlockTesting2 = new Block(new StationaryShape(new Vec2F(0.05f, 0.05f), new Vec2F(0.05f, 0.05f)),
                            new Image(Path.Combine("Assets","Images","yellow-block.png")),4);
            var winArgs = new WindowArgs(); 
            newGame = new Game(winArgs);
            loader = new Loader();
            loader.Reader("level1.txt"); //76 blocks

            BlockEntities = new EntityContainer<Block> ();
            BlockEntities.AddEntity(BlockTesting1);
            BlockEntities.AddEntity(BlockTesting2);
            
        }
        [Test]
        public void BlockDelete () {
            //loader.blocks.entities[2].HitPoint = 0;
            loader.blocks.Iterate(block => 
            block.HitPoint = 0);
            newGame.IterateBlocks();
            Assert.IsTrue(BlockEntities.CountEntities() == 0);
        }
    }
}

