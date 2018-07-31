using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;

public static class ImageFormatExtensions
{
	public static ContentType GetContentType(this ImageFormat format)
	{
		return new ContentType(format.GetMimeName());
	}

	public static string GetMimeName(this ImageFormat format)
	{
		ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
		return codecs.First(codec => codec.FormatID == format.Guid).MimeType;
	}
}