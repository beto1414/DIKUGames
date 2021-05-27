using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Utilities;
using DIKUArcade.Math;

namespace Breakout.PowerUps{
    public class DoubleSpeed : PowerUp {
        public DoubleSpeed(Vec2F Position) : 
            base(Position, new Image(Path.Combine(FileIO.GetProjectPath(),Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png")))) {
            //image = new Image(Path.Combine(FileIO.GetProjectPath(),Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png")));
        }

        public override void Activate() {
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent, 
                Message = "CHANGE_SPEED",
                StringArg1 = "NORMALIZE_SPEED",
                StringArg2 = "REMOVE_STACK",
                Id = 69}, TimePeriod.NewMilliseconds(5000));
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "CHANGE_SPEED",
                StringArg1 = "SPEED_UP",
                StringArg2 = "STACKS!"});
            // BreakoutBus.GetBus().RegisterTimedEvent(new TimedGameEvent(
            //         TimePeriod.NewSeconds(3),
            //         new GameEvent {
            //             EventType = GameEventType.TimedEvent, 
            //             Message = "CHANGE_SPEED",
            //             StringArg1 = "NORMALIZE_SPEED",
            //             StringArg2 = "REMOVE_STACK",
            //             }),TimePeriod.NewSeconds(3));
        }
    }
}