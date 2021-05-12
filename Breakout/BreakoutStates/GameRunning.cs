using Breakout;
using System;
using DIKUArcade.State;
using DIKUArcade.Input; 
using DIKUArcade.Entities;
using Breakout.LevelLoading;
using DIKUArcade.Events;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

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
        
        public void ResetState() { //skriv constructoren her
            Loader.Reader(Path.Combine("Assets","Levels","level1.txt"));
            blocks = Loader.DrawMap();
            backGroundImage = new Entity(
                new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
            player = new Player(new DynamicShape(new Vec2F(0.4f, 0.05f), new Vec2F(0.20f, 0.05f)), 
                new Image(Path.Combine("Assets","Images","player.png")));
            
            
            
        }
        public void UpdateState() {
            player.Move();
        }
        public void RenderState() { 
            backGroundImage.RenderEntity();
            if (GameState) {player.Render();} 
            if (GameState) {blocks.RenderEntities();}
        }
        public void HandleKeyEvent(KeyboardAction KeyAction, KeyboardKey keyValue) {
            if(KeyAction == KeyboardAction.KeyRelease) {
                player.KeyRelease(keyValue);
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
            if(KeyAction == KeyboardAction.KeyPress) {
                player.KeyPress(keyValue);
            }  
        }

        public GameRunning() {
            ResetState();
        }

        public static GameRunning GetInstance () {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void IterateBlocks() {
            blocks.Iterate(block => {
                if (block.HitPoint == 0) {
                    block.DeleteEntity();
                }
            });
        }
    }
}