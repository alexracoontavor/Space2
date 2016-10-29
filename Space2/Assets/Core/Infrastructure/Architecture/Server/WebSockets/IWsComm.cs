using UniRx;

/// <summary>
/// WebSockets API tools
/// </summary>
namespace Assets.Infrastructure.Architecture.Server.WebSockets
{
    /// <summary>
    /// Interface for WebSockets API
    /// </summary>
    public interface IWsComm
    {
        /// <summary>
        /// Stream that emits string data received through the socket
        /// </summary>
        /// <returns></returns>
        IObservable<string> Receive();

        /// <summary>
        /// Sends string data to socket
        /// </summary>
        /// <param name="data"></param>
        void Send(string data);
    }
}