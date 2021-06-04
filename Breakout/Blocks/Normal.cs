 using DIKUArcade.Entities;
 using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public class Normal : Block {
        public Normal (Shape shape, Image image) : base(shape, image) {
            visible = true;
            hitPoint = 1; 
            unbreakable = false; 
            blockType = BlockType.Normal;
            blockValue = 1;
        }

///<summary>
///Block takes damage
///</summary>
        public override void Hit() {
            hitPoint -= 1;
        }

    }
}