using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameFacade
{
    public class Camera
    {
        public Point Location;
        public Matrix Transform;
        protected float Rotation;
        public float Zoom;
        private const float VIRTUAL_WIDTH = 1280;
        private const float VIRTUAL_HEIGHT = 720;

        public Camera()
        {
            Zoom = 1.0f;
            Rotation = 0.0f;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Transform =
                Matrix.CreateTranslation(-Location.X, -Location.Y, 0)
                * Matrix.CreateRotationZ(Rotation)
                * Matrix.CreateScale(
                    Zoom * (graphicsDevice.Viewport.Width / VIRTUAL_WIDTH)
                    , Zoom * (graphicsDevice.Viewport.Height / VIRTUAL_HEIGHT)
                    , 1)
                * Matrix.CreateTranslation(
                    graphicsDevice.Viewport.Width * 0.5f
                    , graphicsDevice.Viewport.Height * 0.5f
                    , 0);

            return Transform;
        }

        public Point GetWorldPosition(Point position) =>
            GetWorldPosition(position.ToVector2());

        public Point GetWorldPosition(Vector2 position) =>
            Vector2.Transform(
                position
                , Matrix.Invert(Transform)
            ).ToPoint();

        public Vector2 GetScreenLocation(Vector2 position)
        {
            return Vector2.Transform(position, Transform);
        }
    }
}
