using System;
using System.IO;
using DIKUArcade.Graphics;


namespace LevelLoading {
    public class Loader {
        public String[] level;
        public void reader (string file) {
            if(File.Exists(Path.Combine("Assets","Levels",file))) {
                level = File.ReadAllLines(Path.Combine("Assets","Levels",file));    
            }

        }
        public void printer(){
            foreach(var item in level){
                Console.WriteLine(item.ToString());
            }
        }
        
        public Loader(){
        }
        
        /*public override string ToString() {
            return string.Format(
                foreach(var item in level){
                item + "\n"
            });
        }*/



        // public override string ToString() {
        //     return string.Format("Name of lecture; " + Name + 
        //     ", Number of students; " + NumOfStudentsOnline);
        // }
        

    }
}