using NUnit.Framework; 
using DIKUArcade.Entities;
using Breakout;

namespace EntityTests {
    public class BlockTests {
        public Block BlockTesting;
        EntityContainer<Block> BlockEntities;

        public Game newGame;

        [Setup]

        public void SetUp() {
            BlockTesting1 = new Block(new Shape(new Vec2F(0.05f, 0.05f), new Vec2F(0,05f, 0.05f)),
                            new Image(Path.Combine("Assets","Images","purple-block.png"),0));
            BlockTesting2 = new Block(new Shape(new Vec2F(0.05f, 0.05f), new Vec2F(0,05f, 0.05f)),
                            new Image(Path.Combine("Assets","Images","yellow-block.png"),4));
            newGame = new Game();

            BlockEntities = new EntityContainer<Block> ();
            BlockEntities.AddEntity(BlockTesting1);
            BlockEntities.AddEntity(Blocktesting2);
            
        }
        [Test]
        public void BlockDelete () {
            newGame.IterateBlocks(BlockEntities);
            Assert.IsTrue(BlockEntities.Count() == 1);
        }
    }
}

