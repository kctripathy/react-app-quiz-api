using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{

    public partial class User
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public byte? AccessLevel { get; set; }
        public string AccessToken { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public partial class UserLogin
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }

    }

}
