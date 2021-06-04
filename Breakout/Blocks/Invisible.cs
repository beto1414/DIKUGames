using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public class Invisible : Block {
        
        public Invisible (Shape Shape, IBaseImage Image) 
            : base (Shape, Image) {
                visible = false;
                hitPoint = 1;
                blockType = BlockType.Invisible;
                blockValue = 2; 
            }

///<summary>
///Block takes damage and turns all invisible blocks visible
///</summary>
        public override void Hit() {
            hitPoint -= 1;
            visible = true;
        }
    }
}