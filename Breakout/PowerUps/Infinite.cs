using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Math;
using Breakout.BreakoutStates;
using System;

namespace Breakout.PowerUps{
    public class Infinite : PowerUp {
        public Infinite(DynamicShape Shape, IBaseImage Image) : base(Shape, Image) {
            image = new Image(Path.Combine("Assets", "Images", "InfinitePowerUp.png"));
        }

        public override void Activate() {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "SHOOT_BALL", StringArg1 = "INFINITE_ON"}); 
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                Message = "SHOOT_BALL", StringArg1 = "INFINITE_OFF"}, TimePeriod.NewSeconds(5.0));       
        }
    }
}