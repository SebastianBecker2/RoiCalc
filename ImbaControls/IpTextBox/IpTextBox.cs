using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ImbaControls.IpTextBox
{
	public class IpTextBox : TextBox
	{
		#region Members

		private readonly static int MinNameLength = 3;

		// Single labels may contain up to 63 characters
		// But the full domain name must not exceed 253 characters
		// We don't check single labes. shouldn't be necessary
		private readonly static int MaxNameLength = 253;

		private Color? BaseForeColor { get; set; }

		// Only period, hyphens, 0-9, a-z and A-Z are valid characters
		// IDNA is not supported
		private HashSet<char> ValidChars { get; set; }

		// Replace ResolveRunning and ResolveSuccessful with an enum
		public bool ResolveRunning
		{
			get { return m_resolve_running; }
			private set
			{
				if (m_resolve_running && !value)
				{
					OnResolveFinished();
				}
				else if (!m_resolve_running && value)
				{
					OnResolveStarted();
				}
				m_resolve_running = value;
			}
		}

		private bool m_resolve_running;
		public bool ResolveSuccessful { get; protected set; }

		[Browsable(true)]
		[DefaultValue(typeof(Color), "Red")]
		[Category("Appearance")]
		[Description("Sets the color that inidicates that the name " +
			"or address could not be resolved")]
		public Color UnresolveableColor
		{
			get { return m_unresolveable_color; }
			set { m_unresolveable_color = value; }
		}

		private Color m_unresolveable_color = System.Drawing.Color.Red;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IPAddress IpAddress { get; set; }

		#endregion

		#region Events

		public event EventHandler<NameResolvedEventArgs> NameResolved;

		protected virtual void OnNameResolved(NameResolvedEventArgs args)
		{
			if (DesignMode) return;
			NameResolved?.Invoke(this, args);
		}

		protected virtual void OnNameResolved(IPAddress ip_address, string name)
		{
			OnNameResolved(new NameResolvedEventArgs(ip_address, name));
		}

		public event EventHandler<ResolveFailedEventArgs> ResolveFailed;

		protected virtual void OnResolveFailed(ResolveFailedEventArgs args)
		{
			if (DesignMode) return;
			ResolveFailed?.Invoke(this, args);
		}

		protected virtual void OnResolveFailed(string text)
		{
			OnResolveFailed(new ResolveFailedEventArgs(text));
		}

		public event EventHandler<ResolveStartedEventArgs> ResolveStarted;

		protected virtual void OnResolveStarted(ResolveStartedEventArgs args)
		{
			if (DesignMode) return;
			ResolveStarted?.Invoke(this, args);
		}

		protected virtual void OnResolveStarted()
		{
			OnResolveStarted(new ResolveStartedEventArgs());
		}

		public event EventHandler<ResolveFinishedEventArgs> ResolveFinished;

		protected virtual void OnResolveFinished(ResolveFinishedEventArgs args)
		{
			if (DesignMode) return;
			ResolveFinished?.Invoke(this, args);
		}

		protected virtual void OnResolveFinished()
		{
			OnResolveFinished(new ResolveFinishedEventArgs());
		}

		#endregion

		public IpTextBox()
		{
			ValidChars = new HashSet<char>();
			ValidChars.Add('.');
			ValidChars.Add('-');
			// Add 0-9
			for (int c = 0x30; c <= 0x39; c++)
			{
				ValidChars.Add((char)c);
			}
			// Add A-Z
			for (int c = 0x41; c <= 0x5A; c++)
			{
				ValidChars.Add((char)c);
			}
			// Add a-z
			for (int c = 0x61; c <= 0x7A; c++)
			{
				ValidChars.Add((char)c);
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			ResolveSuccessful = false;
			ResolveRunning = false;
			base.OnTextChanged(e);

			// Initialize BaseForeColor
			if (!BaseForeColor.HasValue)
			{
				BaseForeColor = ForeColor;
			}

			// Set current forecolor to the BaseForeColor
			ChangeForeColor(BaseForeColor.Value);

			#region Check Input

			if (string.IsNullOrWhiteSpace(Text))
			{
				return;
			}

			IPAddress buffer;
			if (IPAddress.TryParse(Text, out buffer))
			{
				ResolveSuccessful = true;
				IpAddress = buffer;
				return;
			}
			else
			{
				IpAddress = null;
			}

			// Check name length
			if ((Text.Length < MinNameLength) || (Text.Length > MaxNameLength))
			{
				ChangeForeColor(UnresolveableColor);
				return;
			}
			// Check for invalid characters
			foreach (char c in Text)
			{
				if (!ValidChars.Contains(c))
				{
					ChangeForeColor(UnresolveableColor);
					return;
				}
			}
			// DNS name must not begin or end with hyphen
			if ((Text[0] == '-') || (Text[Text.Length - 1] == '-'))
			{
				ChangeForeColor(UnresolveableColor);
				return;
			}

			#endregion

			// Try to resolve the DNS name
			ResolveRunning = true;
			try
			{
				Dns.BeginGetHostEntry(Text, new AsyncCallback(IpResolveCompleted), Text);
			}
			catch
			{
				ResolveRunning = false;
			}
		}

		private void IpResolveCompleted(IAsyncResult ar)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => IpResolveCompleted(ar)));
				return;
			}

			IPHostEntry entry = null;
			var text = ar.AsyncState as string;
			if (text != Text)
			{
				try
				{
					entry = Dns.EndGetHostEntry(ar);
				}
				catch { }
				return;
			}

			ResolveRunning = false;

			try
			{
				entry = Dns.EndGetHostEntry(ar);
			}
			catch (SocketException)
			{
				OnResolveFailed(text);
				ChangeForeColor(UnresolveableColor);
			}

			ValidateResolvedIp(entry);
		}

		private void ValidateResolvedIp(IPHostEntry entry)
		{
			if (entry == null ||
				entry.AddressList == null ||
				entry.AddressList.Length <= 0)
			{
				OnResolveFailed(Text);
				ChangeForeColor(UnresolveableColor);
				return;
			}

			foreach (IPAddress value in entry.AddressList)
			{
				if ((value.AddressFamily == AddressFamily.InterNetwork) ||
					(value.AddressFamily == AddressFamily.InterNetworkV6))
				{
					IpAddress = value;

					ResolveSuccessful = true;
					OnNameResolved(IpAddress, Text);
					return;
				}
			}

			OnResolveFailed(Text);
			ChangeForeColor(UnresolveableColor);
			return;
		}

		private void ChangeForeColor(Color fore_color)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new Action(() => ChangeForeColor(fore_color)));
				return;
			}
			ForeColor = fore_color;
		}
	}
}