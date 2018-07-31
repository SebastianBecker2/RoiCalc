public static class ObjectExtensions
{
	public static void TagLink(this System.Windows.Forms.Control left_ctrl, System.Windows.Forms.Control right_ctrl)
	{
		left_ctrl.Tag = right_ctrl;
		right_ctrl.Tag = left_ctrl;
	}
}