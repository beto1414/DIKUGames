 using DIKUArcade.Entities;
 using DIKUArcade.Graphics;
 using System;
 using Breakout.PowerUps;

namespace Breakout.Blocks {
    public class PowerUpBlock : Block {
        public Random rand = new Random();
        public PowerUpBlock (Shape shape, Image image) : base(shape, image) {
            visible = true;
            hitPoint = 1; 
            unbreakable = false; 
            blockType = BlockType.PowerUpBlock;
            blockValue = 5;
        }

        public override void Hit() {
            hitPoint -= 1;
        }
    }
}