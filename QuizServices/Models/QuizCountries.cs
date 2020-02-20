using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizCountries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Flag { get; set; }
    }
}
