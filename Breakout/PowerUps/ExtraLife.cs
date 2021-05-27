using System; 
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using Breakout.BreakoutStates;
using DIKUArcade.Utilities;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public class ExtraLife : PowerUp {

        public ExtraLife(Vec2F Position) : base (Position, 
            new Image(Path.Combine(FileIO.GetProjectPath(),Path.Combine("Assets", "Images", "LifePickUp.png")))){
        }
        public override void Activate() {
            GameRunning.livesLeft.AddLife();
        }
    }
}