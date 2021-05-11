using Breakout;
using System;
using DIKUArcade.State;
using DIKUArcade.Input; 
using DIKUArcade.Entities;
using Breakout.LevelLoading;
using DIKUArcade.Events;
using System.IO;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState{
        public EntityContainer<Block> blocks;
        private static GameRunning instance = null;
        private Entity backGroundImage;
        private bool GameState = true;
        private Player player;
    

        private void GameOver() {
            GameState = false;
        }
        
        public void ResetState() {
            Init();
        }
        public void UpdateState() {}
        public void RenderState() {
            backGroundImage.RenderEntity();
            if (GameState) {player.Render();} 
            if (GameState) {Loader.DrawMap();}
            if (GameState) {blocks.RenderEntities();}
        }
        public void HandleKeyEvent(KeyboardAction KeyAction, KeyboardKey keyValue) {
            if(KeyAction == KeyboardAction.KeyRelease) {
                switch(keyValue) {
                    case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(
                    new GameEvent{EventType = GameEventType.GameStateEvent, 
                        From = this,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_PAUSED"});
                    break;
                    }
                }
            }
        public GameRunning() {
            Init();
        }
        //Tilføjer denne funktion således at jeg kan kalde det i constructeren. (Skriv contructeren her)
        private void Init() {
            Loader.Reader(Path.Combine("Assets","Levels","level1.txt"));
            blocks = Loader.DrawMap();
        }

        public static GameRunning GetInstance () {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
    }
}