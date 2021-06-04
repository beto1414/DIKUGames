using System;

namespace Breakout.BreakoutStates {
    public class StateTransformer {
///<summary>
///Transforms a string into a GameStateType
///</summary>
///<param name="state">
///A string that matches a GameStateType
///</param>
///<returns>
///A GameStateType matching the string
///</returns>
        public static GameStateType TransformStringToState(string state) {
            switch (state) {
            case "GAME_RUNNING" :
                return GameStateType.GameRunning;
            case "GAME_PAUSED" :
                return GameStateType.GamePaused;
            case "MAIN_MENU" :
                return GameStateType.MainMenu;
            case "GAME_LOST" :
                return GameStateType.GameLost;
            case "GAME_WON" :
                return GameStateType.GameWon;
            default :
                throw new ArgumentException(String.Format("This is not a gametype"));
            }
        }

///<summary>
///Transforms a GameStateType into a string
///</summary>
///<param name="state">
///A GameStateType
///</param>
///<returns>
///A string matching the GameStateType argument
///</returns>
        public static string TransformStateToString(GameStateType state) {
            switch (state) {
            case GameStateType.GameRunning :
                return "GAME_RUNNING";
            case GameStateType.GamePaused :
                return "GAME_PAUSED";
            case GameStateType.MainMenu :
                return "MAIN_MENU";
            case GameStateType.GameLost :
                return "GAME_LOST";
            case GameStateType.GameWon :
                return "GAME_WON";
            default :
                throw new ArgumentException(String.Format("This is not a gametype"));
            }
        }
    }
}