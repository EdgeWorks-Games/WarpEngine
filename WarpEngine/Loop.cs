using System;

namespace WarpEngine
{
	public sealed class Loop
	{
		private readonly Action _tick;
		private TimeSpan _accumulator;

		public Loop(Action tick, TimeSpan targetDelta)
		{
			_tick = tick;
			TargetDelta = targetDelta;

			// Apparently *4 can't be done with a TimeSpan
			AccumulationLimit = TargetDelta + TargetDelta + TargetDelta + TargetDelta;
		}

		public TimeSpan TargetDelta { get; set; }

		/// <summary>
		///     The maximum the accumulator will accumulate.
		/// </summary>
		public TimeSpan AccumulationLimit { get; set; }

		public void NotifyTimePassed(TimeSpan elapsed)
		{
			// Add the time to our internal accumulator, limiting it to our set limit
			_accumulator = WarpMath.Min(_accumulator + elapsed, AccumulationLimit);

			// Continue till our accumulator is under our target delta
			while (_accumulator >= TargetDelta)
			{
				// Remove our target delta from it
				_accumulator -= TargetDelta;

				// Run our actual tick
				_tick();
			}
		}
	}
}