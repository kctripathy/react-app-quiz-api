using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuizServices
{
    public class Security
    {
        public static string GetNewSalt(int saltLength = 4)
        {
            string guidResult = Guid.NewGuid().ToString().Replace("-","");
            if (saltLength<=0 || saltLength >= guidResult.Length)
            {
                throw new ArgumentException(string.Format("Length must be between 1 to {0}", guidResult.Length));
            }
            return guidResult.Substring(0,saltLength);            
        }

        public static string GetSaltedHashPassword(string salt, string password)
        {
            string sourceText = string.Concat(salt.Trim(), password.Trim());

            //Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding ue = new UnicodeEncoding();

            //Retrieve a byte array based on the source text
            Byte[] byteSourceText = ue.GetBytes(sourceText);

            //Instantiate an MD5 Provider object
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //Compute the hash value from the source
            Byte[] byteHash = md5.ComputeHash(byteSourceText);

            //And convert it to String format for return
            return Convert.ToBase64String(byteHash);
        }

        internal static string GetAccessToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        //Public Function GenerateHash(ByVal SourceText As String, SaltText As String) As String
        //    Dim tempsalt As String = Convert.ToString(SaltText).Trim()
        //    Return GenerateHash(String.Concat(SourceText, tempsalt))
        //End Function
        //Public Function GenerateHash(ByVal SourceText As String) As String
        //    'Create an encoding object to ensure the encoding standard for the source text
        //    Dim Ue As New UnicodeEncoding()
        //    'Retrieve a byte array based on the source text
        //    Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
        //    'Instantiate an MD5 Provider object
        //    Dim Md5 As New MD5CryptoServiceProvider()
        //    'Compute the hash value from the source
        //    Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
        //    'And convert it to String format for return
        //    Return Convert.ToBase64String(ByteHash)
        //End Function
    }
}
