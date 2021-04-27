using System; 
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;

namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        public Game(WindowArgs winArgs) : base(winArgs) {
            window.SetKeyEventHandler(KeyHandler);
        }
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if(action == KeyboardAction.KeyPress) {
            }
            else if (action == KeyboardAction.KeyRelease) {
            }
        }
        public override void Render() {
            throw new System.NotImplementedException();
        }
        public override void Update() {
            throw new System.NotImplementedException();
        }

        public void ProcessEvent(GameEvent gameEvent){

        }
    }
}
