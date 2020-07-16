using Microsoft.Xna.Framework;

namespace MonogameFacade
{
    public static class Log
    {
        public static string Text { get => text.Text; set => text.Text = value; }

        private static GuiTextRenderer text;
        public static GameObject Create()
        {
            var obj = GameObject.GetFromPool();
            obj.Location = new Point(-350, 0);
            text = GuiTextRenderer.GetFromPool();
            obj.Renderers.Add(text);

            obj.Update = () => Update(obj, text);

            return obj;
        }

        public static void Update(GameObject obj, GuiTextRenderer text)
        {
            text.Text = "";
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

            obj.Update = () => Update(text, Game.Instance.CurrentFramesPerSecond);

            return obj;
        }

        public static void Update(GuiTextRenderer text, double fps)
        {
            text.Text = ((int)fps).ToString();
        }
    }
}
