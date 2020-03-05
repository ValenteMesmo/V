using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class Camera
    {
        private const float defaultZoom = 0.4f;

        private float zoom;
        private float rotation;

        public Vector2 position;

        public Matrix transform;

        public Camera()
        {
            zoom = defaultZoom;
            rotation = 0.0f;
            position = new Vector2(980.0f, 550.0f);
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
                Matrix.CreateTranslation(-position.X, -position.Y, 0.0f)
                    * Matrix.CreateRotationZ(rotation)
                    * Matrix.CreateScale(
                        zoom * (graphicsDevice.Viewport.Width / 800)//1176.0f;
                        , zoom * (graphicsDevice.Viewport.Height / 480)//664.0f;
                        , 1.0f)
                    * Matrix.CreateTranslation(
                        graphicsDevice.Viewport.Width * 0.5f
                        , graphicsDevice.Viewport.Height * 0.5f
                        , 0.0f);
        }
    }
}
