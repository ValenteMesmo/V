namespace MonogameFacade
{
    public static class ColliderExtensions
    {
        public static void IsCollidingH(
            this Collider a,
            Collider b)
        {
            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Right() - b.Right() > 0)
                {
                    a.LeftCollisionHandler(a, b);
                    b.RightCollisionHandler(b, a);
                }
                else if (a.Right() - b.Right() < 0)
                {
                    a.RightCollisionHandler(a, b);
                    b.LeftCollisionHandler(b, a);
                }
            }
        }

        public static void IsCollidingV(
           this Collider a,
            Collider b)
        {

            if (a.Left() <= b.Right()
                && b.Left() <= a.Right()
                && a.Top() <= b.Bottom()
                && b.Top() <= a.Bottom())
            {
                if (a.Bottom() - b.Bottom() > 0)
                {
                    a.TopCollisionHandler(a, b);
                    b.BotCollisionHandler(b, a);
                }
                else if (a.Bottom() - b.Bottom() < 0)
                {
                    a.BotCollisionHandler(a, b);
                    b.TopCollisionHandler(b, a);
                }
            }
        }

        public static int Left(this Collider a)
        {
            return a.RelativeX;
        }

        public static int Right(this Collider a)
        {
            return a.RelativeX + a.Width;
        }

        public static int Top(this Collider a)
        {
            return a.RelativeY;
        }

        public static int Bottom(this Collider a)
        {
            return a.RelativeY + a.Height;
        }

        public static float CenterX(this Collider collider)
        {
            return (collider.Left() + collider.Right()) * 0.5f;
        }

        public static float CenterY(this Collider collider)
        {
            return (collider.Top() + collider.Bottom()) * 0.5f;
        }
    }
}
