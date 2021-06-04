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
                    blockType = BlockType.PowerUpBlock;
                    break;
                case "Time":
                    Time = Convert.ToInt32(line[6..9]);
                    break;
                case "Invi":
                    blockChar = Convert.ToChar(line[line.Length-1]);
                    blockName = "Invisible";
                    blockType = BlockType.Invisible;
                    break;
            }
        }
///<summary>
///The method returns a block object after pattern-matching through what type the block is.
//Loader.CharToFile is a helper function which indentifies what image the specific block has.
///</summary>
///<param name="position"> Vec2F containing a position on the screen.
///</param>
///<returns>
///Returns a block object of a certain type.
///</returns>
        public Block charToBlock (Vec2F position) {
            switch (blockType) {
                case BlockType.Unbreakable :
                    return new Unbreakable(new StationaryShape(position, new Vec2F(0.08f, 0.03f)), 
                        new Image(Path.Combine("Assets","Images",Loader.CharToFile(blockChar))));
                case BlockType.Invisible :
                    return new Invisible(new StationaryShape(position, new Vec2F(0.08f, 0.03f)), 
                        new Image(Path.Combine("Assets","Images",Loader.CharToFile(blockChar))));
                case BlockType.PowerUpBlock :
                    return new PowerUpBlock(new StationaryShape(position, new Vec2F(0.08f, 0.03f)), 
                        new Image(Path.Combine("Assets","Images",Loader.CharToFile(blockChar))));
                default:
                    return new Normal(new StationaryShape(position, new Vec2F(0.08f, 0.03f)), 
                        new Image(Path.Combine("Assets","Images",Loader.CharToFile(blockChar))));
            }
        }
    }
}