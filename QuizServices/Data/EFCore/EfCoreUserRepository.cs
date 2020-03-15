using Microsoft.EntityFrameworkCore;
using QuizServices.Models;
using QuizServices.ViewModels;
using System;
using System.Collections.Generic;
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
                    UserPhone = user.UserPhone,
                    ClassId = user.ClassId,
                    SubjectIds = user.SubjectIds,
                    Salt = Security.GetNewSalt(5)

                };
                qu.UserPassword = Security.GetSaltedHashPassword(qu.Salt, user.UserPassword);
                qu.AccessLevel = user.AccessLevel;

                _context.QuizUsers.Add(qu);
                _context.SaveChanges();

                return qu.Id;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
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
                var quizUser = _context.QuizUsers.AsNoTracking().Where(u => u.UserName == userLoginCredentials.UserName).ToList();
                if (quizUser == null || quizUser.Count.Equals(0) || string.IsNullOrEmpty(userLoginCredentials.UserName))
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
                        ClassId = user.ClassId,
                        Fullname = user.Fullname,
                        UserEmail = user.UserEmail,
                        AccessLevel=user.AccessLevel,
                        LastLoginDate = user.LastLoginDate,
                        AccessToken = Security.GetAccessToken(),
                        AccountName = GetAcountName(user.AccountId)
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

        private string GetAcountName(int accountId)
        {
            string accName = string.Empty;
            try
            {
                var account = _context.QuizAccounts.Where(a => a.Id == accountId).ToList();
                if (account.Count > 0)
                {
                    accName= account[0].AccountName;
                }
                return accName;
            }
            catch (Exception ex)
            {
                return "";
               // throw;
            }
        }

        internal int UpdateUser(QuizUsers user)
        {
            try
            {
                QuizUsers user2update = new QuizUsers();
                var quizUsers = _context.QuizUsers.AsNoTracking().Where(u => u.Id == user.Id).ToList();
                user2update = quizUsers[0];

                QuizUsers qu = new QuizUsers();
                qu.Id = user.Id;
                qu.Fullname = user.Fullname;
                qu.ClassId = user.ClassId;
                qu.AccessLevel = user.AccessLevel;
                qu.AllowLogin = user.AllowLogin;
                qu.UserPhone = user.UserPhone;

                // check if user supplied a new email id
                if (user2update.UserEmail != user.UserEmail)
                {
                    var quizUserEmail = _context
                                    .QuizUsers
                                    .AsNoTracking()
                                    .Where(u => u.UserEmail == user.UserEmail).ToList();

                    if (quizUserEmail.Count > 0)
                    {
                        return -2; // email already exists 
                    }
                    else if (user2update.UserEmail == user.UserEmail)
                    {
                        qu.UserEmail = user2update.UserEmail;
                    }
                    else
                    {
                        qu.UserEmail = user.UserEmail;
                    }
                }
                else
                {
                    qu.UserEmail = user.UserEmail;
                }

                //check if user has changed the password
                if (user.UserPassword != user2update.UserPassword)
                {
                    string Salt = Security.GetNewSalt(5);
                    qu.Salt = Salt;
                    qu.UserPassword = Security.GetSaltedHashPassword(Salt, user.UserPassword);
                }
                else
                {
                    qu.Salt = user2update.Salt;
                    qu.UserPassword = user2update.UserPassword;
                }

                //qu.AllowLogin = user2update.AllowLogin;
                qu.LastLoginDate = user2update.LastLoginDate;
                qu.UserName = user2update.UserName;
                qu.AccountId = user2update.AccountId;                 

                _context.Update(qu);
                _context.SaveChanges();

                return qu.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return -1;
            }
        }

        public List<User> GetAllUsersByAccountId(int accountId)
        {
            //var usersList = _context.QuizUsers
            //                    .Where(u => u.AccountId == accountId)
            //                    .ToList();

            
            var usersList = _context
                                   .Users
                                   .FromSql($"GetAllUsersByAccountId {accountId}")
                                   .ToList();

            return usersList;
        }
    }
}
