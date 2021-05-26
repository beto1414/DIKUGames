using System; 
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Timers;

namespace Breakout {
    public class TimeBoard : Text {
        public float secondsLeft;
        public bool levelHasTimer;
        public TimeBoard(string text, Vec2F pos, Vec2F extent) : base(text, pos, extent) {
            SetColor(System.Drawing.Color.White);
            levelHasTimer = false;
        }
        public void SetTimer (float sec) {
            secondsLeft = sec*10;
        }
        public void RunClock () {
            if (levelHasTimer) {
                if ( 1000.0f < StaticTimer.GetElapsedMilliseconds() ) {
                    StaticTimer.RestartTimer();
                    secondsLeft -= 1.0f;
                    SetText("Time: " + Convert.ToString(secondsLeft));
                }
            }
        }
    }
}