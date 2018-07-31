//#define MANAGED_HOOKS

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#if MANAGED_HOOKS
using ManagedHooks;
#endif

namespace ImbaControls.Calendar
{
	public class SelectionChangedEventArgs : System.EventArgs
	{
		public SortedSet<DateTime> SelectedDays;

		public SelectionChangedEventArgs(ISet<DateTime> selected_days)
		{
			SelectedDays = new SortedSet<DateTime>(selected_days);
		}
	}

	public class VisibleMonthChangedEventArgs : System.EventArgs
	{
		public SortedSet<DateTime> VisibleMonths;
		public bool Increased { get; set; }

		public bool Decreased
		{
			get
			{
				return !Increased;
			}
			set
			{
				Increased = !value;
			}
		}

		public VisibleMonthChangedEventArgs(IEnumerable<DateTime> visible_months, bool increased)
		{
			VisibleMonths = new SortedSet<DateTime>(visible_months);
			Increased = increased;
		}
	}

	public class DateClickedEventArgs : System.EventArgs
	{
		public DateTime Date;

		public DateClickedEventArgs(DateTime date)
		{
			Date = date;
		}
	}

	public class CImbaCalendar : Control
	{
		#region Member

		public enum SelectionModes
		{
			Day = 0,
			Week = 1,
			Month = 2,
			Fix = 3,
			DragRange = 4,
			ClickRange = 5,
			Free = 6,
		}

		protected enum Operations
		{
			Deselecting = 0,
			Selecting = 1,
		}

		private const int LineSpacing = 5;

		[Browsable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Defines the error message displayed as tool tip " +
			"when the user clicks outside MinimumDate and MaximumDate boundaries")]
		public string OutOfRangeError
		{
			get { return m_out_of_range_error; }
			set { m_out_of_range_error = value; }
		}

		private string m_out_of_range_error = "";

