using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Infrastructure.CoreTools.Extensions;
using Assets.Scripts.Space2Module.Redux.State;

namespace Assets.Scripts.Space2Module.Controllers.ObjectsPopulation
{
    public class ObjectsPopulator
    {
        private struct PopulatableAndDataPair
        {
            public PopulatableObject Populatable;
            public ObjectData Data;
        }

        public struct PopulationFromDataResult
        {
            public PopulatableObject[] PopulatablesWithNoData;
            public ObjectData[] DatasWithNoPopulatables;
        }

        private readonly Dictionary<string, PopulatableAndDataPair> _objectsData = new Dictionary<string, PopulatableAndDataPair>();
 
        public void Register(PopulatableObject obj)
        {
            if (_objectsData.Count(s=>s.Value.Populatable == obj || s.Value.Populatable.Id == obj.Id) != 0)
                return;

            var id = Guid.NewGuid().ToString();
            obj.Id = id;
            _objectsData.Add(obj.Id, 
                new PopulatableAndDataPair
                {
                    Data = ObjectsPopulatorHelper.PopulatableObjectToData(obj),
                    Populatable = obj
                });
        }

        public PopulationFromDataResult PopulateObjectsFromData(ObjectData[] objectsData)
        {
            _objectsData.Values.Select(s => s.Populatable).ForEach(p =>
            {
                var d = objectsData.FirstOrDefault(od => od.Id == p.Id);

                if (d != null)
                {
                   ObjectsPopulatorHelper.FromData(p.transform, d);
                }
            });

            var orphanObjects = _objectsData
                .Where(lod => objectsData.Count(od => lod.Value.Populatable.Id == od.Id) == 0)
                .Select(o=>o.Value.Populatable)
                .ToArray();

            var orphanData = objectsData
                .Where(od => _objectsData.Count(lod => lod.Value.Populatable.Id == od.Id) == 0)
                .ToArray();

            orphanObjects.ForEach(o=>_objectsData.Remove(o.Id));
            orphanData.ForEach(o=>_objectsData.Remove(o.Id));

            var extras = new PopulationFromDataResult
            {
                DatasWithNoPopulatables = orphanData,
                PopulatablesWithNoData = orphanObjects
            };

            return extras;
        }

        public ObjectData[] GetObjectsData()
        {
            UpdateObjectsData();
            return _objectsData.Values.Select(s => s.Data).ToArray();
        }

        private void UpdateObjectsData()
        {
            _objectsData.Values.ForEach(v=>v.Data= ObjectsPopulatorHelper.PopulatableObjectToData(v.Populatable));
        }
    }
}