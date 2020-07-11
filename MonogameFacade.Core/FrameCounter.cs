namespace MonogameFacade
{
    public class FrameCounter
    {
        public double CurrentFramesPerSecond = 60;

        public void Update(double deltaTime)
        {
            CurrentFramesPerSecond = CurrentFramesPerSecond * 0.99 + ((1 / deltaTime)) * 0.01;
        }
    }
}
