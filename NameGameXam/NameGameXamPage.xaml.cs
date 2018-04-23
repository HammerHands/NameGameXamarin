using Xamarin.Forms;

namespace NameGameXam
{
    public partial class NameGameXamPage : ContentPage
    {
        public NameGameXamPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GameTypes.Standard(new ViewModels.StandardViewModel()));
        }
    }
}
