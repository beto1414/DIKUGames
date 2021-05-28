using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Utilities;

namespace Breakout.PowerUps{
    public class HalfSpeed : PowerUp {
        public HalfSpeed(Vec2F Position) : 
            base(Position, new Image(Path.Combine(FileIO.GetProjectPath(),Path.Combine("Assets", "Images", "HalfSpeedPowerUp.png")))) {
        }

        public override void Activate() {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "CHANGE_SPEED",
                StringArg1 = "SPEED_DOWN"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent, 
                Message = "CHANGE_SPEED",
                StringArg1 = "NORMALIZE_SPEED",
                Id = 420}, TimePeriod.NewSeconds(5.0));
        }
    }
}