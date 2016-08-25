using UnityEngine;
using System.Collections.Generic;

namespace Svitwo
{
    public class SystemManager
    {
        readonly SortedList<int, ISystem> _systems = new SortedList<int, ISystem>();

        public bool Init()
        {
            return true;
        }

        public void AttachSystem(ISystem system)
        {
            _systems.Add(system.Priority, system);
            system.Init();
        }

        public void DetachSystem(ISystem system)
        {
            int index = _systems.IndexOfValue(system);

            if (index >= 0)
            {
                _systems.RemoveAt(index);
                system.Shutdown();
            }
        }

        public void AddEntities(List<Entity> entities)
        {
            foreach (KeyValuePair<int, ISystem> pair in _systems)
            {
                pair.Value.AddEntities(entities);
            }
        }

        public void RemoveEntities(List<Entity> entities)
        {
            foreach (KeyValuePair<int, ISystem> pair in _systems)
            {
                pair.Value.RemoveEntities(entities);
            }
        }

        public void Update()
        {
            foreach (KeyValuePair<int, ISystem> pair in _systems)
            {
                pair.Value.Update(Time.deltaTime);
            }
        }

        public void LateUpdate()
        {
            foreach (KeyValuePair<int, ISystem> pair in _systems)
            {
                pair.Value.LateUpdate(Time.deltaTime);
            }
        }

        public void FixedUpdate()
        {
            foreach (KeyValuePair<int, ISystem> pair in _systems)
            {
                pair.Value.FixedUpdate(Time.fixedDeltaTime);
            }
        }
    }
}
