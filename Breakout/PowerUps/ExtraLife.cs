using System; 
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using System.IO;
using Breakout.BreakoutStates;

namespace Breakout.PowerUps {
    public class ExtraLife : PowerUp {
        public ExtraLife(DynamicShape shape, IBaseImage image) : base(shape, image){
            image = new Image(Path.Combine("Assets", "Images", "LifePickUp.png"));
        }
        public override void Activate() {
            GameRunning.player.life++;
        }
    }
}