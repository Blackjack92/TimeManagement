namespace TimeManager.ManageTodos.EventArgs
{
    public enum TimerState
    {
        Tracking,
        Stopped,
        Reset
    }

    public static class TimerStateExtensions
    {
        public static TimerState ToggleState(this TimerState state)
        {
            if (state == TimerState.Stopped || state == TimerState.Reset)
            {
                return TimerState.Tracking;
            }

            return TimerState.Stopped;
        }
    }
}
