using WarpEngine;

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
					//new SpriteComponent(playerSprite),
					new PlayerComponent()
				}
			};

			return entity;
		}
	}
}