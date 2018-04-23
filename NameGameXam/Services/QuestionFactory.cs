using NameGameXam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameGame.Services
{
    public static class QuestionFactory
    {
        public static async Task<Question> CreateQuestion(Question.Type questionType)
        {
            NameGameService gameService = new NameGameService();
            Profile[] allProfiles = await gameService.GetProfiles();

            //Check to see if null, less than 5 returned, or less than five valid headshots
            if (allProfiles == null || allProfiles.Length < 5 || allProfiles.Where(p => p.Headshot.Url != null).Count() < 5)
            {
                throw new InvalidOperationException();
            }

            Question question = new Question();
            question.DisplayedProfiles = new Profile[5];
            Random numGenerator = new Random();
            Profile toAdd;
            int curr = 0;
            //Filter profiles out of Displayed based on question type
            if (questionType == Question.Type.Standard || questionType == Question.Type.Reverse)
            {
                while (curr != 5)
                {
                    toAdd = allProfiles[numGenerator.Next(allProfiles.Length)];

                    if (!question.DisplayedProfiles.Contains(toAdd) && toAdd.Headshot.Url != null)
                    {
                        //valid headshot and person isn't in list
                        //add to list
                        question.DisplayedProfiles[curr] = toAdd;
                        curr++;
                    }
                }
                //Randomly Select Correct Profile
                question.CorrectIndex = numGenerator.Next(question.DisplayedProfiles.Length);
                question.CorrectProfile = question.DisplayedProfiles[question.CorrectIndex];
                return question;
            }
            else if (questionType == Question.Type.Team)
            {
                //Check to see if enough profiles with Job Titles for a question
                if (allProfiles.Where(p => p.JobTitle != null).Count() < 5)
                {
                    throw new InvalidOperationException();
                }
                while (curr != 5)
                {
                    toAdd = allProfiles[numGenerator.Next(allProfiles.Length)];

                    if (!question.DisplayedProfiles.Contains(toAdd) && toAdd.Headshot.Url != null && toAdd.JobTitle != null)
                    {
                        //valid headshot and person isn't in list
                        //add to list
                        question.DisplayedProfiles[curr] = toAdd;
                        curr++;
                    }
                }
                //Randomly Select Correct Profile
                question.CorrectIndex = numGenerator.Next(question.DisplayedProfiles.Length);
                question.CorrectProfile = question.DisplayedProfiles[question.CorrectIndex];
                return question;
            }
            else if (questionType == Question.Type.Mat)
            {
                //Check to see if enough Mat(t)s for a question
                if (allProfiles.Where(p => p.FullName.StartsWith("Mat")).Count() < 5)
                {
                    throw new InvalidOperationException();
                }
                while (curr != 5)
                {
                    toAdd = allProfiles[numGenerator.Next(allProfiles.Length)];

                    if (!question.DisplayedProfiles.Contains(toAdd) && toAdd.Headshot.Url != null && toAdd.FirstName.StartsWith("Mat", StringComparison.CurrentCulture))
                    {
                        //valid headshot and person isn't in list
                        //add to list
                        question.DisplayedProfiles[curr] = toAdd;
                        curr++;
                    }
                }
                //Randomly Select Correct Profile
                question.CorrectIndex = numGenerator.Next(question.DisplayedProfiles.Length);
                question.CorrectProfile = question.DisplayedProfiles[question.CorrectIndex];
                return question;
            }
            throw new InvalidOperationException();
        }
    }
}