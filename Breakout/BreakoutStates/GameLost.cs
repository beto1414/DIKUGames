using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.BreakoutStates{
    public class GameLost : IGameState {
        private static GameLost instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons; 
        private Text screenText;
        public void ResetState(){}
        public void UpdateState(){}
        public void RenderState(){
            backGroundImage.RenderEntity();
            screenText.RenderText();
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
        public void HandleKeyEvent(KeyboardAction keyAction, KeyboardKey keyValue){
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
                            StringArg1 = "GAME_RUNNING",
                            StringArg2 = "RESET_STATE"});
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

        public GameLost() {
            backGroundImage = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));
            
            activeMenuButton = 0;

            maxMenuButtons = 2;

            menuButtons = new Text[] {
                new Text("Play Again", new Vec2F(0.4f, 0.3f), new Vec2F(0.4f, 0.3f)), 
                new Text("Main Menu", new Vec2F(0.4f, 0.2f), new Vec2F(0.4f, 0.3f))
            };
            screenText = new Text ("You Lost", new Vec2F(0.4f, 0.3f), new Vec2F(0.6f, 0.4f));
            screenText.SetColor(System.Drawing.Color.White);
        }

        public static GameLost GetInstance() {
            return GameLost.instance ?? (GameLost.instance = new GameLost());
        }
    }
}