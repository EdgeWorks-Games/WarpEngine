namespace WarpEngine.Basic2D.Graphics
{
	public class SpriteComponent : IEntityComponent
	{
		public Sprite Sprite { get; set; }
		public WorldDistance Offset { get; set; }
		public WorldDistance Size { get; set; }
	}
}