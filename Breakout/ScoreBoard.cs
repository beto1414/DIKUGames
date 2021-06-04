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
///<summary>
///Adds point to the scorerboard. 
///</summary>
///<param name="points">Points is a field that is a positive integer.
///</param>
        public void AddPoints (int point) {
            points += point;
            SetText("Score: " + Convert.ToString(points));
        }
    }
}