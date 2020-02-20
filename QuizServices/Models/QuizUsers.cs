using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizUsers
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? AddressId { get; set; }
        public string Fullname { get; set; }
        public string UserName { get; set; }
        public string UserGender { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }
        public byte? AccessLevel { get; set; }
        public bool? AllowLogin { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Salt { get; set; }
        public string AccessToken { get; set; }

        public QuizAccounts Account { get; set; }
    }
}
