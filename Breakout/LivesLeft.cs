using System; 
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout {
    public class LivesLeft : Text {
        public int lives; 
        public LivesLeft(string text, Vec2F pos, Vec2F extent) : base(text, pos, extent) {
            SetColor(System.Drawing.Color.White); 
             lives = 3;
        } 

        public void LoseLife () {
            lives -= lives;
            SetText("Lives left: " + Convert.ToString(lives));
        }
        public void AddLife () {
            lives += lives;
            SetText("Lives left: " + Convert.ToString(lives));
        }
    }
}