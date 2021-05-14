using System; 
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout {
    public class ScoreBoard : Text {
        private int points; 
        public ScoreBoard(string text, Vec2F pos, Vec2F extent) : base(text, pos, extent) {
            SetColor(System.Drawing.Color.White); 
            points = 0;
        } 

        public void AddPoints (int point) {
            points += point;
            SetText("Score: " + Convert.ToString(points));
        }
    }
}