using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RoiCalc
{
    partial class RecipeView : UserControl
    {
        public class RequirementClickEventArgs : EventArgs
        {
            public Item DisplayedRecipe { get; set; }
            public Item ClickedRequirement { get; set; }
        }

        public class RequirementDoubleClickEventArgs : RequirementClickEventArgs
        {
        }

        private class Requirement
        {
            public PictureBox pibImage {
                get { return pib_image; }
                set
                {
                    if (pib_image != null)
                    {
                        pib_image.Click -= OnClick;
                        pib_image.DoubleClick -= OnDoubleClick;
                    }
                    pib_image = value;
                    if (pib_image != null)
                    {
                        pib_image.Click += OnClick;
                        pib_image.DoubleClick += OnDoubleClick;
                    }
                }
            }
            private PictureBox pib_image;
            public Label lblName {
                get { return lbl_name; }
                set
                {
                    if (lbl_name != null)
                    {
                        lbl_name.Click -= OnClick;
                        lbl_name.DoubleClick -= OnDoubleClick;
                    }
                    lbl_name = value;
                    if (lbl_name != null)
                    {
                        lbl_name.Click += OnClick;
                        lbl_name.DoubleClick += OnDoubleClick;
                    }
                }
            }
            private Label lbl_name;
            public Label lblCount
            {
                get { return lbl_count; }
                set
                {
                    if (lbl_count != null)
                    {
                        lbl_count.Click -= OnClick;
                        lbl_count.DoubleClick -= OnDoubleClick;
                    }
                    lbl_count = value;
                    if (lbl_count != null)
                    {
                        lbl_count.Click += OnClick;
                        lbl_count.DoubleClick += OnDoubleClick;
                    }
                }
            }
            private Label lbl_count;

            private Item Item;

            private void OnClick(object sender, EventArgs e)
            {
                OnRequirementClick(Item);
            }

            private void OnDoubleClick(object sender, EventArgs e)
            {
                OnRequirementDoubleClick(Item);
            }

            public event EventHandler<RequirementClickEventArgs> RequirementClick;
            protected virtual void OnRequirementClick(Item requirement)
            {
                RequirementClick?.Invoke(this, new RequirementClickEventArgs()
                {
                    ClickedRequirement = requirement,
                });
            }

            public event EventHandler<RequirementDoubleClickEventArgs> RequirementDoubleClick;
            protected virtual void OnRequirementDoubleClick(Item requirement)
            {
                RequirementDoubleClick?.Invoke(this, new RequirementDoubleClickEventArgs()
                {
                    ClickedRequirement = requirement,
                });
            }

            public  void SetItem(Item item, int count)
            {
                Item = item;

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

        public event EventHandler<RequirementClickEventArgs> RequirementClick;
        protected virtual void OnRequirementClick(Item requirement)
        {
            RequirementClick?.Invoke(this, new RequirementClickEventArgs()
            {
                DisplayedRecipe = current_recipe,
                ClickedRequirement = requirement,
            });
        }

        public event EventHandler<RequirementDoubleClickEventArgs> RequirementDoubleClick;
        protected virtual void OnRequirementDoubleClick(Item requirement)
        {
            RequirementDoubleClick?.Invoke(this, new RequirementDoubleClickEventArgs()
            {
                DisplayedRecipe = current_recipe,
                ClickedRequirement = requirement,
            });
        }

        public Item Recipe
        {
            get { return current_recipe; }
            set
            {
                current_recipe = value;
                SetItem(current_recipe);
            }
        }
        private Item current_recipe;

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
            foreach (var req in Requirements)
            {
                req.RequirementClick += (s, e) => OnRequirementClick(e.ClickedRequirement);
                req.RequirementDoubleClick += (s, e) => OnRequirementDoubleClick(e.ClickedRequirement);
            }
        }

        private void SetItem(Item item)
        {
            if (item == null)
            {
                lblItemName.Text = string.Empty;
                pibItemImage.Image = null;
                lblItemCount.Text = string.Empty;
                lblInterval.Text = string.Empty;


                grbRequirements.Visible = false;
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
