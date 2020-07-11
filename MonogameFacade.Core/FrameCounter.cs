namespace MonogameFacade
{
    public class FrameCounter
    {
        //public long TotalFrames { get; private set; }
        //public double TotalSeconds { get; private set; }
        //public double AverageFramesPerSecond { get; private set; }
        public double CurrentFramesPerSecond;
        
        //public const int MAXIMUM_SAMPLES = 120; 

        //private Queue<double> _sampleBuffer = new Queue<double>();

        public void Update(double deltaTime)
        {
            CurrentFramesPerSecond = CurrentFramesPerSecond * 0.99 + ((1 / deltaTime)) * 0.01;

            //CurrentFramesPerSecond = 1.0 / deltaTime;

            //_sampleBuffer.Enqueue(CurrentFramesPerSecond);

            //if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            //{
            //    _sampleBuffer.Dequeue();
            //    AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            //}
            //else
            //{
            //    AverageFramesPerSecond = CurrentFramesPerSecond;
            //}

            //TotalFrames = TotalFrames + 1;
            //TotalSeconds = TotalSeconds + deltaTime;
        }
    }
}
