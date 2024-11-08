using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Input;
using Breakout.BreakoutStates;
using System;

namespace Breakout {
    public class Player {
        private Entity entity;
        private DynamicShape shape;
        private const float MOVEMENT_SPEED = 0.04f;
        private float moveRight = 0.0f;  
        private float moveLeft = 0.0f;
        private Random rand;
        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
///<summary> Renders the player </summary>
        public void Render() {
            entity.RenderEntity();
        }
///<summary> Updates which diretion player should move. - is left and + is right </summary>
        private void UpdateDirection() {
            shape.Direction.X = (moveLeft + moveRight);
        }
///<summary> Sets moveLeft to movementspeed and 0 depending on boolean
///value. Calls method to update the direction of the player after setting. </summary>
///<param name = "val"> a boolean whether player should move or not </param>.
        private void SetMoveLeft(bool val) {
            if (val) {
                moveLeft = -MOVEMENT_SPEED;
                UpdateDirection();
            } else {
                moveLeft = 0;
                UpdateDirection();
            }
        }
///<summary> Sets moveRight to positive value of MOVEMENT_SPEED or 0 and updates the direction
///accordingly to boolean value </summary>
///<param name="val"> a boolean whether player should move or not </param>
        private void SetMoveRight(bool val){
            if (val) {
                moveRight = MOVEMENT_SPEED;
                UpdateDirection();
            } else {
                moveRight = 0;
                UpdateDirection();
            }    
        }
///<summary> Moves the player </summary>
        public void Move() {
            var x = shape.Position.X;
            if ((x + shape.Direction.X) <= (1.0f - shape.Extent.X) && 
                (x + shape.Direction.X) >= 0.0f) {
                shape.Move();
            }
        }
///<returns> X-position of player </returns>
        public Vec2F getPos() {
            return shape.Position;
        }

///<returns> Extent of player in the X-axis </returns>
        public float getExtent() {
            return shape.Extent.X;
        }

///<returns> Player's Shape </returns>
        public DynamicShape getShape() {
            return shape;
        }

///<returns> Player's Entity </returns>
        public Entity getEntity(){
            return entity;
        }
///<summary> Begins movement when left or right key is pressed by calling method. </summary>
///<param name ="key"> argument is a given key-input as string </param>
        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    SetMoveLeft(true);
                    break;
                case KeyboardKey.Right:
                    SetMoveRight(true);
                    break;
                default:
                    break;
            }
        }
///<summary> stops movement or closes the game </summary>
///<param name="key"> string as a key on the keyboard </param>
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Left:
                    SetMoveLeft(false);
                    break;
                case KeyboardKey.Right:
                    SetMoveRight(false);
                    break;
                case KeyboardKey.Space:
                rand = new Random();
                if (GameRunning.infiniteMode) {
                    GameRunning.balls.AddEntity(new Ball(
                        new Vec2F(GameRunning.player.getShape().Position.X + GameRunning.player.getExtent()/2,GameRunning.player.getShape().Position.Y),
                        new Vec2F((float)rand.NextDouble()-rand.Next(2),(float)rand.NextDouble())));
                    foreach(Ball x in GameRunning.balls) {x.AlignSpeed();}
                }
                    break;
                default:
                    break;
            }
        }
    }
}