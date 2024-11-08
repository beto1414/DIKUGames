using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Timers;

namespace Breakout.BreakoutStates {
    public class GamePaused : IGameState {
        
        private static GamePaused instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public void ResetState() {}
        public void UpdateState() {}
        public void RenderState() {
            backGroundImage.RenderEntity();
            menuButtons[0].SetColor(System.Drawing.Color.White);
            menuButtons[1].SetColor(System.Drawing.Color.White);
            switch (activeMenuButton) {
            case 0:
                menuButtons[0].SetColor(System.Drawing.Color.Green);
                break;
            case 1:
                menuButtons[1].SetColor(System.Drawing.Color.Green);
                break;
            }
            foreach (Text n in menuButtons) {
                n.RenderText();
            }
        }
        
        public void HandleKeyEvent(KeyboardAction keyAction, KeyboardKey keyValue) {
            if (keyAction == KeyboardAction.KeyRelease) {
                switch (keyValue) {
                case KeyboardKey.Up:
                    if (activeMenuButton > 0)
                        activeMenuButton -= 1;
                    break;
                case KeyboardKey.Down:
                    if (activeMenuButton < maxMenuButtons-1)
                        activeMenuButton += 1;
                    break;
                case KeyboardKey.Enter:
                    switch (activeMenuButton) {
                    case 0:
                        BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_RUNNING"});
                            StaticTimer.ResumeTimer();
                        break;
                    case 1:
                        BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent{EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "MAIN_MENU"});
                    break;
                    }
                break;
                }
            }
        }

        public GamePaused() {
            backGroundImage = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
            
            activeMenuButton = 0;

            maxMenuButtons = 2;

            menuButtons = new Text[] {
                new Text("Continue", new Vec2F(0.4f, 0.3f), new Vec2F(0.4f, 0.3f)), 
                new Text("Main Menu", new Vec2F(0.4f, 0.2f), new Vec2F(0.4f, 0.3f))
            };
            
            menuButtons[0].SetColor(System.Drawing.Color.White);
            menuButtons[1].SetColor(System.Drawing.Color.White);
        }

///<summary>
///returns this class' instance. If the class is not instancilized, make a new instance
///</summary>
        public static GamePaused GetInstance () {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }
    }
} 