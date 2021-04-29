using System;

namespace LevelLoading {
    public class MetaReader {
        string Name;
        BlockSpecial block;
        int Time;
        
        public MetaReader(string line) {
            switch (line[0..3]) {
                case "Name":
                    Name = line[6..line.Length];
                    break;
                case "Unbr":
                    block = new BlockSpecial("Unbreakable",Convert.ToChar(line[13]));
                    break;
                case "Hard":
                    block = new BlockSpecial("Hardened",Convert.ToChar(line[10]));
                    break;
                case "Powe":
                    block = new BlockSpecial("PowerUp",Convert.ToChar(line[8]));
                    break;
                case "Time":
                    Time = Convert.ToInt32(line[6..8]);
                    break;
            }

            //character = Convert.ToChar(line[0]);
            //blockname = line[3..(line.Length)];

        }

    }
}