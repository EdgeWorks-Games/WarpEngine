using Xunit;

namespace WarpEngine.Tests
{
	public class GameTests
	{
		[Fact]
		public async void GetIsRunning_DuringLoopRunning_ReturnsTrue()
		{
			var ranCheck = false;
			var game = new Game();

			// Create and set up our test loop
			var loop = new Loop();
			loop.Tick += (s, e) =>
			{
				// Run the actual test
				ranCheck = true;
				Assert.True(game.IsRunning);

				// Make sure it isn't run again
				loop.Stop();
			};

			// Add and start the loop
			game.TrackLoop(loop);
			loop.Start();

			// Wait till our game object is done and make sure the test ran
			await game;
			Assert.True(ranCheck, "Test didn't run.");
		}

		[Fact]
		public async void GetIsRunning_AfterLoopRunning_ReturnsFalse()
		{
			var game = new Game();

			// Create and set up our test loop
			var loop = new Loop();
			loop.Tick += (s, e) => loop.Stop();

			// Add and start the loop
			game.TrackLoop(loop);
			loop.Start();

			// Wait till the loop's done running
			loop.AwaitFinish();

			// Run the actual test
			Assert.False(game.IsRunning);

			// Wait till our game object is done
			await game;
		}
	}
}