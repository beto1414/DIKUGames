using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public class Unbreakable : Block {
        public Unbreakable(Shape shape, Image image) : base(shape, image) {
            unbreakable = true;
            visible = true;
            HitPoint = 0;
            blockType = BlockType.Unbreakable;
            blockValue = 0;
        }

        public override void Hit() {
            
        } 
    }
}