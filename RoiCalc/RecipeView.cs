using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoiCalc
{
    partial class RecipeView : UserControl
    {
        private class Requirement
        {
            public PictureBox pibImage { get; set; }
            public Label lblName { get; set; }
            public Label lblCount { get; set; }

            public void SetItem(Item item, int count)
            {
                pibImage.Visible = (item != null);
                lblName.Visible = (item != null);
                lblCount.Visible = (item != null);

                if (item == null)
                {
                    return;
                }

                pibImage.Image = item.Image;
                lblName.Text = item.Name;
                lblCount.Text = count.ToString();
            }
        }

        private IEnumerable<Requirement> Requirements { get; set; }

        public RecipeView()
        {
            InitializeComponent();

            Requirements = new List<Requirement>()
            {
                new Requirement() {
                    pibImage = pibRequirement1Image,
                    lblName = lblRequirement1Name,
                    lblCount = lblRequirement1Count,
                },
                new Requirement() {
                    pibImage = pibRequirement2Image,
                    lblName = lblRequirement2Name,
                    lblCount = lblRequirement2Count,
                },
                new Requirement() {
                    pibImage = pibRequirement3Image,
                    lblName = lblRequirement3Name,
                    lblCount = lblRequirement3Count,
                },
            };
        }

        public void SetItem(Item item)
        {
            if (item == null)
            {
                return;
            }

            lblItemName.Text = item.Name;
            pibItemImage.Image = item.Image;
            lblItemCount.Text = item.Count.ToString();
            lblInterval.Text = item.Interval.ToString();

            grbRequirements.Visible = item.Requirements.Any();

            foreach (var req in Requirements
                .Merge(item.Requirements, (View, Req) => new { View, Item = Req.Key, Count = Req.Value }))
            {
                req.View.SetItem(req.Item, req.Count);
            }
        }
    }
}
