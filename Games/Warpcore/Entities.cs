using WarpEngine;
using WarpEngine.Basic2D;
using WarpEngine.Basic2D.Graphics;

namespace Warpcore
{
	internal static class Entities
	{
		public static Entity CreatePlayerEntity()
		{
			var entity = new Entity
			{
				Components =
				{
					new TransformComponent(),
					new SpriteComponent
					{
						Sprite = new Sprite("Sprites/Human.png")
					},
					new PlayerComponent
					{
						Speed = 1
					}
				}
			};

			return entity;
		}
	}
}