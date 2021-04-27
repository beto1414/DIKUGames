using System;
using System.IO;
using DIKUArcade.Graphics;


namespace LevelLoading {
    public class Loader {
        public String[] level;
        public String[] map; 
        public String[] metadata;
        public String[] legend;        

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
        
        public string[] SplitArray(string[] text, string textStart, string textEnd) {
            if (Array.FindIndex(text, row => row.Contains(textStart)) != -1 && Array.FindIndex(text, row => row.Contains(textEnd)) != -1) {
                return text[(Array.FindIndex(text, row => row.Contains(textStart))+1)..Array.FindIndex(text, row => row.Contains(textEnd))];
            } else {
                throw new ArgumentException("404 text not found");
            }
        }
        
        public void AssignChar(string[] leg) {
            foreach(var item in leg){
                //item.Substring(0);
                Convert.ToChar(item);
            }
        }


    }
}