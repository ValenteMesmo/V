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
                    a.Handler.Left(a, b);
                    b.Handler.Right(b, a);
                }
                else if (a.Right() - b.Right() < 0)
                {
                    a.Handler.Right(a, b);
                    b.Handler.Left(b, a);
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
                    a.Handler.Top(a, b);
                    b.Handler.Bot(b, a);
                }
                else if (a.Bottom() - b.Bottom() < 0)
                {
                    a.Handler.Bot(a, b);
                    b.Handler.Top(b, a);
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
