using Microsoft.Xna.Framework;

namespace MonogameFacade
{
    public static class Log
    {
        public static string Text = "";
        public static GameObject Create()
        {
            var obj = GameObject.GetFromPool();
            obj.Location = new Point(-350, 0);
            var text = GuiTextRenderer.GetFromPool();
            obj.Renderers.Add(text);

            obj.Update = () => Update(obj, text);

            return obj;
        }

        public static void Update(GameObject obj, GuiTextRenderer text)
        {
            text.Text = Text;
        }
    }

    public static class FpsDisplay
    {
        public static GameObject Create()
        {
            var obj = GameObject.GetFromPool();
            obj.Location = new Point(-350, -350);
            var text = GuiTextRenderer.GetFromPool();
            obj.Renderers.Add(text);

            obj.Update = () => Update(obj, text);

            return obj;
        }

        public static void Update(GameObject obj, GuiTextRenderer text)
        {
            text.Text = ((int)Game.Instance.FrameCounter.CurrentFramesPerSecond).ToString();
        }
    }
}
