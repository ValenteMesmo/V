namespace MonogameFacade
{
    public static class FpsDisplay
    {
        public static GameObject Create()
        {
            var obj = GameObject.GetFromPool();
            var text = TextRenderer.GetFromPool();
            obj.Renderers.Add(text);

            obj.Update = () => Update(obj, text);

            return obj;
        }

        public static void Update(GameObject obj, TextRenderer text)
        {
            obj.Location = Game.Instance.Camera.Location;
            text.Text = ((int)Game.Instance.FrameCounter.CurrentFramesPerSecond).ToString();
        }
    }
}
