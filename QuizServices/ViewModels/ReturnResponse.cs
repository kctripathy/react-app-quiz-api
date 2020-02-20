using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class ReturnResponse
    {
        public object Result { get; set; }

        public ReturnStatus Status { get; set; }

        public static ReturnResponse GetSuccessStatus(Object obj)
        {
            ReturnResponse r = new ReturnResponse();
            ReturnStatus s = new ReturnStatus("1","Success");

            r.Result = obj;
            r.Status = s;

            return r;
        }

        public static ReturnResponse GetFailureStatus(Object obj)
        {
            ReturnResponse r = new ReturnResponse();
            ReturnStatus s = new ReturnStatus("-1", "Failure");

            r.Result = obj;
            r.Status = s;

            return r;
        }


        public static ReturnResponse Get(int returnCode)
        {
            ReturnResponse returnValue = new ReturnResponse();            
            switch (returnCode)
            {
                case ReturnConstant.FAILURE:
                    returnValue.Status = new ReturnStatus(ReturnConstant.FAILURE.ToString(), ReturnConstant.FAILURE_MESSAGE);
                    break;
                case ReturnConstant.INVALID_USER:
                    returnValue.Status  = new ReturnStatus(ReturnConstant.INVALID_USER.ToString(),ReturnConstant.INVALID_USER_MESSAGE);
                    break;
                case ReturnConstant.INVALID_PASSWORD:
                    returnValue.Status = new ReturnStatus(ReturnConstant.INVALID_PASSWORD.ToString(),ReturnConstant.INVALID_PASSWORD_MESSAGE);
                    break;
                case ReturnConstant.USER_NOT_ALLOWED_TO_LOGIN:
                    returnValue.Status = new ReturnStatus(ReturnConstant.USER_NOT_ALLOWED_TO_LOGIN.ToString(),ReturnConstant.USER_NOT_ALLOWED_TO_LOGIN_MESSAGE);
                    break;

                //CLASSES AND SUBJECTS
                case ReturnConstant.CLASS_SUBJECT_NOT_FOUND_QUESTION_AVAILABLE:
                    returnValue.Status = new ReturnStatus(ReturnConstant.CLASS_SUBJECT_NOT_FOUND_QUESTION_AVAILABLE.ToString(), ReturnConstant.CLASS_SUBJECT_NOT_FOUND_QUESTION_AVAILABLE_MESSAGE);
                    break;
                case ReturnConstant.CLASS_SUBJECT_ALL_NOT_FOUND:
                    returnValue.Status = new ReturnStatus(ReturnConstant.CLASS_SUBJECT_ALL_NOT_FOUND.ToString(), ReturnConstant.CLASS_SUBJECT_ALL_NOT_FOUND_MESSAGE);
                    break;

                default:
                    returnValue.Status = new ReturnStatus(ReturnConstant.INVALID_OPERATION.ToString(),ReturnConstant.INVALID_OPERATION_MESSAGE);
                    break;
            }
            returnValue.Result = "[]";
            return returnValue;
        }



        public static ReturnResponse Get(User user)
        {
            ReturnResponse returnValue = new ReturnResponse();
            ReturnStatus returnStatus = new ReturnStatus(ReturnConstant.SUCCESS.ToString(), ReturnConstant.SUCCESS_MESSAGE);
            returnValue.Result = user;
            returnValue.Status = returnStatus;
            return returnValue;
        }

        public static ReturnResponse Get(int returnCode, Object obj)
        {
            ReturnResponse returnValue = new ReturnResponse();
            ReturnStatus returnStatus = new ReturnStatus(returnCode.ToString(), (returnCode > 0 ? ReturnConstant.SUCCESS_MESSAGE : ReturnConstant.FAILURE_MESSAGE));
            returnValue.Status = returnStatus;
            returnValue.Result = obj;
            return returnValue;
        }

       
    }

    public class ReturnStatus
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ReturnStatus(string code, string messsage)
        {
            this.Code = code;
            this.Message = messsage;

        }

        public ReturnStatus()
        {
        }
    }

   public static class ReturnConstant
    {

        public const int SUCCESS = 1;
        public const string SUCCESS_MESSAGE = "SUCCESS";

        public const int FAILURE = 0;
        public const string FAILURE_MESSAGE = "FAILURE";

        public const int INVALID_USER = -10;
        public const string INVALID_USER_MESSAGE = "USER NOT FOUND";

        public const int INVALID_PASSWORD = -11;
        public const string INVALID_PASSWORD_MESSAGE= "INVALID PASSWORD";

        public const int USER_NOT_ALLOWED_TO_LOGIN = -12;
        public const string USER_NOT_ALLOWED_TO_LOGIN_MESSAGE = "USER NOT ALLOWED TO LOGIN";

        public const int INVALID_OPERATION= -100;
        public const string INVALID_OPERATION_MESSAGE= "INVALID OPERATION";

        public const int CLASS_SUBJECT_NOT_FOUND_QUESTION_AVAILABLE = -200;
        public const string CLASS_SUBJECT_NOT_FOUND_QUESTION_AVAILABLE_MESSAGE = "Classes and subjects not found for the account for which any question available";

        public const int CLASS_SUBJECT_ALL_NOT_FOUND = -210;
        public const string CLASS_SUBJECT_ALL_NOT_FOUND_MESSAGE = "Classes and subjects not found for the given account";

    }
}
