using System;
using Breakout.Blocks;
using System.IO; 
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;

namespace Breakout.LevelLoading {
    public class MetaReader {
        public string Name;
        public BlockType blockType;
        public int Time;
        public string blockName;
        public char blockChar;
        public MetaReader(string line) {
            switch (line[0..4]) {
                case "Name":
                    Name = line[6..line.Length];
                    break;
                case "Unbr":
                    blockChar = Convert.ToChar(line[line.Length-1]);
                    blockName = "Unbreakable";
                    blockType = BlockType.Unbreakable;
                    break;
                case "Hard":
                    blockChar = Convert.ToChar(line[line.Length-1]);
                    blockName = "Hardened";
                    blockType = BlockType.Hardened;
                    break;
                case "Powe":
                    blockChar = Convert.ToChar(line[line.Length-1]);
                    blockName = "PowerUp";
                    blockType = BlockType.PowerUp;
                    break;
                case "Time":
                    Time = Convert.ToInt32(line[6..8]);
                    break;
                case "Invi":
                    blockChar = Convert.ToChar(line[line.Length-1]);
                    blockName = "Invisible";
                    blockType = BlockType.Invisible;
                    break;
            }
        }

        public Block charToBlock (Vec2F position) {
            switch (blockType) {
                case BlockType.Unbreakable :
                    return new Unbreakable(new StationaryShape(position, new Vec2F(0.08f, 0.03f)), 
                        new Image(Path.Combine("Assets","Images",Loader.CharToFile(blockChar))));
                case BlockType.Invisible :
                    return new Invisible(new StationaryShape(position, new Vec2F(0.08f, 0.03f)), 
                        new Image(Path.Combine("Assets","Images",Loader.CharToFile(blockChar))));
                default:
                    return new Normal(new StationaryShape(position, new Vec2F(0.08f, 0.03f)), 
                        new Image(Path.Combine("Assets","Images",Loader.CharToFile(blockChar))));
            }
        }
    }
}