using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Graphics;


namespace LevelLoading {
    public class Loader {
        public String[] level;
        public String[] map; 
        public String[] metadata;
        public String[] legend;
        public List<LegendReader> listOfLegends;        

        public Loader(){
        }
        public void Reader(string file) {
            if(File.Exists(Path.Combine("Assets","Levels",file))) {
                level = File.ReadAllLines(Path.Combine("Assets","Levels",file));
                map = SplitArray(level,"Map:","Map/");
                metadata = SplitArray(level,"Meta:","Meta/");
                legend = SplitArray(level,"Legend:","Legend/");
            }
        }
        public void Printer(string[] list){
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
        public string[] SplitArray(string[] text, string textStart, string textEnd) {
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
        public void AssignChar(string[] leg) {
            foreach(var item in leg){
                //item.Substring(0);
                listOfLegends.Add(new LegendReader(item));
                
                //Convert.ToChar(item);
                //name = new LegendReader(item);
            }
        }
    }
}