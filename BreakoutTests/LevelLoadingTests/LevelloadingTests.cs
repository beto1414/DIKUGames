using NUnit.Framework;
using Breakout;
using Breakout.LevelLoading;
using System;
using System.Collections.Generic;
using DIKUArcade.GUI;
using System.IO;

namespace BreakoutTests.LevelLoadingTests {
    [TestFixture]
    public class LevelloadingTests {
        public Loader LoadTester;
        public String[] levelNames;
        int[] intList;
        public WindowArgs winArgs;

        [SetUp]
        public void SettingUp(){
            winArgs = new WindowArgs();
            LoadTester = new Loader();
            levelNames = new String[] {"..\\..\\..\\Assets\\Levels\\central-mass.txt", 
                "..\\..\\..\\Assets\\Levels\\columns.txt",
                "..\\..\\..\\Assets\\Levels\\level1.txt",
                "..\\..\\..\\Assets\\Levels\\level2.txt",
                "..\\..\\..\\Assets\\Levels\\level3.txt",
                "..\\..\\..\\Assets\\Levels\\wall.txt"};
        }
        [Test]
        public void MetaTest_0() {
            LoadTester.Reader("..\\..\\..\\Assets\\Levels\\level1.txt");
            Assert.AreEqual(LoadTester.listofMeta.Count, 4);
        }
        [Test]
        public void MetaTest_1() {
            intList = new int[] {1,1,4,3,4,1};
            int counter = 0;
            foreach (var item in levelNames) {
                var LoadTester1 = new Loader();
                LoadTester1.Reader(item);
                Assert.AreEqual(LoadTester1.listofMeta.Count,intList[counter]);
                counter++;
            }
        }
        [Test]
        public void TestReaderMeta() {
            LoadTester.Reader(levelNames[2]);
            Assert.IsTrue(LoadTester.metadata[0] == "Name: LEVEL 1");
        }

        [Test]
        public void TestReaderMap() {
            LoadTester.Reader(levelNames[2]);
            Assert.IsTrue(LoadTester.map[4] == "-111----111-");
        }


        [Test]
        public void TestReaderLegends() {
            LoadTester.Reader(levelNames[3]);
        }
    }    
}


// using Galaga.GalagaStates;
// using Galaga;
// using DIKUArcade.EventBus;
// using System.Collections.Generic;

// namespace GalagaTests {
//     [TestFixture]
//     public class StateMachineTesting {
//         private StateMachine stateMachine;
//         [SetUp]
//         public void InitiateStateMachine() {
//             DIKUArcade.Window.CreateOpenGLContext();
//             // Here you should:
//             // (1) Initialize a GalagaBus with proper GameEventTypes
//             GalagaBus.GetBus().InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent, GameEventType.InputEvent});
//             // (2) Instantiate the StateMachine
//             stateMachine = new StateMachine();
//             // (3) Subscribe the GalagaBus to proper GameEventTypes
//             // and GameEventProcessors
//             //GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);
//             //GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, stateMachine);
//             //GalagaBus.GetBus().RegisterEvent(stateMachine.SwitchState(GameStateType.MainMenu));
//         }
//         [Test]
//         public void TestInitialState() {
//             Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
//         }
//         [Test]
//         public void TestEventGamePaused() {
//             GalagaBus.GetBus().RegisterEvent(
//                 GameEventFactory<object>.CreateGameEventForAllProcessors(
//                     GameEventType.GameStateEvent,
//                     this,
//                     "CHANGE_STATE",
//                     "GAME_PAUSED", ""));
        
//         GalagaBus.GetBus().ProcessEventsSequentially();
//             Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
//         }
//     }
// }