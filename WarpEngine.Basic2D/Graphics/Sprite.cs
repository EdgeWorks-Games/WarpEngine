using System;

namespace WarpEngine.Basic2D.Graphics
{
	public sealed class Sprite : IDisposable
	{
		private readonly GlTexture _texture;

		public Sprite(string file)
		{
			// TODO: Make the renderer create textures internally rather than here.
			_texture = new GlTexture(file);
		}

		public void Dispose()
		{
			_texture.Dispose();
		}
	}
}