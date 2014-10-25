using Xunit;

namespace WarpEngine.Tests
{
	public class EntityTests
	{
		[Fact]
		public void Has_MatchingComponent_ReturnsTrue()
		{
			var entity = new Entity();
			entity.Components.Add(new ComponentA());

			Assert.True(entity.Has<ComponentA>());
		}

		[Fact]
		public void Has_ClashingComponent_ReturnsFalse()
		{
			var entity = new Entity();
			entity.Components.Add(new ComponentB());

			Assert.False(entity.Has<ComponentA>());
		}

		[Fact]
		public void Get_MatchingComponent_ReturnsComponent()
		{
			var entity = new Entity();
			var component = new ComponentA();
			entity.Components.Add(component);

			Assert.Same(component, entity.Get<ComponentA>());
		}

		[Fact]
		public void Get_ClashingComponent_ReturnsNull()
		{
			var entity = new Entity();
			entity.Components.Add(new ComponentB());

			Assert.Null(entity.Get<ComponentA>());
		}

		[Fact]
		public void Remove_MatchingComponent_RemovesComponent()
		{
			var entity = new Entity();

			entity.Components.Add(new ComponentA());
			entity.Components.Add(new ComponentB());

			entity.Remove<ComponentA>();

			Assert.Null(entity.Get<ComponentA>());
			Assert.NotNull(entity.Get<ComponentB>());
		}

		[Fact]
		public void Remove_ClashingComponent_DoesNothing()
		{
			var entity = new Entity();

			entity.Components.Add(new ComponentB());

			entity.Remove<ComponentA>();

			Assert.NotNull(entity.Get<ComponentB>());
		}
	}
}