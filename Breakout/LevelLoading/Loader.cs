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
        public void reader (string file) {
            if(File.Exists(Path.Combine("Assets","Levels",file))) {
                level = File.ReadAllLines(Path.Combine("Assets","Levels",file));
                map = splitArray(level,"Map:","Map/");
                metadata = splitArray(level,"Meta:","Meta/");
                legend = splitArray(level,"Legend:","Legend/");
            }
        }
        public void printer(string[] list){
            foreach(var item in list){
                Console.WriteLine(item.ToString());
            }
        }
        
        public string[] splitArray(string[] text, string textStart, string textEnd) {
            //string first = Array.Find(text,x => x.X)
            //if (Array.FindIndex(text, 0, text.Length - 1,first.textStart) != -1) {
            if (Array.FindIndex(text, row => row.Contains(textStart)) != -1 && Array.FindIndex(text, row => row.Contains(textEnd)) != -1) {
                return text[(Array.FindIndex(text, row => row.Contains(textStart))+1)..Array.FindIndex(text, row => row.Contains(textEnd))];
            } else {
                throw new ArgumentException("404 text not found");
            }
        }
        


    }
}