namespace MonogameFacade
{
    public class FpsDisplay : GameObject
    {
        TextRenderer text = null;
               
        public FpsDisplay(Game game)
        {
            text = new TextRenderer();
            text.Font = game.Font;            
            Renderers.Add(text);
        }
        
        public override void Update(Game game)
        {
            Location = game.Camera.Location;
            text.Text = ((int)game.FrameCounter.CurrentFramesPerSecond).ToString();
        }
    }
}
