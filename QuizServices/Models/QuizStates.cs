using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizStates
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
    }
}
