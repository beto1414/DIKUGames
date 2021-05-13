 using DIKUArcade.Entities;
 using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public class Normal : Block {
        public Normal (Shape shape, Image image) : base(shape, image) {
            visible = true;
            HitPoint = 1; 
            unbreakable = false; 
            blockType = BlockType.Normal;
            blockValue = 1;
        }

        public override void Hit() {
            HitPoint -= 1;
        }

    }
}