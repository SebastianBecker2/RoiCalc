using System;
using System.Windows.Forms;

namespace RoiCalc
{
    public partial class AboutDialog : Form
    {
        private string VersionPrefix;
        private Random rnd = new Random();

        public AboutDialog()
        {
            InitializeComponent();
            VersionPrefix = lblVersion.Text;
            timVersion.Tick += (s, e) =>
            {
                lblVersion.Text = $"{VersionPrefix} {rnd.Next(10,25)}.{rnd.Next(0,9):D1}.{rnd.Next(0, 999):D3}";
            };
            timVersion.Start();
        }
    }
}
