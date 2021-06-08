using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Blocks;
using DIKUArcade.Utilities;



namespace Breakout.LevelLoading {
    public static class CreateLevel {
        public static String[] level = new String[]{};
        public static String[] map = new String[]{}; 
        public static String[] metadata = new String[]{};
        public static String[] legend = new String[]{};
        public static List<LegendData> listOfLegends = new List<LegendData>();
        public static List<MetaData> listofMeta = new List<MetaData> ();
        public static EntityContainer<Block> blocks = new EntityContainer<Block> ();
        public static MetaData normMeta = new MetaData("                  ");
        public static float Time;

///<summary> Method is in charge of reading and parsing level-files.
/// File.ReadAllLines reads the file. 
///SplitArray() dissects the level into relevant sub-portions. 
///ReadLegendData ,Meta and -LevelTimer overwrites listOfLegends and listOfMeta which contain
///relevant information of legends and meta-data and Time.
///</summary>
///<param name="file"> string containing the path of a .txt level-file </param>.
        public static void ReadLevelFile(string file) {
                string path = Path.Combine(FileIO.GetProjectPath(),file);
                level = File.ReadAllLines(path);
                map = SplitArray(level,"Map:","Map/");
                metadata = SplitArray(level,"Meta:","Meta/");
                legend = SplitArray(level,"Legend:","Legend/");
                ReadLegendData(legend);
                ReadMetaData(metadata);
                LevelTimer();

        }

///<summary>
///Isolates a string array out of a bigger string array using indexes
///</summary>
///<param name="text">
///The bigger string array that needs a part isolated
///</param>
///<param name="textStart">
///A line where the method should start isolating from. The parameter should be the whole line
///</param>
///<param name="textEnd">
///A line where the mothod should stop isolating. The parameter should be the whole line
///</param>
///<returns>
///Returns the isolated string array
///</returns>
        public static string[] SplitArray(string[] text, string textStart, string textEnd) {
            if (Array.FindIndex(text, row => row.Contains(textStart)) != -1 && Array.FindIndex(text, row => row.Contains(textEnd)) != -1) {
                return text[(Array.FindIndex(text, row => row.Contains(textStart))+1)..Array.FindIndex(text, row => row.Contains(textEnd))];
            } else {
                throw new ArgumentException("404 text not found");
            }
        }

///<summary>
///Adds a LegendData-instance to the listOfLegends list for each line in given argument.
///Each LegendData contains a char and a string (filename)
///</summary>
///<param name="leg">
///A string array containing legends
///</param>
        public static void ReadLegendData(string[] leg) {
            listOfLegends = new List<LegendData>();
            foreach(var item in leg){
                listOfLegends.Add(new LegendData(item));
            }
        }
///<summary>
///Adds a MetaData-instance to the listOfMeta list for each line in given argument.
///Each MetaData contains one of three:
///1. blockName(string), blockType(enum), blockChar(char)
///2. Name(string)
///3. Time(int)
///</summary>
///<param name="line">
///A string array containing meta-data
///</param>
        public static void ReadMetaData(string[] line) {
            listofMeta = new List<MetaData>();
            foreach(var item in line) {
                listofMeta.Add(new MetaData(item));
            }
        }
///<summary>
///Adds a value to the field CreateLevel.Time if such information exists in the level-data.
///</summary>
        public static void LevelTimer() {
            foreach(var item in listofMeta) {
                if (item.Time > 0.00001f) {
                    Time = item.Time;
                }
            }
        }

///<summary>
///Method iterates through the x and y-axis of the screen, adding block-entities corresponding to 
///the characters of the given level. Each block is instantiated based on what character is found 
///and what legends and meta-data are relevant to the given level.
///</summary>
///<returns>
///Returns an entity container with blocks corresponding to the level which was loaded.
///</returns>
        public static EntityContainer<Block> DrawMap() {
            normMeta.blockType = BlockType.Normal;
            blocks.ClearContainer();
            float positionVarX = 1.0f/12.0f;
            float positionVarY = 1.0f/25.0f;
            for(int i = 0; i < map.Length; i++) {
                string temp = map[i];
                for(int j = 0; j < 11; j++) {
                    if(temp[j] != '-') {
                        var tempMeta = CharToMetaData(temp[j]);
                        blocks.AddEntity(tempMeta.charToBlock(new Vec2F(positionVarX*j, 1.0f-(positionVarY*i))));
                    }
                }
            }
            return blocks;
        }
///<summary>
///Method utilized in MetaData.charToBlock(method) which returns a blockImage if character is found in any of
///the items of listOfLegends. Method is also a fail-safe to make sure all characters in a level as recognized and
/// assigned to an image.
///</summary>
///<param name="c"> argument is a character, used in a pattern-matching fashion to find matches in the listOfLegends.
///</param>
///<returns>
///Returns a string with an image-path corresponding to the character argument.
///</returns>
        public static string CharToFile(char c) {
            foreach(LegendData item in listOfLegends) {
                if (item.character == c) {
                    return item.blockImage;
                }
            }
            return "No file-name with this associated character";
        }

///<summary>
///Finds the MetaData containing the char c (argument) and returns it. If not found, makes a dummy MetaData
///containing the char c and the BlockType BlockType.Normal
///</summary>
///<param name="c"> a char. Supposedly a char assigned to a MetaData in listofMeta by ReadLevelFile()
///</param>
///<returns>
///a MetaData from listofMeta if param is found, else a dummy MetaData
///</returns>
        public static MetaData CharToMetaData(char c) {
            foreach(MetaData item in listofMeta) {
                if (item.blockChar == c) {
                    return item;
                }
            }
            normMeta.blockChar = c;
            return normMeta;
        }

    }
}