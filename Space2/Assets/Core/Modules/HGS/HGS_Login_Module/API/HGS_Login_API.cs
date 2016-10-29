using System;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_Login_Module.API
{
    public class HGS_Login_API
    {
        //OUT
        [Serializable]
        public class OpenSocketCall
        {
            public OpenSocketCall(TakeSessionData data, string to = "", string type = "com.akamon.jreactive.sessions.TakeSessionRequest")
            {
                this.type = type;
                this.to = to;
                this.data = data;
            }

            public string type;
            public string to;
            public TakeSessionData data;
        }

        [Serializable]
        public class LoginCall
        {
            public LoginCall(LoginNewDeviceData data, string to = "VivaLoginService", string type = "com.akamon.gameapi.login.LoginNewDeviceRequest")
            {
                this.type = type;
                this.to = to;
                this.data = data;
            }

            public string type;
            public string to;
            public LoginNewDeviceData data;
        }

        [Serializable]
        public class TakeSessionData : BaseMessageData
        {
            public string clientUUID;   //  random UID, must be sent but is unused
            public string id; //  saved session id, if has any (1 minute timeout)
        }

        [Serializable]
        public class LoginNewDeviceData
        {
            public string clientTypeId = "viva_client_android";
            public string deviceId;     //  random UID - this is the permanent guest user key (save locally)
            public string platform = "ANDROID"; //  probably just fr analytics, can be empty 
            public EmptyData userContext = new EmptyData();
            /*
            FACEBOOK,
            ANDROID,
            IOS,
            PORTAL,
            MOBILE*/
        }

        //IN
        [Serializable]
        public class UserDataResponse : WsResponse
        {
            public UserData data;
        }

        [Serializable]
        public class LoginSuccessResponse : UserDataResponse
        {
            public string result;       // SUCCESS, INVALID_CREDENTIALS, INVALID_USER_CONTEXT, UNABLE_TO_CONNECT_WITH_APP
        }

        [Serializable]
        public class SocketOpenedResponse : UserDataResponse
        {

        }

        [Serializable]
        public class TakeSessionResponse
        {
            public string id;       // if null, connection failed. Else, save this to identify session when reconnecting
        }

        [Serializable]
        public class UserData
        {
            public string id;
            public string name;
            public object information;
        }
    }
}