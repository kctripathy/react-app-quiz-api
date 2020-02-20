using QuizServices.Models;
using QuizServices.ViewModels;
using System;
using System.Linq;

namespace QuizServices.Data.EFCore
{
    public class EfCoreUsersRepository: EfCoreRepository<QuizUsers, QuizContext>
    {
        public readonly QuizContext _context;
        public EfCoreUsersRepository(QuizContext context): base(context)
        {
            _context = context;
        }

        public int Register(QuizUsers user)
        {
            int returnValue = 0;
            try
            {
                QuizUsers qu = new QuizUsers
                {
                    AccountId = user.AccountId,
                    UserEmail = user.UserEmail,
                    UserName = user.UserName,
                    Fullname = user.Fullname,
                    Salt = Security.GetNewSalt(5)
                };
                qu.UserPassword = Security.GetSaltedHashPassword(qu.Salt, user.UserPassword);
                qu.AccessLevel = user.AccessLevel;

                _context.QuizUsers.Add(qu);
                _context.SaveChanges();

                return qu.Id;
            }
            catch 
            {

                returnValue = -1;
            }
            return returnValue;           
        }

        public User Login (UserLogin userLoginCredentials, out int returnValue)
        {
            returnValue = 0;
            User usr = null;
            //DateTime? lastLogin;
            try
            {
                //Check if the user is trying to login with username
                var quizUser = _context.QuizUsers.Where(u => u.UserName == userLoginCredentials.UserName).ToList();
                if (quizUser == null || quizUser.Count.Equals(0))
                {
                    //Check if the user is trying to login with email
                    quizUser = _context.QuizUsers.Where(u => u.UserEmail == userLoginCredentials.UserEmail).ToList();
                    if (quizUser == null || quizUser.Count.Equals(0))
                    {
                        //Check if user is trying to login with the phone number
                        quizUser = _context.QuizUsers.Where(u => u.UserPhone == userLoginCredentials.UserPhone).ToList();
                        if (quizUser == null || quizUser.Count.Equals(0))
                        {
                            returnValue = ReturnConstant.INVALID_USER;
                            return null;
                        }
                    }
                }

                //Get the user information
                QuizUsers user = quizUser[0];

                //Check for password
                string suppliedHasedPassword = Security.GetSaltedHashPassword(user.Salt, userLoginCredentials.UserPassword);
                string actualHashedPassword = user.UserPassword;
                if (!(suppliedHasedPassword.Equals(actualHashedPassword)))
                {
                    //Password mismatch
                    returnValue = ReturnConstant.INVALID_PASSWORD;                    
                }
                else if (user.AllowLogin.Equals(false))
                {
                    //User not allowed to login
                    returnValue = ReturnConstant.USER_NOT_ALLOWED_TO_LOGIN;
                }
                else
                {
                    //Return user informtion
                    returnValue = user.Id;
                    usr = new User
                    {     
                         Id = user.Id,
                        AccountId = user.AccountId,
                        Fullname = user.Fullname,
                        UserEmail = user.UserEmail,
                        AccessLevel=user.AccessLevel,
                        LastLogin = user.LastLoginDate,
                        AccessToken = Security.GetAccessToken()
                    };

                    //Update the access token and last login date in the user table
                    //lastLogin = user.LastLoginDate;

                    user.AccessToken = usr.AccessToken;
                    user.LastLoginDate = DateTime.Now;

                    _context.QuizUsers.Update(user);
                    _context.SaveChanges();

                    //usr.LastLogin = lastLogin;
                }

            }
            catch (Exception ex)
            {
                usr = null;
                throw new Exception(ex.Message);
               
            }

            return usr;
        }
    }
}
