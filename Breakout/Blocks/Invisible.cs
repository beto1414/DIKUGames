using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {

    public class Invisible : Block {
        
        public Invisible (Shape Shape, IBaseImage Image) 
            : base (Shape, Image) {
                visible = false;
                HitPoint = 1;
                blockType = BlockType.Invisible;
                blockValue = 2; 
            }
        public override void Hit() {
            HitPoint -= 1;
            visible = true;
        }
    }
}