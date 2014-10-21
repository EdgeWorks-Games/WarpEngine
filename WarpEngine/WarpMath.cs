using System;

namespace WarpEngine
{
	public static class WarpMath
	{
		public static TimeSpan Min(TimeSpan val1, TimeSpan val2)
		{
			return val1 < val2 ? val1 : val2;
		}
	}
}