using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizAddresses
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int? StateId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
