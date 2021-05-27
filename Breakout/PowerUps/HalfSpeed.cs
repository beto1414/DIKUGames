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
                StringArg1 = "SPEED_DOWN",
                StringArg2 = "STACKS!"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent, 
                Message = "CHANGE_SPEED",
                StringArg1 = "NORMALIZE_SPEED",
                StringArg2 = "REMOVE_STACK",
                Id = 420}, TimePeriod.NewSeconds(5.0));
        }
    }
}