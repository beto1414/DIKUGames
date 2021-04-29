using System;

namespace Breakout.LevelLoading {
    public class LegendReader {

        public Char character;
        public string blockname;
        
        public LegendReader(string line) {
            character = Convert.ToChar(line[0]);
            blockname = line[3..(line.Length)];
        }
    }
}