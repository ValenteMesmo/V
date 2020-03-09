using MonogameFacade;

namespace V
{
    public static class Gravity
    {
        public static void Apply(GameObject obj)
        {
            if (obj.Velocity.Y < 500)
                obj.Velocity.Y = obj.Velocity.Y + 5;
        }
    }
}
