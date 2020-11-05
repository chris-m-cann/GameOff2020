using UnityEngine;

namespace Util
{
    public class Timer
    {
        public bool IsRunning { get; private set; }

        private float _elapsedTime = 0f;
        
        public Timer()
        {
            IsRunning = false;
        }

        public bool Elapsed { get { return IsRunning && _elapsedTime <= Time.time; } private set { } }

        public void Start(float timeout)
        {
            IsRunning = true;
            _elapsedTime = Time.time + timeout;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}