using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public abstract class Block : Entity {

        public Shape shape;
        public IBaseImage image;
        public int hitPoint;
        public bool unbreakable;
        public static bool visible;
        public BlockType blockType;
        public int blockValue;


        public Block (Shape Shape, IBaseImage Image) 
            : base (Shape, Image) {}

        public abstract void Hit();
    }
}