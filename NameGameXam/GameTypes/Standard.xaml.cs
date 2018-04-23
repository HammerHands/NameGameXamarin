using System;
using System.Linq;
using NameGame.Services;
using NameGameXam.Models;
using Xamarin.Forms;

namespace NameGameXam.GameTypes
{
    public partial class Standard : ContentPage
    {
       
        public Question question { get; set; }

        private NameGameService gameService { get; set; }

        public Standard()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Loading.IsRunning = true;
            question = await QuestionFactory.CreateQuestion(Question.Type.Standard);
            FillContent();
            Loading.IsRunning = false;
        }

        void FillContent()
        {
            matchThis.Text = question.CorrectProfile.FullName;

            name1.Text = question.DisplayedProfiles[0].FullName;
            pic1.Source = "http:" + question.DisplayedProfiles[0].Headshot.Url;

            name2.Text = question.DisplayedProfiles[1].FullName;
            pic2.Source = "http:" + question.DisplayedProfiles[1].Headshot.Url;

            name3.Text = question.DisplayedProfiles[2].FullName;
            pic3.Source = "http:" + question.DisplayedProfiles[2].Headshot.Url;

            name4.Text = question.DisplayedProfiles[3].FullName;
            pic4.Source = "http:" + question.DisplayedProfiles[3].Headshot.Url;

            name5.Text = question.DisplayedProfiles[4].FullName;
            pic5.Source = "http:" + question.DisplayedProfiles[4].Headshot.Url;
        }

        void IsCorrect(object sender, EventArgs args)
        {
            Image image = (Image)sender;
            int index = Convert.ToInt32(image.AutomationId);
            if(index == question.CorrectIndex)
            {
                setColor(index, Color.Green);
            }
            else
            {
                setColor(index, Color.Red);
            }
        }

        void setColor(int selected, Color color)
        {
            switch(selected)
            {
                case 1:
                    option1.BackgroundColor = color;
                    name1.IsVisible = true;
                    break;
                case 2:
                    option2.BackgroundColor = color;
                    name2.IsVisible = true;
                    break;
                case 3:
                    option3.BackgroundColor = color;
                    name3.IsVisible = true;
                    break;
                case 4:
                    option4.BackgroundColor = color;
                    name4.IsVisible = true;
                    break;
                case 5:
                    option5.BackgroundColor = color;
                    name5.IsVisible = true;
                    break;
            }
        }
    }
}
