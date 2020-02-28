using System.Collections.Generic;
using System.Linq;

namespace MonogameFacade
{
    public class FrameCounter
    {
        //public long TotalFrames { get; private set; }
        //public double TotalSeconds { get; private set; }
        //public double AverageFramesPerSecond { get; private set; }
        public double CurrentFramesPerSecond { get; private set; }

        public const int MAXIMUM_SAMPLES = 10;

        //private Queue<double> _sampleBuffer = new Queue<double>();

        public void Update(double deltaTime)
        {
            CurrentFramesPerSecond = 1.0 / deltaTime;

            //_sampleBuffer.Enqueue(CurrentFramesPerSecond);

            //if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            //{
            //    _sampleBuffer.Dequeue();
            //    //AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            //}
            //else
            //{
            //    //AverageFramesPerSecond = CurrentFramesPerSecond;
            //}

            //TotalFrames = TotalFrames + 1;
            //TotalSeconds = TotalSeconds + deltaTime;
        }
    }
}
