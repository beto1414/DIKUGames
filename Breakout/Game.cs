using System; 
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using LevelLoading;
//using DIKUArcade.Entities;

namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        public Loader maploader;


 
        public Game(WindowArgs winArgs) : base(winArgs) {
            window.SetKeyEventHandler(KeyHandler);
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {});
            maploader = new Loader();
            maploader.Reader("level1.txt");
            maploader.DrawMap();
        }
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if(action == KeyboardAction.KeyPress) {
            switch(key) {
                default:
                    break;
            }
            }
            else if (action == KeyboardAction.KeyRelease) {
                switch(key) {
                    default:
                        break;
                }
            }
        }
        public override void Render() {
            maploader.blocks.RenderEntities();
        }
        public override void Update() {
        }

        public void ProcessEvent(GameEvent gameEvent){

        }
    }
}
