using System; 
using System.IO;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Events;
using Breakout.LevelLoading;
using Breakout.BreakoutStates;
using DIKUArcade.Entities;
using DIKUArcade.Math; 
using DIKUArcade.Graphics;


namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        //private Player player;
        //public EntityContainer<Block> blocks;
        private StateMachine stateMachine;
        public Game(WindowArgs winArgs) : base(winArgs) {
            window.SetKeyEventHandler(KeyHandler);
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> {
                GameEventType.PlayerEvent, GameEventType.WindowEvent, GameEventType.GameStateEvent});
            BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent,this);
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent,this);
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent,this);
            stateMachine = new StateMachine();
        }



        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);
            // if(action == KeyboardAction.KeyPress) {
            //     player.KeyPress(key);
            // }
            // else if (action == KeyboardAction.KeyRelease) {
            //     switch (key) {
            //         case KeyboardKey.Escape:
            //             BreakoutBus.GetBus().RegisterEvent( new GameEvent{
            //             EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
            //             break;
            //         default:
            //             player.KeyRelease(key);
            //             break;
            //     }
            // }
        }
        public override void Render() {
            stateMachine.ActiveState.RenderState();
        }
        public override void Update() {
            stateMachine.ActiveState.UpdateState();
            BreakoutBus.GetBus().ProcessEvents();
        }

        public void ProcessEvent(GameEvent gameEvent){
            if(gameEvent.Message == "CLOSE_WINDOW") {
                 window.CloseWindow();
            }
            stateMachine.ProcessEvent(gameEvent);
        }
    }
}
