
using System;

namespace WarpEngine
{
	public struct ScreenPosition
	{
		private readonly float _x;
		private readonly float _y;

		public ScreenPosition(float x, float y)
		{
			_x = x;
			_y = y;
		}

		public float X { get { return _x; } }
		public float Y { get { return _y; } }

	}
}