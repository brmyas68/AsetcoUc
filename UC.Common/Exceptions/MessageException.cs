 

namespace UC.Common.Exceptions
{
    public static class MessageException
    {
        public enum Messages
        {
            Sucess, Error,
            RequestFailt, Null, RequestNull,
            NotAccess, NotFoundSecurityCode, NotMatchPassWord,
            ErrorToken, NoFoundToken, 
            ErrorNoMobile, ErrorNoDeleteSms, ErrorNullSms, ErrorSendSms, NotFoundMobile, FoundUserMobile,
            ErrorActiveCode, NotFoundActiveCode,
            NotFoundUser, FoundUser,NoUpdateUser, UserCapacity, IsBlockUser, NoUploadUser, RequestNullUser, FoundUserName, IsNotSystemRole,
            NoMatchSecurityCode,
            NotFoundRole, RequestNullRole,
            NoFindPath, NoDirectory,
            NullAnnex,
            TagUsed

        };

        public enum Status
        {
            Status400 = 400,
            Status200 = 200,
        };
    }
}

