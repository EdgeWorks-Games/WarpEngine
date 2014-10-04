using System;

namespace WarpEngine
{
	public class LoopEventArgs : EventArgs
	{
		public LoopEventArgs(TimeSpan delta)
		{
			Delta = delta;
		}

		public TimeSpan Delta { get; private set; }
	}
}