using System; 
using System.IO;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Math; 
using DIKUArcade.Graphics;


namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        public Loader maploader;
        private Player player;

 
        public Game(WindowArgs winArgs) : base(winArgs) {
            window.SetKeyEventHandler(KeyHandler);
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.PlayerEvent});
            eventBus.Subscribe(GameEventType.PlayerEvent,this);
            maploader = new Loader();
            maploader.Reader("level1.txt");
            maploader.DrawMap();
            player = new Player(new DynamicShape(new Vec2F(0.5f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));
        }
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if(action == KeyboardAction.KeyPress) {
                player.KeyPress(key);
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
            player.Render();
        }
        public override void Update() {
        }

        public void ProcessEvent(GameEvent gameEvent){
            
        }
    }
}
