using System;
using System.Timers;
using TimeManager.ManageTodos.EventArgs;
using PostSharp.Patterns.Model;

namespace TimeManager.ManageTodos.Services
{
    [NotifyPropertyChanged]
    public class TimerService
    {
        public event EventHandler<TimeTrackedEventArgs> TimeTracked;

        private Timer timer;
        private DateTime startTime;
        private DateTime endTime;

        public TimerState TimerState { get; private set; }
        public TimeSpan CurrentSpan { get; private set; }

        public TimerService()
        {
            CurrentSpan = new TimeSpan();
            TimerState = TimerState.Stopped;

            timer = new Timer(1000);
            timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            CurrentSpan = e.SignalTime.Subtract(startTime);
        }

        public void ToggleStartStop()
        {
            // Toggle tracking
            if (TimerState == TimerState.Stopped || TimerState == TimerState.Reset)
            {
                // Same behavior for reset and stop
                startTime = DateTime.Now;
                timer.Start();
            }
            else
            {
                endTime = DateTime.Now;
                timer.Stop();

                // Call event that the time was tracked
                OnTimeTracked();
            }

            TimerState = TimerState.ToggleState();
        }

        public void Reset()
        {
            // Stop tracking and reset time
            //TimerState = TimerState.Reset;
            //CurrentSpan = new TimeSpan();
        }

        private void OnTimeTracked()
        {
            if (TimeTracked == null) { return; }

            TimeTracked(this, new TimeTrackedEventArgs(startTime, endTime));
        }
    }
}
