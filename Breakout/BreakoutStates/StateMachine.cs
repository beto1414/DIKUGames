using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            //BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

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
            }
        }
        
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "CHANGE_STATE") {
                SwitchState(StateTransformer.TransformStringToState(gameEvent.StringArg1));
            }
            if (gameEvent.StringArg2 == "RESET_STATE") {
                ActiveState.ResetState();
            }
            // if (gameEvent.EventType == GameEventType.InputEvent) {
            //     ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.StringArg1);
            // }
            // if (type == GameEventType.InputEvent) {
            //     ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            // }
        }
    }      
}