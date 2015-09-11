using System;

using Xamarin.Forms;

namespace BierApp
{
    public class BeerDetailPage : ContentPage
    {
        public static int MAX = 10;
        public static int MIN = 1;
        public Beer beer;
        public Label lblRating;
        public Image imgApprove;

        public BeerDetailPage(Beer beer)
        {
            this.beer = beer;
            //Name beer
            var lblName = new Label()
            { 
                Text = beer.name,
                FontSize = 30,
                TextColor = Color.Yellow
            };

            //Create rating layout (rating/buttons)
            var ratingLayout = CreateRatingLayout();

            //Approval image
            imgApprove = new Image()
            {
                Aspect = Aspect.AspectFit,
                WidthRequest = 200,
                HeightRequest = 200,
            };
            imgApprove.Source = (beer.approval) ? ImageSource.FromFile("like.png") : ImageSource.FromFile("nlike.png");

            //Set content
            Content = new StackLayout
            {
                Spacing = 10,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,					
                Children = { lblName, ratingLayout, imgApprove }
            };
        }

        Color beerColor;

        public StackLayout CreateRatingLayout()
        {
            beerColor = (beer.rating > 5) ? Color.Green : Color.Red;

            //Rating label
            lblRating = new Label()
            {
                Text = "Rating: " + beer.rating.ToString() + "/10",
                FontSize = 30,
                TextColor = beerColor
            };

            //Plus image
            var imgPlus = new Image()
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = 50,
                WidthRequest = 50,
                ClassId = "plus",
            };

            //Add gesture
            imgPlus.Source = ImageSource.FromFile("plus");	
            var tapGestureRecognizer = new TapGestureRecognizer();
            RatingChanged(tapGestureRecognizer);
            imgPlus.GestureRecognizers.Add(tapGestureRecognizer);
			
            //Min image
            var imgMin = new Image()
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = 50,
                WidthRequest = 50,
                ClassId = "min",
            };

            //Add gesture
            imgMin.Source = ImageSource.FromFile("min");
            tapGestureRecognizer = new TapGestureRecognizer();
            RatingChanged(tapGestureRecognizer);
            imgMin.GestureRecognizers.Add(tapGestureRecognizer);

            //Set layout
            var layout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { lblRating, imgPlus, imgMin }
            };
            return layout;
        }

        //Rating gesture
        public void RatingChanged(TapGestureRecognizer tapGestureRecognizer)
        {
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                //Check which button is pressed
                Image pressedImage = (Image)sender;
                beer.rating = (pressedImage.ClassId.Equals("plus")) ? beer.rating += 1 : beer.rating -= 1;

                //Bounds
                if (beer.rating > 10)
                    beer.rating = 10;
                if (beer.rating < 1)
                    beer.rating = 0;
                
                //Change textcolor/image depending on rating
                    beerColor = (beer.rating > 5) ? Color.Green : Color.Red;

                lblRating.TextColor = beerColor;
                    beer.approval = (beerColor.Equals(Color.Green)) ? true : false;
                imgApprove.Source = (beer.approval) ? ImageSource.FromFile("like.png") : ImageSource.FromFile("nlike.png");
                lblRating.Text = "Rating: " + beer.rating.ToString() + "/10";
            };
        }
    }
}