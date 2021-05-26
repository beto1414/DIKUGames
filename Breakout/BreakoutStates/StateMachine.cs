using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        private int speedStacks;

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);

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
                if (gameEvent.StringArg2 == "STACKS!") {
                        speedStacks++;
                    } else if (gameEvent.StringArg2 == "REMOVE_STACKS") {
                        if (speedStacks > 0) {
                            speedStacks--;
                        }
                    }
                if (gameEvent.StringArg1 == "SPEED_UP") {
                    Ball.MOVEMENT_SPEED = 0.06f;
                } else if (gameEvent.StringArg1 == "SPEED_DOWN") {
                    Ball.MOVEMENT_SPEED = 0.015f;
                } else if (gameEvent.StringArg1 == "NORMALIZE_SPEED") {
                    if (speedStacks == 0) {
                        Ball.MOVEMENT_SPEED = 0.03f;
                    }
                }
            }
            if (gameEvent.StringArg2 == "RESET_STATE") {
                ActiveState.ResetState();
            }
        }
    }      
}