using Microsoft.Xna.Framework;

namespace MonogameFacade
{
    public class Collider
    {
        public readonly GameObject Parent;
        public Rectangle Area;

        public Collider(GameObject Parent)
        {
            this.Parent = Parent;
        }

        public int RelativeX => Parent.Location.X + Area.X;
        public int RelativeY => Parent.Location.Y + Area.Y;
        public int Width => Area.Width;
        public int Height => Area.Height;

        public CollisionHandler Handler = CollisionHandler.Empty;
    }
}
