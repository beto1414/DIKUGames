using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public abstract class PowerUp : Entity {
        public DynamicShape shape;
        public IBaseImage image;
        public static double speedTimer = 0.0;

        public PowerUp (DynamicShape Shape, IBaseImage Image) : base (Shape, Image) {
            shape.Extent = new Vec2F(0.5f, 0.5f);
            shape.AsDynamicShape().Direction = new Vec2F(0.0f, -0.01f);
        }

        public abstract void Activate();
        
        public void Move() {
            shape.Move();
        }
    }
}