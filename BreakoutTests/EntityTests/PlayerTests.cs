using NUnit.Framework;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System;
using System.IO;
using DIKUArcade.Input;
using DIKUArcade.GUI;


namespace BreakoutTests.EntityTests {
    [TestFixture]
    public class PlayerTests {
        private Player player;
        private WindowArgs winArgs;   
        [SetUp]
        public void SetUp() {
            winArgs = new WindowArgs();
            player = new Player(new DynamicShape(new Vec2F(0.5f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));

        }

        [Test]
        public void TestGetPos() {
            Assert.IsTrue(player.getPos().X == 0.5f);
        }

        [Test]
        public void TestMoveRight() {
            player.KeyPress(KeyboardKey.Right);
            player.Move();
            Assert.IsTrue(player.getPos().X == 0.54f);
        }

        [Test]
        public void TestMoveLeft() {
            player.KeyPress(KeyboardKey.Left);
            player.Move();
            Assert.IsTrue(player.getPos().X == 0.46f);
        }

        [Test]
        public void TestMoveOutOfBoundsRight() {
            player.KeyPress(KeyboardKey.Right);
            for (int i = 0; i < 100; i++) {
                player.Move();
            }
            Assert.IsTrue((player.getPos().X - (1.0f - player.getExtent())) < 0.05f);
        }

        [Test]
        public void TestMoveOutOfBoundsLeft() {
            player.KeyPress(KeyboardKey.Left);
            for (int i = 0; i < 100; i++) {
                player.Move();
            }
            Console.Write(player.getPos().X);
            Assert.IsTrue((player.getPos().X - 0.0f) < 0.05f);
        }
    }
}