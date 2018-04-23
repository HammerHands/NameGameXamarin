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

        public int TimesGuessed { get; set; }

        public int NumberCorrect { get; set; }

        private NameGameService gameService { get; set; }

        private int percentage { get; set; }

        private int NumberRemoved { get; set; }

        public Standard()
        {
            InitializeComponent();
            TimesGuessed = 0;
            NumberCorrect = 0;
            NumberRemoved = 0;
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

            Next.IsVisible = true;
            Hint.IsVisible = true;
        }

        async void NewQuestion(object sender, EventArgs args)
        {
            Loading.IsRunning = true;
            Reset();
            question = await QuestionFactory.CreateQuestion(Question.Type.Standard);
            FillContent();
            Loading.IsRunning = false;
        }

        void GiveHint(object sender, EventArgs args)
        {
            if(NumberRemoved < 3)
            {
                bool removeDone = false;
                Random random = new Random();
                int remove;

                while (!removeDone)
                {
                    remove = random.Next(question.DisplayedProfiles.Length);
                    if (remove != question.CorrectIndex && HasBeenRemovedAlready(remove))
                    {
                        switch (remove)
                        {
                            case 0:
                                option1.IsVisible = false;
                                NumberRemoved++;
                                removeDone = true;
                                break;
                            case 1:
                                option2.IsVisible = false;
                                NumberRemoved++;
                                removeDone = true;
                                break;
                            case 2:
                                option3.IsVisible = false;
                                removeDone = true;
                                NumberRemoved++;
                                break;
                            case 3:
                                option4.IsVisible = false;
                                removeDone = true;
                                NumberRemoved++;
                                break;
                            case 4:
                                option5.IsVisible = false;
                                removeDone = true;
                                NumberRemoved++;
                                break;
                        }
                    }
                }   
            }
        }

        bool HasBeenRemovedAlready(int option)
        {
            switch(option)
            {
                case 0:
                    return option1.IsVisible;
                case 1:
                    return option2.IsVisible;
                case 2:
                    return option3.IsVisible;
                case 3:
                    return option4.IsVisible;
                case 4:
                    return option5.IsVisible;
                default:
                    return false;
            }
        }

        void Reset()
        {
            option1.BackgroundColor = this.BackgroundColor;
            option1.IsVisible = true;
            name1.IsVisible = false;

            option2.BackgroundColor = Color.Default;
            option2.IsVisible = true;
            name2.IsVisible = false;

            option3.BackgroundColor = Color.Default;
            option3.IsVisible = true;
            name3.IsVisible = false;

            option4.BackgroundColor = Color.Default;
            option4.IsVisible = true;
            name4.IsVisible = false;

            option5.BackgroundColor = Color.Default;
            option5.IsVisible = true;
            name5.IsVisible = false;

            Next.IsVisible = false;
        }

        void IsCorrect(object sender, EventArgs args)
        {
            Image image = (Image)sender;
            int index = Convert.ToInt32(image.AutomationId);
            if(index-1 == question.CorrectIndex)
            {
                HandleGuess(index, Color.Green);
                NumberCorrect++;
                TimesGuessed++;
            }
            else
            {
                HandleGuess(index, Color.Red);
                TimesGuessed++;
            }

            percentage = (int)Math.Round((decimal)NumberCorrect / (decimal)TimesGuessed * 100);
            Accuracy.Text = NumberCorrect + "/" + TimesGuessed + "(" + percentage + "%)";
            Accuracy.IsVisible = true;
        }

        void HandleGuess(int selected, Color color)
        {
            switch(selected)
            {
                case 1:
                    if(option1.BackgroundColor == Color.Default)
                    {
                        option1.BackgroundColor = color;
                        name1.IsVisible = true;
                        break;
                    }
                    break;
                case 2:
                    if (option2.BackgroundColor == Color.Default)
                    {
                        option2.BackgroundColor = color;
                        name2.IsVisible = true;
                        break;
                    }
                    break;
                case 3:
                    if (option3.BackgroundColor == Color.Default)
                    {
                        option3.BackgroundColor = color;
                        name3.IsVisible = true;
                        break;
                    }
                    break;
                case 4:
                    if (option4.BackgroundColor == Color.Default)
                    {
                        option4.BackgroundColor = color;
                        name4.IsVisible = true;
                        break;
                    }
                    break;
                case 5:
                    if (option5.BackgroundColor == Color.Default)
                    {
                        option5.BackgroundColor = color;
                        name5.IsVisible = true;
                        break;
                    }
                    break;
            }
        }
    }
}
