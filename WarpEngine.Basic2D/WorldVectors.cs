
namespace WarpEngine.Basic2D
{
	public struct WorldPosition
	{
		private readonly float _x;
		private readonly float _y;

		public WorldPosition(float x, float y)
		{
			_x = x;
			_y = y;
		}

		public float X { get { return _x; } }
		public float Y { get { return _y; } }

		public static WorldPosition operator +(WorldPosition left, WorldDistance right)
		{
			return new WorldPosition(left.X + right.X, left.Y + right.Y);
		}
	}
	public struct WorldDistance
	{
		private readonly float _x;
		private readonly float _y;

		public WorldDistance(float x, float y)
		{
			_x = x;
			_y = y;
		}

		public float X { get { return _x; } }
		public float Y { get { return _y; } }

		public static WorldDistance operator +(WorldDistance left, WorldDistance right)
		{
			return new WorldDistance(left.X + right.X, left.Y + right.Y);
		}
	}
}