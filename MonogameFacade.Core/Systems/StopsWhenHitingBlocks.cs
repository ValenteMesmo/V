namespace MonogameFacade
{
    public class StopsWhenHitingBlocks : CollisionHandler
    {
        public override void Bot(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                //TODO: - offsetY
                Source.Parent.Location.Y = target.Top() - Source.Height - 1;
                Source.Parent.Velocity.Y = 0;
            }
        }

        public override void Left(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Location.X = target.Right() + 1 - Source.Area.X;
                Source.Parent.Velocity.X = 0;
            }
        }

        public override void Right(Collider Source, Collider target)
        {
            if (target.Parent is Block)
            {
                Source.Parent.Location.X = target.Left() - Source.Area.X - Source.Width - 1;
                Source.Parent.Velocity.X = 0;
            }
        }

        public override void Top(Collider Source, Collider target)
        {
            if (target.Parent is Block)
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
