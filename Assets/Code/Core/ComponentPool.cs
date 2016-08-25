using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Svitwo
{
    public interface  IComponentPool
    {
        void Clear();
    }

    public class ComponentPool<T> : IComponentPool
    {
        T[] _components;

        public int Size
        {
            get { return _components.Length; }
        }

        public ComponentPool(uint size = 100)
        {
            _components = new T[size];
        }

        public void AddComponent(Entity entity, T component)
        {
            _components[entity.Id] = component;
        }

        public T GetComponent(Entity entity)
        {
            return _components[entity.Id];
        }

        public void Resize(int size)
        {
            Array.Resize(ref _components, size);
        }

        public void Clear()
        {
            Array.Clear(_components, 0 , _components.Length);
        }

        public T this [Entity entity]
        {
            get { return _components[entity.Id]; }
            set { _components[entity.Id] = value; }
        }
    }
}
