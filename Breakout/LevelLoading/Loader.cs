using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.Reflection;
using Breakout;
using DIKUArcade.Utilities;



namespace Breakout.LevelLoading {
    public static class Loader {
        public static String[] level = new String[]{};
        public static String[] map = new String[]{}; 
        public static String[] metadata = new String[]{};
        public static String[] legend = new String[]{};
        public static List<LegendReader> listOfLegends = new List<LegendReader>();
        public static List<MetaReader> listofMeta = new List<MetaReader> ();
        public static EntityContainer<Block> blocks = new EntityContainer<Block> ();

        public static void Reader(string file) {
            //if(File.Exists(file)) {
                string path = Path.Combine(FileIO.GetProjectPath(),file);
                Console.WriteLine(path); 
                level = File.ReadAllLines(path);
                map = SplitArray(level,"Map:","Map/");
                metadata = SplitArray(level,"Meta:","Meta/");
                legend = SplitArray(level,"Legend:","Legend/");
                AssignChar(legend);
                AssignMeta(metadata);
            //}
            //else {throw new ArgumentException("404 File not found");}
        }
        public static void Printer(string[] list){
            foreach(var item in list){
                Console.WriteLine(item.ToString());
            }
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
///Adds a LegendReader-instance to the listOfLegends list for each line in given argument.
///Each LegendReader contains a char and a string (filename)
///</summary>
///<param name="leg">
///A string array containing legends
///</param>
        public static void AssignChar(string[] leg) {
            listOfLegends = new List<LegendReader>();
            foreach(var item in leg){
                listOfLegends.Add(new LegendReader(item));
            }
        }
        public static void AssignMeta(string[] line) {
            listofMeta = new List<MetaReader>();
            foreach(var item in line) {
                listofMeta.Add(new MetaReader(item));
            }
        }

        public static EntityContainer<Block> DrawMap() {
            float positionVarX = 1.0f/12.0f;
            float positionVarY = 1.0f/25.0f;
            for(int i = 0; i < map.Length; i++) {
                string temp = map[i];
                for(int j = 0; j < 11; j++) {
                    if(temp[j] != '-') {
                        blocks.AddEntity(new Block(
                            new StationaryShape(new Vec2F(positionVarX*j, 1.0f-(positionVarY*i)), new Vec2F(0.08f, 0.03f)),
                                new Image(Path.Combine("Assets","Images",CharToFile(temp[j]))), 1));
                    }
                }
            }
            return blocks;
        }

        public static string CharToFile(char c) {
            foreach(LegendReader item in listOfLegends) {
                if (item.character == c) {
                    return item.blockname;
                }
            }
            return "No file-name with this associated character";
        }

    }
}