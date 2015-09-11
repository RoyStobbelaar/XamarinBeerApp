using System;

using Xamarin.Forms;

namespace BierApp
{
    public class MyBeerPage : ContentPage
    {
        public MyBeerPage()
        {
            Padding = new Thickness(10);

            var lblBeer = new Label()
            {
                Text = "BeerApp",
                FontSize = 30,
                TextColor = Color.Yellow
            };

            //Create listview
            var listView = new ListView()
            {
                RowHeight = 100,
            };
            
            listView.ItemsSource = new Beer[]
            {
                new Beer{ name = "Grolsch", rating = 9, approval = true, imagePath = "beer" },
                new Beer{ name = "Heineken", rating = 7, approval = true, imagePath = "beer1" },
                new Beer{ name = "Hertog Jan", rating = 7, approval = true, imagePath = "beer2" },
                new Beer{ name = "Dors", rating = 6, approval = true, imagePath = "beer3" },
                new Beer{ name = "Bitburger", rating = 8, approval = true, imagePath = "beer" },
                new Beer{ name = "Jupiler", rating = 4, approval = false, imagePath = "beer1" },
                new Beer{ name = "Hoegarden", rating = 5, approval = false, imagePath = "beer2" },
                new Beer{ name = "Brand", rating = 3, approval = false, imagePath = "beer3" }
            };

            //Bind beer values to listview
            //listView.ItemTemplate = new DataTemplate (typeof(TextCell));
            //listView.ItemTemplate.SetBinding (TextCell.TextProperty, "name");
            listView.ItemTemplate = new DataTemplate(typeof(BeerCell));

            //Selected item / navigation onclick
            listView.ItemSelected += async (sender, e) =>
            {
                var beer = (Beer)e.SelectedItem;
                var beerPage = new BeerDetailPage(beer);
                await Navigation.PushModalAsync(beerPage);
            };

            //Set content
            Content = new StackLayout
            {
                Spacing = 10,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = { lblBeer, listView }
            };
        }
    }

    //Custom made cells, allows multiple data display in listview
    public class BeerCell : ViewCell
    {
        public BeerCell()
        {
            //create image
            var image = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
            };
            //bind image to listview item
            image.SetBinding(Image.SourceProperty, new Binding("imagePath"));
            image.WidthRequest = image.HeightRequest = 100;

            //create layout inside this cell
            var beerLayout = CreateBeerLayout();

            //set layout
            var viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { image, beerLayout }
            };
            View = viewLayout;
        }

        public StackLayout CreateBeerLayout()
        {
            //create name label
            var lblBeerName = new Label()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 12,
            };
            lblBeerName.SetBinding(Label.TextProperty, "name");

            //create rating label
            var lblRating = new Label()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 12,
            };
            lblRating.SetBinding(Label.TextProperty, "rating");

            //set layout
            var beerLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { lblBeerName, lblRating }
            };
            return beerLayout;
        }
    }
}