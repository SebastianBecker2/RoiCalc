using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImbaControls
{
	public class Splitter : SplitContainer
	{
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Point[] points = new Point[3];
			if (Orientation == Orientation.Horizontal)
			{
				points[0] = new Point((Width / 2), SplitterDistance + (SplitterWidth / 2));
				points[1] = new Point(points[0].X - 10, points[0].Y);
				points[2] = new Point(points[0].X + 10, points[0].Y);
			}
			else
			{
				points[0] = new Point(SplitterDistance + (SplitterWidth / 2), (Height / 2));
				points[1] = new Point(points[0].X, points[0].Y - 10);
				points[2] = new Point(points[0].X, points[0].Y + 10);
			}

			foreach (Point p in points)
			{
				p.Offset(-2, -2);
				e.Graphics.FillEllipse(SystemBrushes.ControlDark,
						new Rectangle(p, new Size(3, 3)));

				p.Offset(1, 1);
				e.Graphics.FillEllipse(SystemBrushes.ControlLight,
						new Rectangle(p, new Size(3, 3)));
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			Refresh();
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);

			Panel1.Focus();
			Refresh();
		}
	}
}