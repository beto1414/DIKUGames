using System;

namespace Breakout.LevelLoading {
    public class LegendReader {

        public Char character;
        public string blockImage;
        
        public LegendReader(string line) {
            character = Convert.ToChar(line[0]);
            blockImage = line[3..(line.Length)];
        }
    }
}