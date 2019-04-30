namespace BaseCode.Data
{
    public static class Constants
    {
        public class Token
        {
            public const string Issuer = "BaseCode:Issuer";
            public const string Audience = "BaseCode:Audience";            
            public const string SecretKey = "BaseCode:AuthSecretKey";
            public const string POST = "POST";
            public const string TokenPath = "/api/token";
            public const string Username = "username";
            public const string Password = "password";
            public const string RefreshToken = "refresh_token";
            public const string UserID = "user_id";
        }

        public class SortDirection
        {
            public const string Ascending = "Ascending";
            public const string Descending = "Descending";
        }

        public class Claims
        {
            public const string Id = "userId";
            public const string UserName = "userName";
            public const string Role = "userRole";
        }

        public class ClaimTypes
        {
            public const string UserName = "user_name";
            public const string ID = "id";
            public const string UserId = "user_id";
            public const string FullName = "full_name";
        }

        public class Common
        {
            public const string BaseCode = "BaseCode";
            public const string OAuth = "/oauth";
            public const string Client = "Client";
            public const string ClientID = "ClientID";
            public const string ClientSecret = "ClientSecret";
            public const string JSONContentType = "application/json";

            // Messages
            public const string BadRequest = "Bad Request";
        }

        public class User
        {
            public const string InvalidUserNamePassword = "Invalid username or password.";
        }

        public class Student
        {
            // Sort Keys
            public const string StudentHeaderId = "stud_id";
            public const string StudentHeaderName = "stud_name";
            public const string StudentHeaderEmail = "stud_email";
            public const string StudentHeaderClass = "stud_class";
            public const string StudentHeaderEnrollYear = "stud_enrollYear";
            public const string StudentHeaderCity = "stud_city";
            public const string StudentHeaderCountry = "stud_country";

            // Messages
            public const string StudentNameExists = "Student name already exists";
            public const string StudentEntryInvalid = "Student entry is not valid!";
            public const string StudentNotExist = "Student does not exist.";       
            public const string StudentDoesNotExists = "Student does not exist.";
            public const string StudentSuccessAdd = "Student added successfully.";
            public const string StudentSuccessEdit = "Student is updated successfully.";
            public const string StudentSuccessDelete = "Student is deleted successfully.";
        }
    }
}
