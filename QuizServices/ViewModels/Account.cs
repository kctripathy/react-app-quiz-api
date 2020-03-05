using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class Account
    {
        public int id { get; set; }
        public string accountName { get; set; }
        public string contactName { get; set; }
        public string contactPhone { get; set; }
        public string contactEmail { get; set; }
        public string loginPassword { get; set; }
        public string classSubjects { get; set; }
    }
}
