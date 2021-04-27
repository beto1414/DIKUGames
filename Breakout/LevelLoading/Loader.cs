using System;
using System.IO;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;



namespace LevelLoading {
    public class Loader {
        public String[] level;
        public String[] map; 
        public String[] metadata;
        public String[] legend;
        public List<LegendReader> listOfLegends;        

        public EntityContainer<Block> blocks; 
        public Loader(){
            blocks = new EntityContainer<Block> ();    
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
        
        public void DrawMap(string[] mapArray) {
            float positionVarX = 1/12;
            float positionVarY = 1/25;
            for(int i = 0; i < mapArray.Length; i++) {
                var temp = mapArray[i];
                for(int j = 0; j < 11; j++) {
                    if(temp[j] != '-') {
                        Path.Combine("Assets","Images",CharToFile(temp[j]));
                            blocks.AddEntity(new Block(
                                new StationaryShape(new Vec2F(positionVarX*j, positionVarY*i), new Vec2F(0.08f, 0.03f)),
                                    new Image(Path.Combine("Assets","Images",CharToFile(temp[j])))));
                    }
                }
            }
        }

        public string CharToFile(char c) {
            foreach(var item in listOfLegends)
                if (item.character == c)
                    return item.blockname;
            return "No file-name with this associated character";
        }

    }
}

        //      private EntityContainer<Enemy> enemies;

            //  enemies = new EntityContainer<Enemy>(numEnemies);

            //  for (int i = 0; i < numEnemies; i++) {
            //     enemies.AddEntity(new Enemy(
            //         new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
            //             new ImageStride(80, images)));