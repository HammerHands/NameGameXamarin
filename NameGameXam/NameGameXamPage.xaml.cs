using System;
using Xamarin.Forms;

namespace NameGameXam
{
    public partial class NameGameXamPage : ContentPage
    {
        public NameGameXamPage()
        {
            InitializeComponent();
        }

        async void StandardClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameTypes.Standard(new ViewModels.QuestionViewModel()));
        }

        async void ReverseClick(object sender, EventArgs args)
        {
            //await Navigation.PushAsync(new GameTypes.Reverse(new ViewModels.QuestionViewModel()));
        }
    }
}