		[Browsable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Defines the error message displayed as tool tip " +
			"when the user tries to select ranger bigger than MaximumSelectedDays")]
		public string SelectionTooLongError
		{
			get { return m_selection_too_long_error; }
			set { m_selection_too_long_error = value; }
		}

		private string m_selection_too_long_error = "";

		[Browsable(true)]
		[DefaultValue(2000)]
		[Category("Appearance")]
		[Description("Defines how long the tool tip is visible when " +
			"displaying an error")]
		public int ErrorToolTipDuration
		{
			get { return m_error_tool_tip_duration; }
			set { m_error_tool_tip_duration = value; }
		}

		private int m_error_tool_tip_duration = 2000;

		[Browsable(true)]
		[DefaultValue(typeof(Color), "Red")]
		[Category("Appearance")]
		[Description("Defines the color of the highlighting for holidays")]
		public Color HolidayColor
		{
			get { return m_holiday_color; }
			set { m_holiday_color = value; }
		}

		private Color m_holiday_color = System.Drawing.Color.Red;

		[Browsable(true)]
		[DefaultValue(typeof(Color), "DodgerBlue")]
		[Category("Appearance")]
		[Description("Sets the highlighting color for the current day")]
		public Color TodayColor
		{
			get { return m_today_color; }
			set { m_today_color = value; }
		}

		private Color m_today_color = System.Drawing.Color.DodgerBlue;

		[Browsable(true)]
		[DefaultValue(typeof(Color), "White")]
		[Category("Appearance")]
		[Description("Sets the background color of the date elements")]
		public Color DateBackColor
		{
			get { return m_date_back_color; }
			set { m_date_back_color = value; }
		}

		private Color m_date_back_color = System.Drawing.Color.White;

		[Browsable(true)]
		[DefaultValue(typeof(Color), "Gray")]
		[Category("Appearance")]
		[Description("Sets the highlight color of the date elements")]
		public Color SelectionColor
		{
			get { return m_selection_color; }
			set { m_selection_color = value; }
		}

		private Color m_selection_color = System.Drawing.Color.Gray;

		[Browsable(true)]
		[DefaultValue(50)]
		[Category("Appearance")]
		[Description("Defines the percentage of the flattening of " +
			"the fore colors for months outside the visiblity.")]
		public byte Flattening
		{
			get { return m_flattening; }
			set { m_flattening = Math.Max(Math.Min(value, (byte)100), (byte)0); }
		}

		private byte m_flattening = 50;

		[Browsable(true)]
		[Category("Appearance")]
		[Description("Defines the font used for the header")]
		public Font HeaderFont
		{
			get { return m_header_font; }
			set { m_header_font = value; }
		}

		private Font m_header_font = DefaultFont;

		[Browsable(true)]
		[Category("Appearance")]
		[Description("Defines the font used for the day names")]
		public Font DayNameFont
		{
			get { return m_day_name_font; }
			set { m_day_name_font = value; }
		}

		private Font m_day_name_font = DefaultFont;

		[Browsable(true)]
		[Category("Appearance")]
		[Description("Set to true to show parts of the next month or leave the space empty")]
		public bool ShowPartialMonth { get; set; }

		[Browsable(true)]
		[Category("Data")]
		[Description("Sets the holidays to be highlighted")]
		public DateTime[] Holidays
		{
			get { return m_holidays.ToArray<DateTime>(); }
			set { m_holidays = new HashSet<DateTime>(value); }
		}

		private HashSet<DateTime> m_holidays = new HashSet<DateTime>();

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Definies the smallest selectable date")]
		public DateTime MinimumDate
		{
			get { return m_minimum_date; }
			set { m_minimum_date = value.Date; }
		}

		private DateTime m_minimum_date;

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Definies the highest selectable date")]
		public DateTime MaximumDate
		{
			get { return m_maximum_date; }
			set { m_maximum_date = value.Date; }
		}

		private DateTime m_maximum_date;

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Specifies the selection mode to use")]
		public SelectionModes SelectionMode { get; set; }

		[Browsable(true)]
		[Category("Behavior")]
		[Description("Defines the maximum number of selectable days. " +
			"Some selection modes have fixed numbers of selectable days")]
		public int MaximumSelectedDays
		{
			get
			{
				switch (SelectionMode)
				{
				case SelectionModes.Day:
					return 1;

				case SelectionModes.Week:
					return 7;

				case SelectionModes.Month:
					return -1;

				default:
					return m_maximum_selected_days;
				}
			}
			set { m_maximum_selected_days = value; }
		}

		private int m_maximum_selected_days = 14;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime? SelectionStart
		{
			get
			{
				if (SelectedDays.Count > 0)
				{
					return SelectedDays.First();
				}
				else
				{
					return null;
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime? SelectionEnd
		{
			get
			{
				if (SelectedDays.Count > 0)
				{
					return SelectedDays.Last();
				}
				else
				{
					return null;
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public SortedSet<DateTime> SelectedDays
		{
			get { return m_selected_days; }
			set
			{
				m_selected_days = value;
				Invalidate();
			}
		}

		private SortedSet<DateTime> m_selected_days = new SortedSet<DateTime>();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime FirstVisibleMonth
		{
			get { return m_first_visible_month; }
			set
			{
				m_first_visible_month = value.FirstDayOfMonth();
				//Invalidate();
			}
		}

		private DateTime m_first_visible_month;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime LastVisibleMonth
		{
			get
			{
				return FirstVisibleMonth.AddMonths(NumberOfVisibleMonths - 1).FirstDayOfMonth();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IEnumerable<DateTime> VisibleMonths
		{
			get
			{
				ISet<DateTime> months = new SortedSet<DateTime>();
				for (DateTime month = FirstVisibleMonth; month <= LastVisibleMonth; month = month.AddMonths(1))
				{
					months.Add(month);
				}
				return months;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int NumberOfVisibleMonths
		{
			get
			{
				var used_height = GetHeightOfMonth(FirstVisibleMonth);
				if (used_height == Height)
				{
					return 1;
				}

				int number_of_month = 0;
				do
				{
					number_of_month += 1;
					used_height += GetHeightOfMonth(FirstVisibleMonth.AddMonths(number_of_month));
				} while (used_height < Height);
				if (ShowPartialMonth)
				{
					number_of_month += 1;
				}
				return number_of_month;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int HeaderHeight
		{
			get
			{
				return HeaderFont.Height;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int DayNameHeight
		{
			get
			{
				return DayNameFont.Height;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int WeekHeight
		{
			get
			{
				return Font.Height;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int LineHeight
		{
			get
			{
				return Font.Height + LineSpacing;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int DayWidth
		{
			get { return Width / 7; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int WeekPadding
		{
			get { return (LineHeight - WeekHeight) / 2; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int RoundEdgeRadius
		{
			get { return RoundEdgeDiagonal / 2; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int RoundEdgeDiagonal
		{
			get { return WeekHeight; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Color FlattenTextColor
		{
			get { return ForeColor.Flatten(DateBackColor, Flattening); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Color FlattenHolidayColor
		{
			get { return HolidayColor.Flatten(DateBackColor, Flattening); }
		}

		// Internal selection managing data
		protected DateTime? m_first_date;

		protected DateTime? m_last_date;
		protected Operations m_current_operation;
		protected HashSet<DateTime> m_selection_changed = new HashSet<DateTime>();
		private Point m_previous_mouse_location;

		// Tool tip to display error messages
		private TimedToolTip m_tool_tip;

#if MANAGED_HOOKS
		SystemHook m_system_hook;
#endif

		#endregion

		#region Events

		public event System.EventHandler<SelectionChangedEventArgs> SelectionChanged;

		protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			SelectionChanged?.Invoke(this, e);
		}

		protected virtual void OnSelectionChanged(ISet<DateTime> selected_days)
		{
			OnSelectionChanged(new SelectionChangedEventArgs(selected_days));
		}

		public event System.EventHandler<VisibleMonthChangedEventArgs> VisibleMonthChanged;

		protected virtual void OnVisibleMonthChanged(VisibleMonthChangedEventArgs e)
		{
			VisibleMonthChanged?.Invoke(this, e);
		}

		protected virtual void OnVisibleMonthChanged(IEnumerable<DateTime> visible_months, bool increased)
		{
			OnVisibleMonthChanged(new VisibleMonthChangedEventArgs(visible_months, increased));
		}

		public event System.EventHandler<DateClickedEventArgs> DateClicked;

		protected virtual void OnDateClicked(DateClickedEventArgs e)
		{
			DateClicked?.Invoke(this, e);
		}

		protected virtual void OnDateClicked(DateTime date)
		{
			OnDateClicked(new DateClickedEventArgs(date));
		}

		#endregion

		public CImbaCalendar()
		{
			this.SetStyle((ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint), true);
			FirstVisibleMonth = DateTime.Now.Date;
			MinimumDate = new DateTime(2015, 01, 01);
			MaximumDate = new DateTime(2100, 12, 31);

			m_tool_tip = new TimedToolTip(this);

#if MANAGED_HOOKS
			this.Enter += (sender, args) => {
				if (m_system_hook == null) {
					m_system_hook = new SystemHook();

					m_system_hook.MouseWheelChange += (ss, event_args) => {
						System.Diagnostics.Debug.Print("Mouse Wheel Changed");
					};
					m_system_hook.MouseLeftButtonDown += (ss, event_args) => {
						System.Diagnostics.Debug.Print("Mouse Left Button Down");
					};
				}
			};
#endif
		}

		public void SyncSelection(CImbaCalendar cal)
		{
			m_first_date = cal.m_first_date;
			m_last_date = cal.m_last_date;
			//m_current_operation = cal.m_current_operation;
			//m_selection_changed =
			SelectedDays = cal.SelectedDays;
		}

		#region BUTTONS

		private void bt_next_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			int dy = this.Font.Height;
			var down = new Pen(ForeColor, 12);
			down.EndCap = LineCap.ArrowAnchor;
			e.Graphics.DrawLine(down, e.ClipRectangle.Width / 2 - 25, e.ClipRectangle.Height / 2, e.ClipRectangle.Width / 2 + 25,
			e.ClipRectangle.Height / 2);
		}

		private void bt_home_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			var home = new Pen(ForeColor, 5);
			home.StartCap = LineCap.DiamondAnchor;
			e.Graphics.DrawLine(home, e.ClipRectangle.Width / 2, e.ClipRectangle.Height / 2, e.ClipRectangle.Width / 2 + 1,
			e.ClipRectangle.Height / 2);
		}

		private void bt_prev_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			int dy = this.Font.Height;
			var up = new Pen(ForeColor, 12);
			up.StartCap = LineCap.ArrowAnchor;
			e.Graphics.DrawLine(up, e.ClipRectangle.Width / 2 - 25, e.ClipRectangle.Height / 2, e.ClipRectangle.Width / 2 + 25,
			e.ClipRectangle.Height / 2);
		}

		private void bt_next_Click(object sender, EventArgs e)
		{
			if (MaximumDate < FirstVisibleMonth.AddMonths(1)) return;
			FirstVisibleMonth = FirstVisibleMonth.AddMonths(1);
			OnVisibleMonthChanged(VisibleMonths, true);
			Invalidate();
		}

		private void bt_prev_Click(object sender, EventArgs e)
		{
			if (MinimumDate.FirstDayOfMonth() > FirstVisibleMonth.AddMonths(-1)) return;
			FirstVisibleMonth = FirstVisibleMonth.AddMonths(-1);
			OnVisibleMonthChanged(VisibleMonths, false);
			Invalidate();
		}

		private void bt_home_Click(object sender, EventArgs e)
		{
			FirstVisibleMonth = DateTime.Now.Date;
			this.Invalidate();
		}

		#endregion

		#region OVERRIDES

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			FirstVisibleMonth = FirstVisibleMonth.AddMonths(Math.Sign(e.Delta));
			Invalidate();
			base.OnMouseWheel(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Controls.Clear();

			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			DrawBackground(e);

			Brush text_brush = new SolidBrush(this.ForeColor);
			Brush holiday_brush = new SolidBrush(HolidayColor);
			var header_font = new Font(this.Font, FontStyle.Bold);

			var center_text = new StringFormat();
			center_text.Alignment = StringAlignment.Center;

			// Make sure the first visible month is inside MinimumDate and MaximumDate
			if (FirstVisibleMonth < MinimumDate) FirstVisibleMonth = MinimumDate;
			if (FirstVisibleMonth > MaximumDate) FirstVisibleMonth = MaximumDate;
			DateTime current_visible_month = FirstVisibleMonth;

			// Decide minimus size
			float minimum_width = 0;
			for (int i = 0; i < 7; i++)
			{
				minimum_width += e.Graphics.MeasureString(GetWeekdayName(i), header_font).Width;
			}
			var minimum_height = GetHeightOfMonth(FirstVisibleMonth);
			MinimumSize = new Size((int)minimum_width, minimum_height);

			foreach (DateTime month in VisibleMonths)
			{
				DrawMonth(month, e.Graphics);
			}

			base.OnPaint(e);
		}

		protected override void OnEnter(EventArgs e)
		{
			Focus();
			base.OnEnter(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			m_previous_mouse_location = e.Location;

			// Quit if no date at location
			var date_clicked = GetDateFromLocation(e.Location);
			if (!date_clicked.HasValue)
			{
				return;
			}

			// Date must be between MinimumDate and MaximumDate
			if ((date_clicked.Value < MinimumDate) || (date_clicked.Value > MaximumDate))
			{
				m_tool_tip.Show(OutOfRangeError, ErrorToolTipDuration, e.Location);
				return;
			}

			OnDateClicked(date_clicked.Value);

			switch (SelectionMode)
			{
			case SelectionModes.Day:

				if (SelectedDays.Contains(date_clicked.Value))
				{
					SelectedDays.Clear();
				}
				else
				{
					SelectedDays.Clear();
					SelectedDays.Add(date_clicked.Value);
				}
				break;

			case SelectionModes.Week:
				if (SelectedDays.Contains(date_clicked.Value))
				{
					SelectedDays.Clear();
				}
				else
				{
					SelectedDays.Clear();
					SelectWeek(date_clicked.Value);
				}
				break;

			case SelectionModes.Month:
				if (SelectedDays.Contains(date_clicked.Value))
				{
					SelectedDays.Clear();
				}
				else
				{
					SelectedDays.Clear();
					SelectMonth(date_clicked.Value);
				}
				break;

			case SelectionModes.Fix:
				if (SelectedDays.Contains(date_clicked.Value))
				{
					SelectedDays.Clear();
				}
				else
				{
					SelectedDays.Clear();
					SelectFixedDays(date_clicked.Value);
				}
				break;

			case SelectionModes.DragRange:
				if (SelectedDays.Contains(date_clicked.Value))
				{
					SelectedDays.Clear();
					m_first_date = null;
				}
				else
				{
					SelectedDays.Clear();
					SelectedDays.Add(date_clicked.Value);
					m_first_date = date_clicked.Value;
				}
				break;

			case SelectionModes.ClickRange:
				if (!m_first_date.HasValue || 
					m_last_date.HasValue ||
					(m_first_date.HasValue && m_first_date.Value == date_clicked))
				{
					SelectedDays.Clear();
					SelectedDays.Add(date_clicked.Value);
					m_first_date = date_clicked.Value;
					m_last_date = null;
				}
				else
				{
					var selected_days = GetEachDayBetween(m_first_date.Value, date_clicked.Value);
					if (selected_days.Count > MaximumSelectedDays)
					{
						m_tool_tip.Show(SelectionTooLongError, ErrorToolTipDuration, e.Location);
						return;
					}
					SelectDaysBetween(m_first_date.Value, date_clicked.Value);
					m_last_date = date_clicked.Value;
				}
				break;

			case SelectionModes.Free:
				m_first_date = date_clicked.Value;
				m_selection_changed.Clear();
				if (SelectedDays.Contains(date_clicked.Value))
				{
					m_current_operation = Operations.Deselecting;
					SelectedDays.Remove(date_clicked.Value);
				}
				else
				{
					m_current_operation = Operations.Selecting;
					SelectedDays.Add(date_clicked.Value);
				}
				break;
			}

			OnSelectionChanged(SelectedDays);

			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			Focus();

			// Quit if mouse button not pressed
			if (e.Button == MouseButtons.None)
			{
				return;
			}

			// Quit if mouse is outside of the control
			if (!ClientRectangle.Contains(e.Location))
			{
				return;
			}

			// Quit if location unchanged
			if (m_previous_mouse_location.Equals(e.Location))
			{
				return;
			}

			// Quit if we didn't select a start date yet
			if (!m_first_date.HasValue)
			{
				return;
			}

			// Quit if no date at location
			var date_hover = GetDateFromLocation(e.Location);
			if (!date_hover.HasValue)
			{
				return;
			}

			// Date must be between MinimumDate and MaximumDate
			if (date_hover.Value > MaximumDate)
			{
				date_hover = MaximumDate;
			}
			if (date_hover.Value < MinimumDate)
			{
				date_hover = MinimumDate;
			}

			// Quit if mouse over same date as last event
			if (date_hover == GetDateFromLocation(m_previous_mouse_location))
			{
				return;
			}
			m_previous_mouse_location = e.Location;

			switch (SelectionMode)
			{
			case SelectionModes.DragRange:
				SelectedDays.Clear();
				SelectDaysBetween(m_first_date.Value, date_hover.Value);
				break;

			case SelectionModes.Free:
				if (m_current_operation == Operations.Deselecting)
				{
					// Get all days in selected range
					IEnumerable<DateTime> selected_days =
						GetEachDayBetween(m_first_date.Value, date_hover.Value);
					// Get all days in selected range which
					// have been selected already
					var newly_selected_days =
						selected_days.Where(date => SelectedDays.Contains(date));
					// Keep them in mind in case the user
					// changes his mind
					m_selection_changed.AddRange(newly_selected_days);

					if (m_last_date.HasValue)
					{
						var unselected_days = m_selection_changed.Except(
							GetEachDayBetween(m_first_date.Value, date_hover.Value)
							).ToList();
						SelectedDays.AddRange(unselected_days);
						m_selection_changed.RemoveRange(unselected_days);
					}

					m_last_date = date_hover.Value;
					DeselectDaysBetween(m_first_date.Value, date_hover.Value);
				}
				else
				{
					// Get all days in selected range
					IEnumerable<DateTime> selected_days =
						GetEachDayBetween(m_first_date.Value, date_hover.Value);
					// Get all days in selected range which
					// haven't been selected already
					var newly_selected_days =
						selected_days.Where(date => !SelectedDays.Contains(date));
					// Keep them in mind in case the user
					// changes his mind
					m_selection_changed.AddRange(newly_selected_days);

					if (m_last_date.HasValue)
					{
						var unselected_days = m_selection_changed.Except(
							GetEachDayBetween(m_first_date.Value, date_hover.Value)
							).ToList();
						SelectedDays.RemoveRange(unselected_days);
						m_selection_changed.RemoveRange(unselected_days);
					}

					m_last_date = date_hover.Value;
					SelectDaysBetween(m_first_date.Value, date_hover.Value);
				}
				break;
			}

			OnSelectionChanged(SelectedDays);

			//Invalidate();
			Refresh();
		}

		#endregion

		#region INNER CONTROL LOGIC

		private List<DateTime> GetEachDayBetween(DateTime start, DateTime end)
		{
			if (end < start)
			{
				DateTime temp = end;
				end = start;
				start = temp;
			}

			var dates = new List<DateTime>((end - start).Days);

			for (; start <= end; start = start.AddDays(1))
			{
				dates.Add(start);
			}

			return dates;
		}

		private void SelectDaysBetween(DateTime start, DateTime end)
		{
			SelectedDays.AddRange(GetEachDayBetween(start, end));
		}

		private void DeselectDaysBetween(DateTime start, DateTime end)
		{
			SelectedDays.RemoveRange(GetEachDayBetween(start, end));
		}

		private List<DateTime> GetEachDayOfFixed(DateTime date)
		{
			var fixed_dates = new List<DateTime>(MaximumSelectedDays);

			for (int i = 0; i < MaximumSelectedDays; i++)
			{
				fixed_dates.Add(date.AddDays(i));
			}

			return fixed_dates;
		}

		private void SelectFixedDays(DateTime date)
		{
			SelectedDays.AddRange(GetEachDayOfFixed(date));
		}

		private List<DateTime> GetEachDayOfMonth(DateTime date)
		{
			var month = new List<DateTime>(31);

			date = date.FirstDayOfMonth();
			var last_day = date.LastDayOfMonth();
			for (; date <= last_day; date = date.AddDays(1))
			{
				month.Add(date);
			}

			return month;
		}

		private void SelectMonth(DateTime month)
		{
			SelectedDays.AddRange(GetEachDayOfMonth(month));
		}

		private List<DateTime> GetEachDayOfWeek(DateTime date)
		{
			var week = new List<DateTime>(7);

			// Get first day of the week
			date = date.AddDays(GetWeekday(date) * -1);
			// Iterate through each day of the week and add to selected
			for (int i = 0; i < 7; i++)
			{
				week.Add(date.AddDays(i));
			}

			return week;
		}

		private void SelectWeek(DateTime date)
		{
			SelectedDays.AddRange(GetEachDayOfWeek(date));
		}

		private Size GetScaledPreviousButtonSize()
		{
			return GetScaledButtonSize(Properties.Resources.arrow_left);
		}

		private Size GetScaledNextButtonSize()
		{
			return GetScaledButtonSize(Properties.Resources.arrow_right);
		}

		private Size GetScaledButtonSize(Image arrow)
		{
			return new Size((int)(((float)HeaderHeight / arrow.Height) * arrow.Width), HeaderHeight);
		}

		private DateTime? GetDateFromLocation(Point location)
		{
			//System.Diagnostics.Debug.Print("mouse x: " + location.X.ToString() + " mouse y: " + location.Y.ToString());

			foreach (DateTime month in VisibleMonths)
			{
				var month_location = GetLocationOfMonth(month);
				if (!month_location.HasValue) continue;
				if (location.Y < month_location.Value.Y) break;
				if (location.Y > (month_location.Value.Y + GetHeightOfMonth(month))) continue;

				location.Y -= month_location.Value.Y;
				location.Y -= HeaderHeight;
				location.Y -= DayNameHeight;

				// Select a day in the proper week of the month
				var date = month.AddDays((location.Y / LineHeight) * 7);
				// Select the proper day in the week of the month
				date = date.AddDays((location.X / DayWidth) - GetWeekday(date));

				if (date.Month == month.Month)
				{
					return date;
				}
			}

			return null;
		}

		private void DrawBackground(PaintEventArgs e)
		{
			var background_brush = new LinearGradientBrush(e.ClipRectangle, this.BackColor,
			ControlPaint.Light(this.BackColor), 90);
			e.Graphics.FillRectangle(background_brush, e.ClipRectangle);
		}

		private Rectangle GetRectangle(DateTime date)
		{
			var first_weekday_of_month = GetWeekday(date.FirstDayOfMonth());
			var monthst = new DateTime(date.Year, date.Month, 1);
			int dy = Font.Height;
			int dx = DayWidth;

			int x = (int)((int)(((date - monthst).TotalDays + first_weekday_of_month) % 7) * dx) + 1;
			int y = (int)((int)(((date - monthst).TotalDays + first_weekday_of_month) / 7) * dy) + 2 * dy;

			return new Rectangle(x, y, dx, dy);
		}

		private DateTime GetDate(Point location, int list)
		{
			var dt = FirstVisibleMonth.AddMonths(list);
			var first_weekday_of_month = GetWeekday(dt.FirstDayOfMonth());
			int dy = this.Font.Height;
			int dx = DayWidth;
			var XYcolumn = (int)(location.X / dx);
			var XYstring = (int)((location.Y - 2 * dy) / dy);
			int adddays = 7 * XYstring + XYcolumn - first_weekday_of_month;

			if (adddays < 0) adddays = 0;

			if (adddays >= GetDaysInMonth(dt))
				adddays = GetDaysInMonth(dt) - 1;

			return dt.AddDays(adddays);
		}

		private int GetDaysInMonth(DateTime date)
		{
			return date.LastDayOfMonth().Day;
		}

		private string GetWeekdayName(int index)
		{
			var daynn = (int)(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
			// First day of week
			int actualdayn = (daynn + index) % 7;
			return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName((DayOfWeek)actualdayn);
		}

		private int GetWeekday(DateTime date)
		{
			var first_day_of_month = (int)date.DayOfWeek;
			var first_day_of_week = (int)(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
			if (first_day_of_week <= first_day_of_month)
				return (first_day_of_month - first_day_of_week);
			else
				return (7 - first_day_of_week - first_day_of_month);
		}

		private string GetLocalMonthName(DateTime month)
		{
			return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Month);
		}

		/// <summary>
		/// Returns the calendar week of a particular day.
		/// </summary>
		/// <param name="date">DateTime specifing the particular day</param>
		/// <returns>Integer between 1 and 53</returns>
		private int GetWeekOfYear(DateTime date)
		{
			DateTimeFormatInfo date_time_format_info = DateTimeFormatInfo.CurrentInfo;
			System.Globalization.Calendar calendar = date_time_format_info.Calendar;
			return calendar.GetWeekOfYear(date,
				date_time_format_info.CalendarWeekRule,
				date_time_format_info.FirstDayOfWeek);
		}

		/// <summary>
		/// Returns the number of the week in the month of that
		/// particular day.
		/// </summary>
		/// <param name="date">DateTime specifing the particular day</param>
		/// <returns>Integer between 1 and 5</returns>
		private int GetWeekOfMonth(DateTime date)
		{
			var first_week = GetWeekOfYear(date.FirstDayOfMonth());
			var week_date = GetWeekOfYear(date);
			// In case of January when the week of the first
			// is still part of the previous year
			if (first_week > week_date)
			{
				first_week = 0;
			}
			return (week_date - first_week) + 1;
		}

		private int GetWeekCountOfMonth(DateTime month)
		{
			return GetWeekOfMonth(month.LastDayOfMonth());
		}

		private int GetHeightOfMonth(DateTime month)
		{
			var number_of_weeks = GetWeekCountOfMonth(month);
			//System.Diagnostics.Debug.Print("Month: " + month.ToString("YYYY-MM") + " Number of weeks: " + number_of_weeks.ToString() + " Height: " + (HeaderHeight + DayNameHeight + (LineHeight * number_of_weeks)).ToString());
			return HeaderHeight + DayNameHeight + (LineHeight * number_of_weeks);
		}

		private Point? GetLocationOfMonth(DateTime month)
		{
			month = month.Date.FirstDayOfMonth();
			if (month == FirstVisibleMonth) return new Point(0, 0);
			if (month < FirstVisibleMonth) return null;
			if (month > LastVisibleMonth) return null;

			DateTime first = FirstVisibleMonth;
			int y_pos = 0;
			for (; first < month; first = first.AddMonths(1))
			{
				y_pos += GetHeightOfMonth(first);
			}
			return new Point(0, y_pos);
		}

		private void DrawMonth(DateTime month, Graphics graph)
		{
			month = month.Date.FirstDayOfMonth();

			DrawMonthHeader(month, graph);
			DrawWeekLines(month, graph);
			DrawSelectionHighlight(month, graph);
			DrawDayNumbers(month, graph);
		}

		private void DrawWeekLines(DateTime month, Graphics graph)
		{
			month = month.Date.FirstDayOfMonth();
			var location = GetLocationOfMonth(month);
			if (!location.HasValue) return;
			int y_offset = location.Value.Y + HeaderHeight + DayNameHeight;

			var brush = new SolidBrush(DateBackColor);
			var edge_size = new Size(RoundEdgeDiagonal, RoundEdgeDiagonal);

			for (int i = 0; i < GetWeekCountOfMonth(month); i++)
			{
				// Set default position for the line
				var pos = new Point(0, y_offset);
				// Position at the line of the current week
				pos.Y += (LineHeight * i);
				// Add the padding to make it look a bit more fancy
				pos.Y += WeekPadding;
				// Count for the rounded edges
				pos.X += RoundEdgeRadius;

				// Set the default size of the line
				var size = new Size((DayWidth * 7), WeekHeight);
				// Count for the rounded edges
				size.Width -= (RoundEdgeRadius * 2);

				// Don't color the days of the previous month
				// if the previous month is already visible above
				if ((month != FirstVisibleMonth) && (i == 0))
				{
					pos.X += (GetWeekday(month) * DayWidth);
					size.Width -= (GetWeekday(month) * DayWidth);
				}
				// Don't color the days of the following month
				// if the following month is already visible above
				if ((month != LastVisibleMonth) && (i == (GetWeekCountOfMonth(month) - 1)))
				{
					size.Width -= ((6 - GetWeekday(month.LastDayOfMonth())) * DayWidth);
				}

				// Draw line for each week
				// Padded left and right for the rounded edges below
				graph.FillRectangle(brush, new Rectangle(pos, size));

				// Draw the rounded edges for the week lines
				pos.X -= RoundEdgeRadius;
				graph.FillEllipse(brush, new Rectangle(pos, edge_size));
				// on the right side as well
				pos.X += size.Width;
				graph.FillEllipse(brush, new Rectangle(pos, edge_size));
			}
		}

		private void DrawSelectionHighlight(DateTime month, Graphics graph)
		{
			month = month.Date.FirstDayOfMonth();
			var location = GetLocationOfMonth(month);
			if (!location.HasValue) return;
			int y_offset = location.Value.Y + HeaderHeight + DayNameHeight;
			var last_day_of_month = month.LastDayOfMonth();

			var brush = new SolidBrush(SelectionColor);
			Rectangle rectangle;

			foreach (DateTime selected_date in SelectedDays)
			{
				if ((selected_date.Year != month.Year) || (selected_date.Month != month.Month))
				{
					continue;
				}

				int y_pos = y_offset + (LineHeight * (GetWeekOfMonth(selected_date) - 1)) + WeekPadding;
				int x_pos = GetWeekday(selected_date) * DayWidth;
				int length = DayWidth;
				if (GetWeekday(selected_date) != 6)
				{
					length += 1;
				}
				if (!SelectedDays.Contains(selected_date.AddDays(1)))
				{
					rectangle = new Rectangle(x_pos + length - WeekHeight, y_pos, WeekHeight, WeekHeight);
					graph.FillEllipse(brush, rectangle);
					length -= (WeekHeight / 2);
				}
				if (!SelectedDays.Contains(selected_date.AddDays(-1)))
				{
					rectangle = new Rectangle(x_pos, y_pos, WeekHeight, WeekHeight);
					graph.FillEllipse(brush, rectangle);
					x_pos += (WeekHeight / 2);
					length -= (WeekHeight / 2);
				}

				rectangle = new Rectangle(x_pos, y_pos, length, WeekHeight);
				graph.FillRectangle(brush, rectangle);
			}
		}

		private void DrawDayNumbers(DateTime month, Graphics graph)
		{
			month = month.Date.FirstDayOfMonth();
			var location = GetLocationOfMonth(month);
			if (!location.HasValue) return;
			int y_offset = location.Value.Y + HeaderHeight + DayNameHeight;
			var last_day_of_month = month.LastDayOfMonth();

			Brush text_brush = new SolidBrush(ForeColor);
			Brush holiday_brush = new SolidBrush(HolidayColor);
			Brush flatten_text_brush = new SolidBrush(FlattenTextColor);
			Brush flatten_holiday_brush = new SolidBrush(FlattenHolidayColor);
			var text_format = new StringFormat();
			text_format.Alignment = StringAlignment.Center;

			// Draw the last days of the previous month
			// Only for the first visible month
			if ((month == FirstVisibleMonth) && (GetWeekday(month) != 0))
			{
				var prev_month = month.AddDays(GetWeekday(month) * -1);
				for (; prev_month < month; prev_month = prev_month.AddDays(1))
				{
					int x_pos = GetWeekday(prev_month) * DayWidth;
					int y_pos = y_offset + (LineHeight * (GetWeekOfMonth(month) - 1)) + WeekPadding;

					var day_rectangle = new Rectangle(x_pos, y_pos, DayWidth, WeekHeight);
					if (Holidays.Contains(prev_month))
					{
						graph.DrawString(prev_month.Day.ToString(), Font, flatten_holiday_brush, day_rectangle, text_format);
					}
					else
					{
						graph.DrawString(prev_month.Day.ToString(), Font, flatten_text_brush, day_rectangle, text_format);
					}
				}
			}

			for (DateTime day = month; day <= last_day_of_month; day = day.AddDays(1))
			{
				int x_pos = GetWeekday(day) * DayWidth;
				int y_pos = y_offset + (LineHeight * (GetWeekOfMonth(day) - 1)) + WeekPadding;
				var day_rectangle = new Rectangle(x_pos, y_pos, DayWidth, WeekHeight);

				if (Holidays.Contains(day))
				{
					graph.DrawString(day.Day.ToString(), Font, holiday_brush, day_rectangle, text_format);
				}
				else
				{
					graph.DrawString(day.Day.ToString(), Font, text_brush, day_rectangle, text_format);
				}

				if (day == DateTime.Now.Date)
				{
					var brush = new Pen(TodayColor);
					graph.DrawRectangle(brush, day_rectangle);
				}
			}

			// Draw the first days of the next month
			// Only for the last visible month
			month = month.LastDayOfMonth();
			if ((month.FirstDayOfMonth() == LastVisibleMonth) && (GetWeekday(month) != 6))
			{
				var next_month = month.AddDays(6 - GetWeekday(month));
				for (; next_month > month; next_month = next_month.AddDays(-1))
				{
					int x_pos = GetWeekday(next_month) * DayWidth;
					int y_pos = y_offset + (LineHeight * (GetWeekOfMonth(month) - 1)) + WeekPadding;

					var day_rectangle = new Rectangle(x_pos, y_pos, DayWidth, WeekHeight);
					if (Holidays.Contains(next_month))
					{
						graph.DrawString(next_month.Day.ToString(), Font, flatten_holiday_brush, day_rectangle, text_format);
					}
					else
					{
						graph.DrawString(next_month.Day.ToString(), Font, flatten_text_brush, day_rectangle, text_format);
					}
				}
			}
		}

		private void DrawMonthHeader(DateTime month, Graphics graph)
		{
			month = month.Date.FirstDayOfMonth();
			var location = GetLocationOfMonth(month);
			if (!location.HasValue) return;
			int y_pos = location.Value.Y;

			DrawMonthHeaderPrevButton(month, y_pos);
			DrawMonthHeaderNextButton(month, y_pos);
			DrawMonthHeaderName(month, y_pos);
			DrawMonthHeaderDayNames(month, (y_pos + HeaderHeight), graph);
		}

		private void DrawMonthHeaderNextButton(DateTime month, int y_pos)
		{
			if ((month != FirstVisibleMonth) || (month.AddMonths(1) > MaximumDate))
			{
				return;
			}
			var arrow = Properties.Resources.arrow_right.Resize(GetScaledNextButtonSize());

			var next = new Button();
			next.FlatStyle = FlatStyle.Flat;
			next.FlatAppearance.BorderSize = 0;
			next.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
			next.Size = arrow.Size;
			next.Location = new Point((Width - next.Width), y_pos);
			next.Click += bt_next_Click;
			next.Image = arrow;
			this.Controls.Add(next);
		}

		private void DrawMonthHeaderPrevButton(DateTime month, int y_pos)
		{
			if ((month != FirstVisibleMonth) || (month.AddMonths(-1) < MinimumDate.FirstDayOfMonth()))
			{
				return;
			}

			var arrow = Properties.Resources.arrow_left.Resize(GetScaledPreviousButtonSize());

			var prev = new Button();
			prev.FlatStyle = FlatStyle.Flat;
			prev.FlatAppearance.BorderSize = 0;
			prev.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
			prev.Size = arrow.Size;
			prev.Location = new Point(0, y_pos);
			prev.Click += bt_prev_Click;
			prev.Image = arrow;
			this.Controls.Add(prev);
		}

		private void DrawMonthHeaderName(DateTime month, int y_pos)
		{
			var header = new Label();
			if (month == FirstVisibleMonth)
			{
				header.Location = new Point(GetScaledPreviousButtonSize().Width, y_pos);
				header.Size = new Size((Width - GetScaledPreviousButtonSize().Width) - GetScaledNextButtonSize().Width, HeaderHeight);
			}
			else
			{
				header.Location = new Point(0, y_pos);
				header.Size = new Size(Width, HeaderHeight);
			}
			header.BackColor = BackColor;
			header.Font = HeaderFont;
			header.Text = GetLocalMonthName(month) + " " + month.Year.ToString();
			header.TextAlign = ContentAlignment.MiddleCenter;
			header.Click += bt_home_Click;
			header.MouseEnter += (sender, event_arg) => { header.BackColor = SystemColors.ControlLight; };
			header.MouseLeave += (sender, event_arg) => { header.BackColor = BackColor; };
			this.Controls.Add(header);
		}

		private void DrawMonthHeaderDayNames(DateTime month, int y_pos, Graphics graph)
		{
			var center_text = new StringFormat();
			center_text.Alignment = StringAlignment.Center;
			Brush text_brush = new SolidBrush(ForeColor);

			for (int i = 0; i < 7; i++)
			{
				var day_name_rect = new Rectangle(DayWidth * i, y_pos, DayWidth, DayNameHeight);
				var day_name = GetWeekdayName(i);
				graph.DrawString(day_name, DayNameFont, text_brush, day_name_rect, center_text);
			}
		}

		#endregion

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
				if (m_tool_tip != null)
				{
					m_tool_tip.Dispose();
					m_tool_tip = null;
				}
				if (m_header_font != null)
				{
					m_header_font.Dispose();
					m_header_font = null;
				}
				if (m_day_name_font != null)
				{
					m_day_name_font.Dispose();
					m_day_name_font = null;
				}
#if MANAGED_HOOKS
				if (m_system_hook != null) {
					m_system_hook.Dispose();
					m_system_hook = null;
				}
#endif
			}

			disposed = true;

			base.Dispose(disposing);
		}

		#endregion
	}
}