using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public abstract class PowerUps : Entity {
        public DynamicShape shape;
        public IBaseImage image;

        public PowerUps (DynamicShape Shape, IBaseImage Image) : base (Shape, Image) {
            shape.Extent = new Vec2F(0.5f, 0.5f);
        }

        public abstract void Activate();
    }
}