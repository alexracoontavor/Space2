using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Infrastructure.Architecture.Logger;
using Assets.Infrastructure.Architecture.Modulux;
using UniRx;
namespace Assets.Infrastructure.Architecture.Server.WebSockets
{
    /// <summary>
    /// Default implementation of WebSockets API communicator. This is a temporary implementation and needs to be improved.
    /// </summary>
    public class WsComm : IWsComm
    {
        private readonly WebSocket _webSocket;
        private readonly Subject<string> _replySubject = new Subject<string>();
        private IObservable<Unit> _connectedStream;
        private List<string> _todoWhenConnected = new List<string>();
        private bool _isConnected;

        public WsComm()
        {
            _webSocket = new WebSocket(new Uri("wss://gameserverdevenv01.devel.akamon.com/:8080"));
            _connectedStream = Observable.FromCoroutine(_webSocket.Connect);
            _connectedStream.Subscribe(s =>
            {
                Observable.FromCoroutine(Start).Subscribe(c=>_webSocket.Close());
                _isConnected = true;
                _todoWhenConnected.ForEach(Send);
            });
        }

        public IEnumerator Start()
        {
            while (true)
            {
                var reply = _webSocket.RecvString();

                if (!string.IsNullOrEmpty(reply))
                {
                    _replySubject.OnNext(reply);
                }
                if (_webSocket.error != null)
                {
                    throw new Exception(_webSocket.error);
                }

                yield return null;
            }
        }

        public IObservable<string> Receive()
        {
            return _replySubject.AsObservable();
        }

        public void Send(string data)
        {
            if (_isConnected)
            {
                _webSocket.SendString(data);
            }
            else
            {
                _todoWhenConnected.Add(data);
            }
        }
    }
}