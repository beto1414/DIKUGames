using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;
using Breakout.BreakoutStates;
using System;
using DIKUArcade.Utilities;

namespace Breakout.PowerUps{
    public class Infinite : PowerUp {
        public Infinite(Vec2F Position) : base (Position, 
            new Image(Path.Combine(FileIO.GetProjectPath(),Path.Combine("Assets", "Images", "InfinitePowerUp.png")))){
        }
///<summary>
///allows the player to shoot an infinite amount of balls.
///</summary>
        public override void Activate() {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "SHOOT_BALL", StringArg1 = "INFINITE_ON"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "SHOOT_BALL", StringArg1 = "INFINITE_OFF"}, TimePeriod.NewSeconds(3.0f));     
        }
    }
}