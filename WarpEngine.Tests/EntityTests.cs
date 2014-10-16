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
	}
}