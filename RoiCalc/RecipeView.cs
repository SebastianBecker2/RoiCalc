using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RoiCalc
{
    partial class RecipeView : UserControl
    {
        public class IngredientClickEventArgs : EventArgs
        {
            public Item DisplayedRecipe { get; set; }
            public Item ClickedIngredient { get; set; }
        }

        public class IngredientDoubleClickEventArgs : IngredientClickEventArgs
        {
        }

        private class Ingredient
        {
            public PictureBox PibImage
            {
                get => pib_image;
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
            public Label LblName
            {
                get => lbl_name;
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
            public Label LblCount
            {
                get => lbl_count;
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

            private void OnClick(object sender, EventArgs e) => OnIngredientClick(Item);

            private void OnDoubleClick(object sender, EventArgs e) => OnIngredientDoubleClick(Item);

            public event EventHandler<IngredientClickEventArgs> IngredientClick;
            protected virtual void OnIngredientClick(Item Ingredient)
            {
                IngredientClick?.Invoke(this, new IngredientClickEventArgs()
                {
                    ClickedIngredient = Ingredient,
                });
            }

            public event EventHandler<IngredientDoubleClickEventArgs> IngredientDoubleClick;
            protected virtual void OnIngredientDoubleClick(Item Ingredient)
            {
                IngredientDoubleClick?.Invoke(this, new IngredientDoubleClickEventArgs()
                {
                    ClickedIngredient = Ingredient,
                });
            }

            public  void SetItem(Item item, int count)
            {
                Item = item;

                PibImage.Visible = (item != null);
                LblName.Visible = (item != null);
                LblCount.Visible = (item != null);

                if (item == null)
                {
                    return;
                }

                PibImage.Image = item.Image;
                LblName.Text = item.Name;
                LblCount.Text = count.ToString();
            }
        }

        private IEnumerable<Ingredient> Ingredients { get; set; }

        public event EventHandler<IngredientClickEventArgs> IngredientClick;
        protected virtual void OnIngredientClick(Item Ingredient)
        {
            IngredientClick?.Invoke(this, new IngredientClickEventArgs()
            {
                DisplayedRecipe = current_recipe,
                ClickedIngredient = Ingredient,
            });
        }

        public event EventHandler<IngredientDoubleClickEventArgs> IngredientDoubleClick;
        protected virtual void OnIngredientDoubleClick(Item Ingredient)
        {
            IngredientDoubleClick?.Invoke(this, new IngredientDoubleClickEventArgs()
            {
                DisplayedRecipe = current_recipe,
                ClickedIngredient = Ingredient,
            });
        }

        public class ItemClickEventArgs : EventArgs
        {
            public Item Item { get; set; }
        }

        public class ItemDoubleClickEventArgs : ItemClickEventArgs
        {
        }

        public Item Recipe
        {
            get => current_recipe;
            set
            {
                current_recipe = value;
                SetItem(current_recipe);
            }
        }
        private Item current_recipe;

        public event EventHandler<ItemClickEventArgs> ItemClick;
        protected virtual void OnItemClick(Item item)
        {
            ItemClick?.Invoke(this, new ItemClickEventArgs() { Item = item });
        }

        public event EventHandler<ItemDoubleClickEventArgs> ItemDoubleClick;
        protected virtual void OnItemDoubleClick(Item item)
        {
            ItemDoubleClick?.Invoke(this, new ItemDoubleClickEventArgs() { Item = item });
        }

        public RecipeView()
        {
            InitializeComponent();

            Ingredients = new List<Ingredient>()
            {
                new Ingredient() {
                    PibImage = pibIngredient1Image,
                    LblName = lblIngredient1Name,
                    LblCount = lblIngredient1Count,
                },
                new Ingredient() {
                    PibImage = pibIngredient2Image,
                    LblName = lblIngredient2Name,
                    LblCount = lblIngredient2Count,
                },
                new Ingredient() {
                    PibImage = pibIngredient3Image,
                    LblName = lblIngredient3Name,
                    LblCount = lblIngredient3Count,
                },
            };
            foreach (var ingredient in Ingredients)
            {
                ingredient.IngredientClick += (s, e) => OnIngredientClick(e.ClickedIngredient);
                ingredient.IngredientDoubleClick += (s, e) => OnIngredientDoubleClick(e.ClickedIngredient);
            }

            pibItemImage.Click += (s, e) => OnItemClick(Recipe);
            pibItemImage.DoubleClick += (s, e) => OnItemDoubleClick(Recipe);
            lblItemCount.Click += (s, e) => OnItemClick(Recipe);
            lblItemCount.DoubleClick += (s, e) => OnItemDoubleClick(Recipe);
            lblItemName.Click += (s, e) => OnItemClick(Recipe);
            lblItemName.DoubleClick += (s, e) => OnItemDoubleClick(Recipe);
        }

        private void SetItem(Item item)
        {
            if (item == null)
            {
                lblItemName.Text = string.Empty;
                pibItemImage.Image = null;
                lblItemCount.Text = string.Empty;
                lblInterval.Text = string.Empty;


                grbIngredients.Visible = false;
                return;
            }

            lblItemName.Text = item.Name;
            pibItemImage.Image = item.Image;
            lblItemCount.Text = item.Count.ToString();
            lblInterval.Text = item.Interval.ToString();

            grbIngredients.Visible = item.Ingredients.Any();

            foreach (var ingredient in Ingredients
                .Merge(item.Ingredients, (View, Req) => new { View, Item = Req.Key, Count = Req.Value }))
            {
                ingredient.View.SetItem(ingredient.Item, ingredient.Count);
            }
        }
    }
}
