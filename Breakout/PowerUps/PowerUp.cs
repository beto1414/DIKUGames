using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public abstract class PowerUp : Entity {

        public PowerUp (Vec2F Position, IBaseImage Image) : base (new DynamicShape(Position, new Vec2F(0.05f, 0.05f), new Vec2F(0.0f, -0.01f)), Image) {
        }

///<summary>
/// Activates the powerup.
///</summary>
        public abstract void Activate();
        
///<summary>
///Move is the method that allows the powerup to move.
///</summary>
        public void Move() {
            Shape.AsDynamicShape().Move();
        }
    }
}