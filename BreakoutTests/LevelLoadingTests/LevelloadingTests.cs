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
