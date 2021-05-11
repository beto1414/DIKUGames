using System; 
using System.IO;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Math; 
using DIKUArcade.Graphics;


namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        private Player player;
        public EntityContainer<Block> blocks;
        public Game(WindowArgs winArgs) : base(winArgs) {
            window.SetKeyEventHandler(KeyHandler);
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.PlayerEvent, GameEventType.WindowEvent});
            eventBus.Subscribe(GameEventType.PlayerEvent,this);
            eventBus.Subscribe(GameEventType.WindowEvent,this);
            Loader.Reader(Path.Combine("Assets","Levels","level1.txt"));
            blocks = Loader.DrawMap();
            
            player = new Player(new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));
        }

        public void IterateBlocks() {
            blocks.Iterate(block => {
                if (block.HitPoint == 0) {
                    block.DeleteEntity();
                }
            });
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            if(action == KeyboardAction.KeyPress) {
                player.KeyPress(key);
            }
            else if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Escape:
                        eventBus.RegisterEvent( new GameEvent{
                        EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                        break;
                    default:
                        player.KeyRelease(key);
                        break;
                }
            }
        }
        public override void Render() {
            blocks.RenderEntities();
            player.Render();
        }
        public override void Update() {
            player.Move();
            IterateBlocks();
            eventBus.ProcessEvents();
        }

        public void ProcessEvent(GameEvent gameEvent){
            if(gameEvent.Message == "CLOSE_WINDOW") {
                window.CloseWindow();
            }
        }
    }
}
