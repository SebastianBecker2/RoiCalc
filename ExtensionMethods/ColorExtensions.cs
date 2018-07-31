using System.Drawing;

public static class ColorExtensions
{
	public static Color GrayScale(this Color color)
	{
		int gray_scale = (int)((color.R * .3) + (color.G * .59) + (color.B * .11));
		return Color.FromArgb(gray_scale, gray_scale, gray_scale);
	}

	public static Color Flatten(this Color base_color, Color back_color)
	{
		return base_color.Flatten(back_color, 50);
	}

	public static Color Flatten(this Color base_color, Color back_color, int percent)
	{
		int red = base_color.R;
		int green = base_color.G;
		int blue = base_color.B;

		if (red < back_color.R)
		{
			red += (int)((back_color.R - red) * percent / 100);
		}
		else
		{
			red -= (int)((red - back_color.R) * percent / 100);
		}

		if (green < back_color.G)
		{
			green += (int)((back_color.G - green) * percent / 100);
		}
		else
		{
			green -= (int)((green - back_color.G) * percent / 100);
		}

		if (blue < back_color.B)
		{
			blue += (int)((back_color.B - blue) * percent / 100);
		}
		else
		{
			blue -= (int)((blue - back_color.B) * percent / 100);
		}

		return Color.FromArgb(red, green, blue);
	}
}