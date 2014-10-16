using System;
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

		[Fact]
		public async void AwaitFinish_50Ticks_AwaitsLoopFinish()
		{
			var count = 0;
			var game = new Game();

			// Create and set up our test loop
			var loop = new Loop {TargetDelta = TimeSpan.FromSeconds(0.0001)};
			loop.Tick += (s, e) =>
			{
				if (count >= 50)
					loop.Stop();

				count++;
			};

			// Add and start the loop
			game.TrackLoop(loop);
			loop.Start();

			// Wait till our game object is done
			await game;

			// Make sure it waited for the loop to end
			Assert.False(loop.IsRunning);
		}

		[Fact]
		public async void StartLoop_OneLoop_TrackedAndStarted()
		{
			var game = new Game();

			// Create and set up our test loop
			var loop = game.StartLoop((s, e) => { }, TimeSpan.FromSeconds(0.0001));

			// Make sure it's indeed running and tracked
			Assert.True(loop.IsRunning);
			Assert.True(game.IsRunning);

			// Stop the loop and wait till our game object is done
			loop.Stop();
			await game;
		}

		[Fact]
		public async void StopAllLoops_MultipleLoops_StopsAll()
		{
			var game = new Game();

			// Start up our test loops
			var loopA = game.StartLoop((s, e) => { }, TimeSpan.FromSeconds(0.0001));
			var loopB = game.StartLoop((s, e) => { }, TimeSpan.FromSeconds(0.0001));

			// Stop the loops and wait till our game object is done
			game.StopAllLoops();
			await game;

			// Make sure the loops are stopped
			Assert.False(loopA.IsRunning);
			Assert.False(loopB.IsRunning);
		}
	}
}