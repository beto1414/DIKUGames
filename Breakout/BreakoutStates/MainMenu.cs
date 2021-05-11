using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Input;

namespace Breakout.BreakoutStates {
    public class MainMenu : IGameState {
        
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public void ResetState() {}
        public void UpdateState() {}
        public void RenderState() {
            backGroundImage.RenderEntity();
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
                        menuButtons[0].SetColor(System.Drawing.Color.Green);
                        menuButtons[1].SetColor(System.Drawing.Color.White);
                    break;
                case KeyboardKey.Down:
                    if (activeMenuButton < maxMenuButtons-1)
                        activeMenuButton += 1;
                        menuButtons[1].SetColor(System.Drawing.Color.Green);
                        menuButtons[0].SetColor(System.Drawing.Color.White);
                    break;
                case KeyboardKey.Enter:
                    switch (activeMenuButton) {
                    case 0:
                        BreakoutBus.GetBus().RegisterEvent(
                        // GameEventFactory<object>.CreateGameEventForAllProcessors(
                        //     GameEventType.GameStateEvent,
                        //     this,
                        //     "CHANGE_STATE",
                        //     "GAME_RUNNING", ""));
                            
                            new GameEvent{EventType = GameEventType.GameStateEvent, 
                                From = this,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING",
                                StringArg2 = "RESET_STATE"});
                        break;
                    case 1:
                        BreakoutBus.GetBus().RegisterEvent(
                        // GameEventFactory<object>.CreateGameEventForAllProcessors()
                        // GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                        new GameEvent{EventType = GameEventType.WindowEvent, 
                            Message = "CLOSE_WINDOW"});
                    break;
                    }
                break;
                }
            }
        }

        public MainMenu() {
            backGroundImage = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
            
            ResetState();
            
            activeMenuButton = 0;

            maxMenuButtons = 2;

            menuButtons = new Text[] {
                new Text("New Game", new Vec2F(0.4f, 0.3f), new Vec2F(0.4f, 0.3f)), 
                new Text("Exit Game", new Vec2F(0.4f, 0.2f), new Vec2F(0.4f, 0.3f))
            };
            
            menuButtons[0].SetColor(System.Drawing.Color.Green);
            menuButtons[1].SetColor(System.Drawing.Color.White);
        }

        public static MainMenu GetInstance () {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
    }
}