using WarpEngine;
using WarpEngine.Basic2D.Graphics;

namespace Warpcore
{
	public static class TempDisposerSystem
	{
		public static EntitySystem Create()
		{
			return new EntitySystem
			{
				Filter = e => e.Has<SpriteComponent>(),
				Processor = Process
			};
		}

		private static void Process(Entity entity)
		{
			var sprite = entity.Get<SpriteComponent>();

			sprite.Sprite.Dispose();
		}
	}
}