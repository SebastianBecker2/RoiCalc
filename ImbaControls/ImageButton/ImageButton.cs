using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImbaControls.ImageButton
{
	public class ImageButton : PictureBox
	{
		private enum ImageStatus
		{
			Pressed,
			Highlighted,
			Nothing
		}

		private readonly static float HightlightBrightnessFactor = 0.2f;
		private readonly static float PressedBrightnessFactor = -0.3f;

		private ImageStatus Status = ImageStatus.Nothing;
		private Image OriginalImage = null;

		protected override void OnMouseEnter(EventArgs e)
		{
			HighlightImage();
			base.OnMouseEnter(e);
		}

		protected override void OnMouseHover(EventArgs e)
		{
			if (!ClientRectangle.Contains(PointToClient(MousePosition)))
			{
				HighlightImage();
			}
			base.OnMouseHover(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			DeHighlightImage();
			base.OnMouseLeave(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			PressImage();
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (ClientRectangle.Contains(PointToClient(MousePosition)))
			{
				HighlightImage();
			}
			else
			{
				UnPressImage();
			}
			base.OnMouseUp(e);
		}

		protected override void OnClick(EventArgs e)
		{
			if (ClientRectangle.Contains(PointToClient(MousePosition)))
			{
				HighlightImage();
			}
			else
			{
				UnPressImage();
			}
			base.OnClick(e);
		}
		
		private void HighlightImage()
		{
			if (Status == ImageStatus.Highlighted)
			{
				return;
			}

			if (Image == null)
			{
				return;
			}

			if (OriginalImage == null)
			{
				OriginalImage = new Bitmap(Image);
			}

			Image = OriginalImage.Adjust(1.0f + HightlightBrightnessFactor);
			Status = ImageStatus.Highlighted;
		}

		private void DeHighlightImage()
		{
			if (Status != ImageStatus.Highlighted)
			{
				return;
			}

			if (OriginalImage == null)
			{
				return;
			}

			Image = OriginalImage;
			Status = ImageStatus.Nothing;
		}

		private void PressImage()
		{
			if (Status == ImageStatus.Pressed)
			{
				return;
			}

			if (OriginalImage == null)
			{
				if (Image == null)
				{
					return;
				}
				OriginalImage = Image;
			}

			Image = OriginalImage.Adjust(1.0f + PressedBrightnessFactor);
			Status = ImageStatus.Pressed;
		}

		private void UnPressImage()
		{
			if (Status != ImageStatus.Pressed)
			{
				return;
			}

			if (OriginalImage == null)
			{
				return;
			}

			Image = OriginalImage;
			Status = ImageStatus.Nothing;
		}
	}
}
