using Assets.Infrastructure.Architecture.Server.RESTful.Tools;
using UniRx;

/// <summary>
/// RESTful API tools
/// </summary>
namespace Assets.Infrastructure.Architecture.Server.RESTful
{
    /// <summary>
    /// Interface for RESTful API calling
    /// </summary>
    public interface IApiCaller
    {
        /// <summary>
        /// Calls the API and returns an answer as string
        /// </summary>
        /// <typeparam name="TCallRawDataType">Type of data expected from server. Usually is string</typeparam>
        /// <typeparam name="TReplyDataType">Type of data that this Call returns</typeparam>
        /// <param name="request">an ApiRequest containing all the data needed to dispatch a request and process the reply</param>
        /// <returns>Observable stream emitting replies</returns>
        IObservable<TReplyDataType> Call<TCallRawDataType, TReplyDataType>(
            ApiRequest<TCallRawDataType, TReplyDataType> request) where TCallRawDataType : class;
    }
}