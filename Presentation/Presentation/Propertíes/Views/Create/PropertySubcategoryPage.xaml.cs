using Immowert4You.Presentation.Common.Bases.Views;
using Immowert4You.Presentation.Properties.Models.Create;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Immowert4You.Presentation.Properties.Views.Create
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertySubcategoryPage : BaseContentPage
    {
        private SelectCell lastCell;

        public PropertySubcategoryPage(PropertySubcategoryViewModel viewModel)
        {
            BindingContext = viewModel;

            InitializeComponent();
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (lastCell != null)
            {
                lastCell.Frame.BackgroundColor = Color.Transparent;
                lastCell.Label.TextColor = lastCell.Icon.BorderColor = Color.FromHex("B4B4B4");
                lastCell.Image.Source = "circle_empty";
                if (lastCell.Icon.Source.ToString().Contains("flat"))
                    lastCell.Icon.Source = lastCell.Icon.Source.ToString().Split(' ')[1].Replace("_blue", "");
            }

            var viewCell = (ViewCell)sender;

            var frame = viewCell.FindByName<Frame>("frm");
            viewCell.View.BackgroundColor = Color.White;

            var stack = frame.FindByName<StackLayout>("stack");

            var icon = stack.FindByName<ImageButton>("icon");
            if (icon.Source.ToString().Contains("flat"))
                icon.Source = icon.Source.ToString().Split(' ')[1] + "_blue";

            var label = stack.FindByName<Label>("label");
            label.TextColor = Color.White;

            var image = stack.FindByName<Image>("image");
            image.Source = "circle_check";

            if (viewCell != null)
            {
                frame.BackgroundColor = icon.BorderColor = Color.FromHex("#0D62B0");
                lastCell = new SelectCell 
                { 
                    Frame = frame, 
                    Label = label, 
                    Image = image, 
                    Icon = icon 
                };
            }
        }
    }

    public class SelectCell
    {
        public ImageButton Icon { get; set; }
        public Frame Frame { get; set; }
        public Label Label { get; set; }
        public Image Image { get; set; }
    }
}