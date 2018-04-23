using System;
using NameGame.Services;
using NameGameXam.Models;
using NameGameXam.ViewModels;
using Xamarin.Forms;

namespace NameGameXam.GameTypes
{
    public partial class Standard : ContentPage
    {
        QuestionViewModel vm;

        public Standard(QuestionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.vm = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Loading.IsRunning = true;
            await vm.NewQuestion(Question.Type.Standard);
            Loading.IsRunning = false;
            FillContent();
        }

        void FillContent()
        {
            matchThis.Text = vm.question.CorrectProfile.FullName;
            matchThis.IsVisible = true;
            Accuracy.IsVisible = true;

            name1.Text = vm.GetName(0);
            pic1.Source = vm.FormUrl(0);

            name2.Text = vm.GetName(1);
            pic2.Source = vm.FormUrl(1);

            name3.Text = vm.GetName(2);
            pic3.Source = vm.FormUrl(2);

            name4.Text = vm.GetName(3);
            pic4.Source = vm.FormUrl(3);

            name5.Text = vm.GetName(4);
            pic5.Source = vm.FormUrl(4);

            Next.IsVisible = true;
            Mat.IsVisible = true;
            Hint.IsVisible = true;
        }

        async void NewQuestion(object sender, EventArgs args)
        {
            Loading.IsRunning = true;
            Reset();
            await vm.NewQuestion(Question.Type.Standard);
            FillContent();
            Loading.IsRunning = false;
        }

        async void NewMatQuestion(object sender, EventArgs args)
        {
            Loading.IsRunning = true;
            Reset();
            await vm.NewQuestion(Question.Type.Mat);
            FillContent();
            Loading.IsRunning = false;
        }

        void GiveHint(object sender, EventArgs args)
        {
            if(vm.NumberRemoved < 3)
            {
                bool removeDone = false;
                Random random = new Random();
                int remove;

                while (!removeDone)
                {
                    remove = random.Next(vm.question.DisplayedProfiles.Length);
                    if (remove != vm.question.CorrectIndex && HasBeenRemovedAlready(remove))
                    {
                        switch (remove)
                        {
                            case 0:
                                option1.IsVisible = false;
                                vm.NumberRemoved++;
                                removeDone = true;
                                break;
                            case 1:
                                option2.IsVisible = false;
                                vm.NumberRemoved++;
                                removeDone = true;
                                break;
                            case 2:
                                option3.IsVisible = false;
                                removeDone = true;
                                vm.NumberRemoved++;
                                break;
                            case 3:
                                option4.IsVisible = false;
                                removeDone = true;
                                vm.NumberRemoved++;
                                break;
                            case 4:
                                option5.IsVisible = false;
                                removeDone = true;
                                vm.NumberRemoved++;
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
            matchThis.IsVisible = false;
            Accuracy.IsVisible = false;

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
            Mat.IsVisible = false;
            Hint.IsVisible = false;
        }

        void IsCorrect(object sender, EventArgs args)
        {
            Image image = (Image)sender;
            int index = Convert.ToInt32(image.AutomationId);
            if(index-1 == vm.question.CorrectIndex)
            {
                if(HandleGuess(index, Color.Green))
                {
                    vm.NumberCorrect++;
                    vm.TimesGuessed++;   
                }
            }
            else
            {
                if(HandleGuess(index, Color.Red))
                {
                    vm.TimesGuessed++;   
                }
            }

            Accuracy.Text = vm.GetAccuracyText();
            Accuracy.IsVisible = true;
        }

        bool HandleGuess(int selected, Color color)
        {
            switch(selected)
            {
                case 1:
                    if(option1.BackgroundColor == Color.Default)
                    {
                        option1.BackgroundColor = color;
                        name1.IsVisible = true;
                        return true;   
                    }
                    return false;
                case 2:
                    if (option2.BackgroundColor == Color.Default)
                    {
                        option2.BackgroundColor = color;
                        name2.IsVisible = true;
                        return true;
                    }
                    return false;
                case 3:
                    if (option3.BackgroundColor == Color.Default)
                    {
                        option3.BackgroundColor = color;
                        name3.IsVisible = true;
                        return true;
                    }
                    return false;
                case 4:
                    if (option4.BackgroundColor == Color.Default)
                    {
                        option4.BackgroundColor = color;
                        name4.IsVisible = true;
                        return true;
                    }
                    return false;
                case 5:
                    if (option5.BackgroundColor == Color.Default)
                    {
                        option5.BackgroundColor = color;
                        name5.IsVisible = true;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}
