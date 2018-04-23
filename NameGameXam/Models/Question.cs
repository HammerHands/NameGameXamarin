using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameGameXam.Models
{
    public class Question
    {
        public Profile[] DisplayedProfiles { get; set; }
        public Profile CorrectProfile { get; set; }
        public int CorrectIndex { get; set; }
        public enum Type { Standard, Reverse, Team, Mat }
    }
}