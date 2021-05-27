using System; 
using DIKUArcade.Math;
using DIKUArcade.Graphics;

namespace Breakout {
    public class LivesLeft : Text {
        public int lives; 
        public LivesLeft(string text, Vec2F pos, Vec2F extent) : base(text, pos, extent) {
            SetColor(System.Drawing.Color.White); 
             lives = 3;
             SetText("Lives left: " + Convert.ToString(lives));
        } 

        public void LoseLife () {
            if ((lives -= 1) < -0.0000001) {
                lives = 0;
            } else {
                lives -= 1;
                SetText("Lives left: " + Convert.ToString(lives));
            }
        }
        public void AddLife () {
            lives += 1;
            SetText("Lives left: " + Convert.ToString(lives));
        }
    }
}