using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImbaControls
{
	public class TimedToolTip : Component
	{
		private const int DefaultDuration = 5000;

		public string Text { get; set; }
		public Control Window { get; set; }
		public long Duration { get; set; }
		private HandyTimer.Timer m_timer;
		private ToolTip m_tool_tip;

		public TimedToolTip(Control window)
			: this(window, "", DefaultDuration)
		{
		}

		public TimedToolTip(Control window, string text)
			: this(window, text, DefaultDuration)
		{
		}

		public TimedToolTip(Control window, long duration)
			: this(window, "", duration)
		{
		}

		public TimedToolTip(Control window, string text, long duration)
		{
			Text = text;
			Window = window ?? throw new ArgumentException();
			Duration = duration;
			m_tool_tip = new ToolTip();
			m_timer = new HandyTimer.Timer((state) =>
			{
				try
				{
					if (Window.InvokeRequired)
					{
						Window.Invoke(new Action(() => m_tool_tip.Hide(window)));
						return;
					}
					m_tool_tip.Hide(window);
				}
				catch (ObjectDisposedException) { }
			});
		}

		public void Show()
		{
			this.Show(Text, Duration, new Point(0, 0));
		}

		public void Show(string text)
		{
			this.Show(text, Duration, new Point(0, 0));
		}

		public void Show(long duration)
		{
			this.Show(Text, duration, new Point(0, 0));
		}

		public void Show(Point location)
		{
			this.Show(Text, Duration, location);
		}

		public void Show(string text, long duration)
		{
			this.Show(text, duration, new Point(0, 0));
		}

		public void Show(string text, Point location)
		{
			this.Show(text, Duration, location);
		}

		public void Show(long duration, Point location)
		{
			this.Show(Text, duration, location);
		}

		public void Show(string text, long duration, Point location)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return;
			}
			m_tool_tip.Show(text, Window, location);
			m_timer.StartSingle(duration);
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
				if (m_timer != null)
				{
					m_timer.Dispose();
					m_timer = null;
				}
				if (m_tool_tip != null)
				{
					m_tool_tip.Dispose();
					m_tool_tip = null;
				}
			}

			disposed = true;

			base.Dispose(disposing);
		}

		#endregion
	}
}