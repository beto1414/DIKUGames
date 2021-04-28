using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;
using System.IO;
using System.Collections.Generic;


namespace Breakout {
    public class Player : IGameEventProcessor {

        private Entity entity;
        private DynamicShape shape;
        private GameEventBus eventBus;
        private IBaseImage playerShotImage;

        private const float MOVEMENT_SPEED = 0.01f;
        private float moveRight = 0.0f;  
        private float moveLeft = 0.0f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            
            eventBus = new GameEventBus();

            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
            eventBus.Subscribe(GameEventType.InputEvent, this);

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
        public float getPos() {
            return shape.Position.X;
        }

        ///<summary> Begins movement when left or right key is pressed by calling method. </summary>
        ///<param name ="key"> argument is a given key-input as string </param>
        public void KeyPress(string key) {
            switch (key) {
                case "KEY_LEFT":
                    SetMoveLeft(true);
                    break;
                case "KEY_RIGHT":
                    SetMoveRight(true);
                    break;
                default:
                    break;
            }
        }

        ///<summary> stops movement or closes the game </summary>
        ///<param name="key"> string as a key on the keyboard </param>
        public void KeyRelease(string key) {
            switch (key) {
                // case "KEY_ESCAPE":
                //     eventBus.RegisterEvent(
                //     GameEventFactory<object>.CreateGameEventForAllProcessors(
                //     GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                //     break;
                case "KEY_LEFT":
                    SetMoveLeft(false);
                    break;
                case "KEY_RIGHT":
                    SetMoveRight(false);
                    break;
                default:
                    break;
                
            }
        }

        ///<summary> Check if a key is pressed or released and acts accordingly 
        ///using KeyPress or KeyRelease</summary>
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.StringArg1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                default:
                    break;

            }
        }
    }
}