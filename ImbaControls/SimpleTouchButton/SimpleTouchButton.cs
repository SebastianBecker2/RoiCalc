using System;
using System.Windows.Forms;

namespace ImbaControls
{
	public class SimpleTouchButton : Button
	{
		public SimpleTouchButton()
		{
			MouseDown += MouseDownHandler;
		}

		private void MouseDownHandler(object sender, EventArgs e)
		{
			OnClick(e);
		}

		#region Disposing

		private bool disposed;

		protected override void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				MouseDown -= MouseDownHandler;
			}

			disposed = true;

			base.Dispose(disposing);
		}

		#endregion
	}
}