using Assets.Infrastructure.Architecture;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Server.WebSockets;
using Assets.Infrastructure.CoreTools.Instanced.JsonFormatter;
using Assets.Infrastructure.Modules.HGS.HGS_Login_Module.API;
using Assets.Infrastructure.Modules.HGS.HGS_Module.BaseClasses;

namespace Assets.Infrastructure.Modules.HGS.HGS_Login_Module.Actions
{
    public static class HGS_Login_ActionsCreator
    {
        public static void ProcessServerResponse(IResponseData response)
        {
            ModuluxRoot.Logger.Log("HGS_Login_ActionsCreator processing server response " + response);

            if (response is HGS_Login_API.SocketOpenedResponse)
            {
                var r = response as HGS_Login_API.SocketOpenedResponse;
                ModuluxRoot.Store.Dispatch(new SocketOpenedAction() {Id = r.data.id});

                var call = new HGS_Login_API.LoginCall(new HGS_Login_API.LoginNewDeviceData() {deviceId = System.Guid.NewGuid().ToString()});
                ModuluxRoot.IoC.Get<IWsComm>().Send(ModuluxRoot.IoC.Get<IJsonFormatter>().ToJsonString(call));
            }
            else if (response is HGS_Login_API.LoginSuccessResponse)
            {
                ModuluxRoot.Logger.Log("Dispatching login success");

                ModuluxRoot.Store.Dispatch(new LoginSucceedAction());
            }
        }

        public static void StartLoginProcess()
        {
            ModuluxRoot.Store.Dispatch(new LoginStartAction());

            var call = new HGS_Login_API.OpenSocketCall(new HGS_Login_API.TakeSessionData() { clientUUID = System.Guid.NewGuid().ToString() });
            ModuluxRoot.IoC.Get<IWsComm>().Send(ModuluxRoot.IoC.Get<IJsonFormatter>().ToJsonString(call));
        }
    }
}
