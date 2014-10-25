using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace WarpEngine.Basic2D.Graphics
{
	public sealed class GlTexture : IDisposable
	{
		private readonly int _texture;

		public GlTexture(string file)
		{
			using (var bitmap = new Bitmap(file))
			{

				// Save some metadata
				Width = bitmap.Width;
				Height = bitmap.Height;

				// Load the data from the bitmap
				var textureData = bitmap.LockBits(
					new Rectangle(0, 0, bitmap.Width, bitmap.Height),
					ImageLockMode.ReadOnly,
					PixelFormat.Format32bppArgb);

				// Generate and bind a new OpenGL texture
				_texture = GL.GenTexture();
				GL.BindTexture(TextureTarget.Texture2D, _texture);

				// Configure the texture
				GL.TexParameter(TextureTarget.Texture2D,
					TextureParameterName.TextureMinFilter, (int)(TextureMinFilter.Nearest));
				GL.TexParameter(TextureTarget.Texture2D,
					TextureParameterName.TextureMagFilter, (int)(TextureMinFilter.Nearest));

				// Load the texture
				GL.TexImage2D(
					TextureTarget.Texture2D,
					0, // level
					PixelInternalFormat.Rgba,
					bitmap.Width, bitmap.Height,
					0, // border
					OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
					PixelType.UnsignedByte,
					textureData.Scan0);

				// Free the data since we won't need it anymore
				bitmap.UnlockBits(textureData);
				GL.BindTexture(TextureTarget.Texture2D, 0);
			}
		}

		public int Width { get; private set; }
		public int Height { get; private set; }

		public Size Size
		{
			get { return new Size(Width, Height); }
		}

		/// <summary>
		///     Deletes the texture.
		/// </summary>
		public void Dispose()
		{
			GL.DeleteTexture(_texture);
			GC.SuppressFinalize(this);
		}

		~GlTexture()
		{
			Trace.TraceWarning("[RESOURCE LEAK] Texture finalizer invoked!");
			Dispose();
		}

		public void Bind()
		{
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, _texture);
		}

		public static void ClearBind()
		{
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}
	}
}