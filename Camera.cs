using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MortenSurvivor
{
    public class Camera
    {

        #region Fields

        private const float screenHalfX = 960;
        private const float screenHalfY = 540;
        private Vector2 position;
        private readonly GraphicsDevice _graphicsDevice;

        #endregion
        #region Properties


        /// <summary>
        /// Property to access the posi
        /// </summary>
        public Vector2 Position
        {
            
            get => position;

            set
            {

                Vector2 newPos = new Vector2();

                switch(value.X)
                {
                    case > 3840 - screenHalfX:
                        newPos.X = 3840 - screenHalfX;
                        break;
                    case < -1920 + screenHalfX:
                        newPos.X = -1920 + screenHalfX;
                        break;
                    default:
                        newPos.X = value.X;
                        break;
                }
                switch(value.Y)
                {
                    case > 2240 - screenHalfY:
                        newPos.Y = 2240 - screenHalfY;
                        break;
                    case < -1080 + screenHalfY:
                        newPos.Y = -1080 + screenHalfY;
                        break;
                    default:
                        newPos.Y = value.Y;
                        break;
                }

                position = newPos;

            }

        }

        /// <summary>
        /// Used to set the zoomlevel of the viewport (scaled float)
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        /// Used to rotate the camera
        /// </summary>
        public float Rotation { get; set; }

        #endregion
        #region Constuctor

        /// <summary>
        /// Constructor for the camera viewport
        /// </summary>
        /// <param name="graphicsDevice">Defines which graphicsdevice to get viewport parameters from</param>
        /// <param name="position">Defines starting position of the viewport</param>
        public Camera(GraphicsDevice graphicsDevice, Vector2 position)
        {
            _graphicsDevice = graphicsDevice;
            Position = position;
            Zoom = 1f; 
            Rotation = 0.0f;
        }

        #endregion
        #region Methods

        /// <summary>
        /// Transforms the cameraviewport to return value
        /// </summary>
        /// <returns>Center of screen location</returns>
        public Matrix GetTransformation()
        {
            var screenCenter = new Vector3(_graphicsDevice.Viewport.Width / 2f, _graphicsDevice.Viewport.Height / 2f, 0);

            return Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1) *
                   Matrix.CreateTranslation(screenCenter);
        }

        #endregion

    }
}
