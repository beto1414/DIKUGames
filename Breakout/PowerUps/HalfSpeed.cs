using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;

namespace Breakout.PowerUps{
    public class HalfSpeed : PowerUps {
        public HalfSpeed(DynamicShape Shape, IBaseImage Image) : base(Shape, Image) {
            image = new Image(Path.Combine("Assets", "Images", "HalfSpeedPowerUp.png"));
        }

        public override void Activate() {
            Ball.MOVEMENT_SPEED /= 2;
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "CHANGE_SPEED",
                StringArg1 = "SPEED_DOWN"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent, 
                Message = "CHANGE_SPEED",
                StringArg1 = "SPEED_UP"}, TimePeriod.NewSeconds(5.0));
        }
    }
}