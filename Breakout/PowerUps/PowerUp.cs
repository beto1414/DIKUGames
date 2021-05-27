using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public abstract class PowerUp : Entity {

        public PowerUp (Vec2F Position, IBaseImage Image) : base (new DynamicShape(Position, new Vec2F(0.05f, 0.05f), new Vec2F(0.0f, -0.01f)), Image) {
        }

        public abstract void Activate();
        
        public void Move() {
            Shape.AsDynamicShape().Move();
        }
    }
}