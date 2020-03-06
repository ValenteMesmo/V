using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class Camera
    {
        private const float defaultZoom = 0.4f;
        //1280 x 720
        public const int windowWidth =
            //1366
            1176
            //640
            ;
        public const int windowHeight =
            //768
            664
            //480
            ;

        public float Zoom;
        private float rotation;

        public Point Location;

        public Matrix transform;

        public Camera()
        {
            Zoom = defaultZoom;
            rotation = 0.0f;
            //Location = new Point(980, 550);
            transform = new Matrix();
        }

        public Vector2 GetWorldPosition(Vector2 position2)
        {
            return Vector2.Transform(position2, Matrix.Invert(transform));
        }

        public Vector2 GetScreenPosition(Vector2 position2)
        {
            return Vector2.Transform(position2, transform);
        }

        public void Update(GraphicsDevice graphicsDevice)
        {           
            transform =
                Matrix.CreateTranslation(-Location.X, -Location.Y, 0.0f)
                    * Matrix.CreateRotationZ(rotation)
                    * Matrix.CreateScale(
                        Zoom * (graphicsDevice.Viewport.Width / windowWidth)
                        , Zoom * (graphicsDevice.Viewport.Height / windowHeight)
                        , 1.0f)
                    * Matrix.CreateTranslation(
                        graphicsDevice.Viewport.Width * 0.5f
                        , graphicsDevice.Viewport.Height * 0.5f
                        , 0.0f);


            //var widthDiff = graphicsDevice.Viewport.Width / windowWidth;
            //var HeightDiff = graphicsDevice.Viewport.Height / windowHeight;

            //transform =
            //  Matrix.CreateTranslation(new Vector3(-Location.X, -Location.Y, 0.0f))
            //    * Matrix.CreateRotationZ(rotation)
            //    * Matrix.CreateScale(new Vector3(Zoom * widthDiff, Zoom * HeightDiff, 1))
            //    * Matrix.CreateTranslation(new Vector3(
            //        graphicsDevice.Viewport.Width * 0.5f,
            //        graphicsDevice.Viewport.Height * 0.5f, 0));
        }
    }
}
