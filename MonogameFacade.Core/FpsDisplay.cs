namespace MonogameFacade
{
    public class FpsDisplay : GameObject
    {
        TextRenderer text;
        double lowValue = 60;
        int duration = 0;

        public FpsDisplay(BaseGame game)
        {
            text = new TextRenderer();
            text.Font = game.Font;
        }

        public override void Update(BaseGame game)
        {
            text.Target.X = Position.X;
            text.Target.Y = Position.Y;
            if (game.FrameCounter.CurrentFramesPerSecond < 59)
            {
                if (game.FrameCounter.CurrentFramesPerSecond < lowValue)
                    lowValue = game.FrameCounter.CurrentFramesPerSecond;
                duration = 100;
            }
            if (duration > 0)
            {
                text.Text = string.Format(@"{0:0.00}
{1:0.00}", game.FrameCounter.CurrentFramesPerSecond, lowValue);
                duration--;
            }
            else
            {
                lowValue = 60;
                text.Text = string.Format(@"{0:0.00}", game.FrameCounter.CurrentFramesPerSecond);                
            }
            game.Renderers.Add(text);
        }
    }
}
