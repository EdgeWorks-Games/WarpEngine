using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WarpEngine
{
	public sealed class Loop
	{
		private readonly Action<TimeSpan> _tickFunc;
		private bool _keepRunning;
		private Task _task;

		internal Loop(Action<TimeSpan> tickFunc, TimeSpan minimumDelta)
		{
			_tickFunc = tickFunc;
			MinimumDelta = minimumDelta;
		}

		public TimeSpan MinimumDelta { get; private set; }

		internal void Start()
		{
			_task = Task.Run(() => Run());
		}

		public void Stop()
		{
			_keepRunning = false;
		}

		public void AwaitFinish()
		{
			_task.Wait();
		}
		
		private void Run()
		{
			var stopwatch = new Stopwatch();
			var previousDelta = MinimumDelta;

			_keepRunning = true;
			while (_keepRunning)
			{
				stopwatch.Restart();

				// Run the function this loop should be ticking
				_tickFunc(previousDelta);

				// Sleep until we're at the target
				while (stopwatch.Elapsed < MinimumDelta)
				{
					if (Thread.Yield())
						continue;

					// We couldn't yield, just sleep a bit
					Thread.Sleep(0);
				}

				previousDelta = stopwatch.Elapsed;
			}
		}
	}
}