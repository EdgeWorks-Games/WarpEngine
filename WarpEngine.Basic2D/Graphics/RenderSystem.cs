namespace WarpEngine.Basic2D.Graphics
{
	public static class RenderSystem
	{
		public static EntitySystem Create()
		{
			return new EntitySystem
			{
				Filter = e => e.Has<TransformComponent>() && e.Has<SpriteComponent>(),
				Processor = Process
			};
		}

		private static void Process(Entity entity)
		{
			var transform = entity.Get<TransformComponent>();
			var sprite = entity.Get<SpriteComponent>();
		}
	}
}