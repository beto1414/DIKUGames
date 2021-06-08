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
        public String[] levelNames;
        int[] intList;
        public WindowArgs winArgs;
        [SetUp]
        public void SettingUp(){
            winArgs = new WindowArgs();
            levelNames = new String[] {Path.Combine("Assets","Levels","central-mass.txt"),
                Path.Combine("Assets","Levels","columns.txt"),
                Path.Combine("Assets","Levels","level1.txt"),
                Path.Combine("Assets","Levels","level2.txt"),
                Path.Combine("Assets","Levels","level3.txt"),
                Path.Combine("Assets","Levels","wall.txt")};
        }
        [Test]
        public void MetaTest_0() {
            CreateLevel.ReadLevelFile(Path.Combine("Assets","Levels","level1.txt"));
            Assert.AreEqual(CreateLevel.listofMeta.Count, 3);
        }
        
        [Test]
        public void MetaTest_1() {
            intList = new int[] {1,1,3,3,4,1};
            int counter = 0;
            foreach (var item in levelNames) {
                CreateLevel.ReadLevelFile(item);
                Assert.AreEqual(CreateLevel.listofMeta.Count,intList[counter]);
                counter++;
            }
        }
        [Test]
        public void TestReadLevelFileMeta() {
            CreateLevel.ReadLevelFile(levelNames[2]);
            Assert.IsTrue(CreateLevel.metadata[0] == "Name: LEVEL 1");
        }

        [Test]
        public void TestReadLevelFileMap() {
            CreateLevel.ReadLevelFile(levelNames[2]);
            Assert.IsTrue(CreateLevel.map[4] == "-111----111-");
        }

        [Test]
        public void TestReadLevelFileLegends() {
            CreateLevel.ReadLevelFile(levelNames[2]);
            Assert.IsTrue(CreateLevel.legend[3] == "q) darkgreen-block.png");
        }
    }    
}
