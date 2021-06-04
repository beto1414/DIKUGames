using DIKUArcade.Events;

namespace Breakout {
    public static class BreakoutBus {
        public static GameEventBus eventBus;

///<summary>
///Calls the eventbus. If the eventbus is not instancilized, instancilize a new one
///</summary>
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus =
                                          new GameEventBus());
        }
    }
}