namespace MonogameFacade
{
    public static class Identifier
    {
        public const int Undefined = 0;
        public const int Block = 1;
    }

    public static class StopsWhenHitingBlocks
    {
        public static void Bot(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                //TODO: - offsetY
                Source.Parent.Location.Y = target.Top() - Source.Height - 1;
                Source.Parent.Velocity.Y = 0;
            }
        }

        public static void Left(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                Source.Parent.Location.X = target.Right() + 1 - Source.Area.X;
                Source.Parent.Velocity.X = 0;
            }
        }

        public static void Right(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                Source.Parent.Location.X = target.Left() - Source.Area.X - Source.Width - 1;
                Source.Parent.Velocity.X = 0;
            }
        }

        public static void Top(Collider Source, Collider target)
        {
            if (target.Parent.Identifier == Identifier.Block)
            {
                //TODO: - offsetY
                Source.Parent.Location.Y =
                    target.Bottom()
                    + 1;
                Source.Parent.Velocity.Y = 0;
            }
        }
    }
}
