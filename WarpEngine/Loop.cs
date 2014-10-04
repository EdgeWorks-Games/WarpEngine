using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace WarpEngine
{
	public sealed class Loop
	{
		private bool _keepRunning;
		private Task _task;

		public Loop()
		{
		}

		public Loop(EventHandler<LoopEventArgs> callback, TimeSpan minimumDelta)
		{
			Tick += callback;
			MinimumDelta = minimumDelta;
		}

		public TimeSpan MinimumDelta { get; set; }

		public bool IsRunning
		{
			get { return !_task.IsCompleted; }
		}

		public event EventHandler<LoopEventArgs> Tick = (s, e) => { };

		public void Start()
		{
			_task = Task.Run(() => Run());
		}

		public void Stop()
		{
			_keepRunning = false;
		}

		public TaskAwaiter GetAwaiter()
		{
			return Task.Run(() => AwaitFinish()).GetAwaiter();
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
				Tick(this, new LoopEventArgs(previousDelta));

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