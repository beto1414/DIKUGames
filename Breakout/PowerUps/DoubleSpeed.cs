using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;

namespace Breakout.PowerUps{
    public class DoubleSpeed : PowerUp {
        public DoubleSpeed(DynamicShape Shape, IBaseImage Image) : base(Shape, Image) {
            image = new Image(Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png"));
        }

        public override void Activate() {
            speedTimer = 5.0;
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "CHANGE_SPEED",
                StringArg1 = "SPEED_UP",
                StringArg2 = "STACK!"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent, 
                Message = "CHANGE_SPEED",
                StringArg1 = "NORMALIZE_SPEED",
                StringArg2 = "REMOVE_STACK"}, TimePeriod.NewSeconds(5.0));
        }
    }
}