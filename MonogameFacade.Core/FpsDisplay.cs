namespace MonogameFacade
{
    public class FpsDisplay : GameObject
    {
        TextRenderer text = null;
               
        public FpsDisplay(BaseGame game)
        {
            text = new TextRenderer();
            text.Font = game.Font;            
            Renderers.Add(text);
        }
        
        public override void Update(BaseGame game)
        {
            Location = game.Camera.Location;
            text.Text = ((int)game.FrameCounter.CurrentFramesPerSecond).ToString();
        }
    }
}
