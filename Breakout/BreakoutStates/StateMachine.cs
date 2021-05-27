using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Math;
using System;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        private int speedStacks;
        private Random rand;

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            rand = new Random();
            ActiveState = MainMenu.GetInstance();
        }
        private void SwitchState(GameStateType stateType){
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break; 
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameLost:
                    ActiveState = GameLost.GetInstance();
                    break;
                case GameStateType.GameWon:
                    ActiveState = GameWon.GetInstance();
                    break;   
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "CHANGE_STATE") {
                SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
            } else if (gameEvent.Message == "CHANGE_SPEED") {
                Console.WriteLine(BreakoutBus.GetBus().HasTimedEvent(69));
                if (gameEvent.StringArg2 == "STACKS!") {
                        speedStacks++;
                        Console.WriteLine("Stacking... Amount of Stacks: " + speedStacks);
                } else if (gameEvent.StringArg2 == "REMOVE_STACKS") {
                    if (speedStacks > 0) {
                            speedStacks--;
                            Console.WriteLine("Stacking... Amount of Stacks: " + speedStacks);
                    }
                }
                if (gameEvent.StringArg1 == "SPEED_UP") {
                    Ball.MOVEMENT_SPEED = 0.03f;
                    GameRunning.balls.Iterate(ball => {ball.AlignSpeed();});
                } else if (gameEvent.StringArg1 == "SPEED_DOWN") {
                    Ball.MOVEMENT_SPEED = 0.0075f;
                    GameRunning.balls.Iterate(ball => {ball.AlignSpeed();});
                } else if (gameEvent.StringArg1 == "NORMALIZE_SPEED") {
                    //if (speedStacks <= 0) {
                        Console.WriteLine(BreakoutBus.GetBus().HasTimedEvent(69));
                        Ball.MOVEMENT_SPEED = 0.015f;
                        GameRunning.balls.Iterate(ball => {ball.AlignSpeed();});
                    //}
                }
            } else if (gameEvent.Message == "SHOOT_BALL") {
                    if (gameEvent.StringArg1 == "INFINITE_ON") {
                        GameRunning.infiniteMode = true;
                        Console.WriteLine("Turn on");
                    } else if (gameEvent.StringArg1 == "INFINITE_OFF") {
                        GameRunning.infiniteMode = false;
                        Console.WriteLine("Turn off");  
                    }
            }
            if (gameEvent.StringArg2 == "RESET_STATE") {
                ActiveState.ResetState();
            }
        }
    }      
}