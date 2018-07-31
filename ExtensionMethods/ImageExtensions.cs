using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

public static class ImageExtensions
{
	public static Image Resize(this Image org, int width, int height)
	{
		return org.Resize(new Size(width, height));
	}

	public static Image Resize(this Image org, Size size)
	{
		var result = new Bitmap(size.Width, size.Height);
		using (var g = Graphics.FromImage(result))
		{
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;
			g.DrawImage(org, 0, 0, size.Width, size.Height);
		}
		return result;
	}

	public static Image Crop(this Image org, int width, int height)
	{
		return org.Crop(new Size(width, height));
	}

	public static Image Crop(this Image org, Size size)
	{
		// Clip to image size
		size = new Size(Math.Min(org.Width, size.Width), Math.Min(org.Height, size.Height));

		var point = new Point(0, 0);
		var rectangle = new Rectangle(point, size);

		return ((Bitmap)org).Clone(rectangle, org.PixelFormat);
	}

	public static Image Merge(this Image org, Image new_image)
	{
		using (Graphics gfx = Graphics.FromImage(org))
		{
			gfx.DrawImage(new_image, 0, 0, new_image.Size.Width, new_image.Size.Height);
			return org;
		}
	}
	
	public static Image MakeMonochrome(this Image org, int threshold)
	{
		Bitmap mono_image = new Bitmap(org);

		var gray_matrix = new float[][] {
				new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
				new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
				new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
				new float[] { 0,      0,      0,      1, 0 },
				new float[] { 0,      0,      0,      0, 1 }
			};


		using (var attribute = new ImageAttributes())
		using (var gfx = Graphics.FromImage(mono_image))
		{
			attribute.SetColorMatrix(new ColorMatrix(gray_matrix));
			attribute.SetThreshold((float)threshold / 255);
			gfx.DrawImage(org, new Rectangle(0, 0, org.Width, org.Height),
				0, 0, org.Width, org.Height, GraphicsUnit.Pixel, attribute);

			return mono_image;
		}
	}

	public static Image MakeGreyscale(this Image org)
	{
		Bitmap grey_image = new Bitmap(org);

		//create the grayscale ColorMatrix
		var colorMatrix = new ColorMatrix(
			new float[][]
			{
					new float[] {.3f, .3f, .3f, 0, 0},
					new float[] {.59f, .59f, .59f, 0, 0},
					new float[] {.11f, .11f, .11f, 0, 0},
					new float[] {0, 0, 0, 1, 0},
					new float[] {0, 0, 0, 0, 1}
			});

		using (var attributes = new ImageAttributes())
		using (var gfx = Graphics.FromImage(grey_image))
		{
			//set the color matrix attribute
			attributes.SetColorMatrix(colorMatrix);

			//draw the original image on the new image
			//using the grayscale color matrix
			gfx.DrawImage(org, new Rectangle(0, 0, org.Width, org.Height),
				 0, 0, org.Width, org.Height, GraphicsUnit.Pixel, attributes);

			return grey_image;
		}
	}

	public static Image Adjust(this Image org, float brightness = 1.0f, float contrast = 1.0f, float gamma = 1.0f)
	{
		Bitmap adjusted_image = new Bitmap(org);

		brightness -= 1.0f;

		// create matrix that will brighten and contrast the image
		float[][] ptsArray ={
				new float[] {contrast, 0, 0, 0, 0}, // scale red
				new float[] {0, contrast, 0, 0, 0}, // scale green
				new float[] {0, 0, contrast, 0, 0}, // scale blue
				new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
				new float[] { brightness, brightness, brightness, 0, 1}};

		using (var gfx = Graphics.FromImage(adjusted_image))
		using (var attributes = new ImageAttributes())
		{
			attributes.ClearColorMatrix();
			attributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			attributes.SetGamma(gamma, ColorAdjustType.Bitmap);
			gfx.DrawImage(org, new Rectangle(0, 0, org.Width, org.Height),
				0, 0, org.Width, org.Height, GraphicsUnit.Pixel, attributes);
		}

		return adjusted_image;
	}

	public static string ToBase64(this Image image, ImageFormat format)
	{
		using (MemoryStream memory_stream = new MemoryStream())
		{
			image.Save(memory_stream, format);

			return Convert.ToBase64String(memory_stream.ToArray());
		}
	}

	public static Attachment ToMailAttachment(this Image image, ImageFormat format)
	{
		return image.ToMailAttachment(format.GetMimeName(), format);
	}

	public static Attachment ToMailAttachment(this Image image, string file_name, ImageFormat format)
	{
		MemoryStream stream = new MemoryStream();
		image.Save(stream, format);
		stream.Position = 0;
		return new Attachment(stream, file_name, format.GetMimeName());
	}

	public static ContentType GetContentType(this Image image)
	{
		return image.RawFormat.GetContentType();
	}

	public static string GetMimeName(this Image image)
	{
		return image.RawFormat.GetMimeName();
	}

	public static MemoryStream ToDeviceIndependentBitmap(this Image image)
	{
		using (var buffer = new MemoryStream())
		{
			image.Save(buffer, ImageFormat.Bmp);

			int image_length;
			try
			{
				image_length = checked((int)(buffer.Length - 14));
			}
			catch (OverflowException e)
			{
				throw new InvalidDataException("Source image size invalid", e);
			}

			var result = new MemoryStream();
			result.Write(buffer.GetBuffer(), 14, image_length);
			return result;
		}
	}
}