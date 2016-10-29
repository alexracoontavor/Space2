using System;
using Assets.Infrastructure.CoreTools.Instanced.FileSystemAccessor;
using Assets.Scripts.Space2Module.Redux.State;
using UniRx;

namespace Assets.Scripts.Space2Module.Controllers.SaveLoad
{
    public class SaveLoadController
    {
        public static IDisposable InitSaveStream(IObservable<Space2State> observable)
        {
            return observable
                .DistinctUntilChanged(s => new { s.DataSaveRequest })
                .Where(s => s.DataSaveRequest != null)
                .Subscribe(s =>
                {
                    SerializedDataImporter.SaveData(s, s.DataSaveRequest.FileName);
                });
        }
    }
}