namespace MonogameFacade.Core.Systems
{
    public static class HorizontalMovement
    {
        public static void Update(
            GameObject obj
            , InputKeeper inputs)
        {
            if (inputs.Left)
                obj.Velocity.X = -120;
            else if (inputs.Right)
                obj.Velocity.X = 120;
            else
                obj.Velocity.X = 0;
        }
    }

    public static class JumpsUsingInput
    {
        public static void Update(
            GameObject obj
            , InputKeeper actionInput
            , bool grounded)
        {
            if (grounded && actionInput.Down)
                obj.Velocity.Y = -510;
        }
    }
}
