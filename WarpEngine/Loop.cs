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
			AccumulationLimit = TimeSpan.FromSeconds(0.5);
		}

		public Loop(EventHandler<LoopEventArgs> callback, TimeSpan targetDelta)
		{
			Tick += callback;
			TargetDelta = targetDelta;

			// Apparently *4 can't be done with a TimeSpan
			AccumulationLimit = TargetDelta + TargetDelta + TargetDelta + TargetDelta;
		}

		public Loop(EventHandler<LoopEventArgs> callback, TimeSpan targetDelta, EventHandler startCallback)
			:this(callback, targetDelta)
		{
			LoopStart += startCallback;
		}

		public TimeSpan TargetDelta { get; set; }

		/// <summary>
		///     The maximum the accumulator will accumulate.
		/// </summary>
		public TimeSpan AccumulationLimit { get; set; }

		public bool IsRunning
		{
			get { return !_task.IsCompleted; }
		}

		public event EventHandler<LoopEventArgs> Tick = (s, e) => { };

		public event EventHandler LoopStart = (s, e) => { };

		public void Start()
		{
			_keepRunning = true;
			_task = Task.Run(() => Run());
		}

		public void Stop()
		{
			_keepRunning = false;
		}

		public TaskAwaiter GetAwaiter()
		{
			return _task.GetAwaiter();
		}

		public void AwaitFinish()
		{
			_task.Wait();
		}

		private void Run()
		{
			LoopStart(this, EventArgs.Empty);

			// TODO: Make the loop adjust the target delta dynamically to adjust for performance issues.
			// Right now it will just run slower, since AccumulationLimit simply clamps the target.

			var accumulator = new TimeSpan();
			var stopwatch = new Stopwatch();

			while (_keepRunning)
			{
				// Increase our accumulator with the elapsed time
				accumulator += stopwatch.Elapsed;
				stopwatch.Restart();

				// Check if the accumulator has gone over our target
				if (accumulator > TargetDelta)
				{
					// It has, execute a tick
					Tick(this, new LoopEventArgs(TargetDelta));
					accumulator -= TargetDelta;
				}
				else
				{
					// It hasn't yet, so we need to calculate how long till it will
					var targetElapsed = TargetDelta - accumulator;

					// And wait till that point
					while (stopwatch.Elapsed < targetElapsed)
					{
						// Use yield to give the rest of our thread's time to another thread
						if (Thread.Yield())
							continue;

						// We couldn't yield, sleep a bit instead
						Thread.Sleep(0);
					}
				}

				// Limit accumulator to the set limit
				if (accumulator > AccumulationLimit)
				{
					accumulator = AccumulationLimit;
				}
			}
		}
	}
}