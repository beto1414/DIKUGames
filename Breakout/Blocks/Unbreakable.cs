using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public class Unbreakable : Block {
        public Unbreakable(Shape shape, Image image) : base(shape, image) {
            unbreakable = true;
            visible = true;
            hitPoint = 0;
            blockType = BlockType.Unbreakable;
            blockValue = 0;
        }

///<summary>
///Block is indestuctable and does not take damage
///</summary>
        public override void Hit() {
            
        } 
    }
}