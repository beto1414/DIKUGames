using System;

namespace LevelLoading {
    public class LegendReader {

        public Char character;
        public string blockname;
        
        public LegendReader(string line) {
            character = Convert.ToChar(line);
            blockname = line[3..(line.Length-1)];
        }
    }
}