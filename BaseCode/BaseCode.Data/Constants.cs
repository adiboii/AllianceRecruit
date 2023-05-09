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
            public const string Bearer = "Bearer";

            // Messages
            public const string BadRequest = "Bad Request";
            public const string InvalidRole = "Invalid Role";
        }
        public class Roles
        {
            public const string Admin = "Administrator";
            public const string User = "User";
        }

        public class User
        {
            public const string Empty = "Empty username or password.";
            public const string InvalidUserNamePassword = "Invalid username or password.";
            public const string Success = "Successfull.";
        }

        public class Subject {
            public const int PageSize = 15;

            // Messages
            public const string SubjectSuccessAdd = "Subject added successfully.";
            public const string SubjectDoesNotExist = "Subject does not exist.";
            public const string SubjectSuccessDelete = "Subject is deleted successfully.";
            public const string SubjectExists = "Subject already exists.";
            public const string SubjectNameAlreadyTaken = "Subject name already taken.";
            public const string SubjectTooMuchNumberOfCredits = "Subject number of credits should not be more than 5 and not less than or equal to 0.";
            public const string SubjectDescriptionTooLong = "Subject description characters should not exceed 100.";
            public const string SubjectNameTooLong = "Subject name characters should not exceed 30.";
            public const string SubjectCategoryNotFound = "Subject category should either be `major` or `minor`";
            public const string SubjectUpdateSuccess = "Subject updated successfully.";
        }

        public class PersonalInformation
        {
            public const int PageSize = 15;
            // Success Messages
            public const string PersonalInformationSuccessAdd = "Personal information added successfully.";
            public const string PersonalInformationSuccessDelete = "Personal information removed successfully.";
            public const string PersonalInformationUpdateSuccess = "Personal information updated successfully.";

            // Error Messages
            public const string PersonalInformationDoesNotExist = "Personal information does not exist.";
            public const string PersonalInformationExists = "Personal information already exists.";
            public const string PersonalInformationFirstNameTooLong = "First Name should not exceed 250 characters.";
            public const string PersonalInformationLastNameTooLong = "Last Name should not exceed 250 characters.";
            public const string PersonalInformationPhoneNumberTooLong = "Phone Number should not exceed 11 characters.";
            public const string PersonalInformationAddressTooLong = "Address should not exceed 500 characters.";
        }

        public class JobRequirement
        {
            public const int PageSize = 15;
            // Success Messages
            public const string JobRequirementSuccessAdd = "Job Requirement added successfully.";
            public const string JobRequirementSuccessDelete = "Job Requirement removed successfully.";
            public const string JobRequirementSuccessUpdate = "Job Requirement updated successfully.";

            // Error Messages
            public const string JobRequirementDoesNotExist = "Job Requirement does not exist.";
            public const string JobRequirementExists = "Job Requirement already exists.";
        }

        public class JobDescription
        {
            public const int PageSize = 15;
            // Success Messages
            public const string JobDescriptionSuccessAdd = "Job Description added successfully.";
            public const string JobDescriptionSuccessDelete = "Job Description removed successfully.";
            public const string JobDescriptionSuccessUpdate = "Job Description updated successfully.";

            // Error Messages
            public const string JobDescriptionDoesNotExist = "Job Description does not exist.";
            public const string JobDescriptionExists = "Job Description already exists.";
        }

        public class Attachment
        {
            public const int PageSize = 15;
            // Success Messages
            public const string AttachmentSuccessAdd = "Attachment added successfully.";
            public const string AttachmentSuccessDelete = "Attachment removed successfully.";
            public const string AttachmentSuccessUpdate = "Attachment updated successfully.";

            // Error Messages
            public const string AttachmentDoesNotExist = "Attachment does not exist.";
            public const string AttachmentExists = "Attachment already exists.";
        }

        public class Job
        {
            public const int PageSize = 15;
            // Success Messages
            public const string JobSuccessAdd = "Job added successfully.";
            public const string JobSuccessDelete = "Job removed successfully.";
            public const string JobSuccessUpdate = "Job updated successfully.";

            // Error Messages
            public const string JobDoesNotExist = "Job does not exist.";
            public const string JobExists = "Job already exists.";
        }

        public class Application
        {
            public const int PageSize = 15;
            // Success Messages
            public const string ApplicationSuccessAdd = "Application added successfully.";
            public const string ApplicationSuccessDelete = "Application removed successfully.";
            public const string ApplicationSuccessUpdate = "Application updated successfully.";

            // Error Messages
            public const string ApplicationDoesNotExist = "Application does not exist.";
            public const string ApplicationExists = "Application already exists.";
        }
    }
}
