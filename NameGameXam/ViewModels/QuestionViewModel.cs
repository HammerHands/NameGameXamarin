﻿using System;
using System.Threading.Tasks;
using NameGame.Services;
using NameGameXam.Models;

namespace NameGameXam.ViewModels
{
    public class QuestionViewModel
    {
        public Question question { get; set; }

        public int TimesGuessed { get; set; }

        public int NumberCorrect { get; set; }

        private NameGameService gameService { get; set; }

        public int NumberRemoved { get; set; }

        public QuestionViewModel()
        {
            TimesGuessed = 0;
            NumberCorrect = 0;
        }

        public string FormUrl(int index)
        {
            return $"http:{question.DisplayedProfiles[index].Headshot.Url}";
        }

        public string GetName(int index)
        {
            return question.DisplayedProfiles[index].FullName;
        }

        public async Task<bool> NewQuestion(Question.Type type)
        {
            question = await QuestionFactory.CreateQuestion(type);
            if (question.CorrectProfile != null)
            {
                return true;
            }
            return false;
        }

        public string GetAccuracyText()
        {
            return $"{NumberCorrect}/{TimesGuessed}";
        }
    }
}
