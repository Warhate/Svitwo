using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Svitwo
{
    public class Entity
    {
        public uint Id { get; set; }

        public const uint INVALID_ENTITY = uint.MaxValue;

        public bool IsInvalid { get { return Id == INVALID_ENTITY; } }

    }

    public class EntityManager
    {
        private const int MAX_FREE_ID = 64;
        private const int START_ENTITY_COUNT = 256;

        readonly Queue<uint>     _freeIds            = new Queue<uint>();
        readonly List<Entity>    _createdEntities    = new List<Entity>();
        readonly List<Entity>    _destroyedEntities  = new List<Entity>();

        BitArray[] _componentMasks = new BitArray[START_ENTITY_COUNT];
        IComponentPool[] _storages = new IComponentPool[START_ENTITY_COUNT];

        public List<Entity> CreatedEntities
        {
            get { return _createdEntities; }
        }

        public List<Entity> DestroyedEntities
        {
            get { return _destroyedEntities; }
        }

        uint _lastEntityId = 0;

        public Entity CreatEntity()
        {
            var entity = new Entity();

            if (_freeIds.Count > MAX_FREE_ID)
            {
                entity.Id = _freeIds.Dequeue();
            }
            else
            {
                entity.Id = ++_lastEntityId;

                if (entity.Id >= _componentMasks.Length)
                {
                    Array.Resize(ref _componentMasks, _componentMasks.Length + 48);
                }
            }

            _createdEntities.Add(entity);

            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            Assert.IsTrue(entity.Id < _componentMasks.Length);
            Assert.IsTrue(entity.IsInvalid);

            _freeIds.Enqueue(entity.Id);
            _componentMasks[entity.Id].SetAll(false);
            _destroyedEntities.Add(entity);
        }

        public void ClearTemporaryEntities()
        {
            _createdEntities.Clear();
            _destroyedEntities.Clear();
        }

        public BitArray GetComponentMask(Entity entity)
        {
            Assert.IsTrue(entity.Id < _componentMasks.Length);
            Assert.IsTrue(!entity.IsInvalid);

            return _componentMasks[entity.Id];
        }

        public ComponentPool<T> ProvideComponentPool<T>()
        {
            uint componentId = Component<T>.TypeId;

            if (componentId >= _storages.Length)
            {
                Array.Resize(ref _storages, _storages.Length + 10);
            }

            if (_storages[componentId] == null)
            {
                _storages[componentId] = new ComponentPool<T>();
            }

            return (ComponentPool<T>)_storages[componentId];
        }

        public T AddComponent<T>(Entity entity) where T : new ()
        {
            var component = new T();
            AddComponent(entity, component);
            return component;
        }

        public void AddComponent<T>(Entity entity, T component)
        {
            uint componentId = Component<T>.TypeId;
            var storage = ProvideComponentPool<T>();

            if (storage.Size <= entity.Id)
            {
                storage.Resize((int)entity.Id + 100);
            }

            storage[entity] = component;

            if (_componentMasks[entity.Id] == null)
            {
                _componentMasks[entity.Id] = new BitArray(BaseComponent.MAX_COMPONENT_COUNT);
            }
            _componentMasks[entity.Id].Set((int)componentId, true);
        }

        public bool HasComponent<T>(Entity entity)
        {
            return _componentMasks[entity.Id].Get((int)Component<T>.TypeId);
        }

        public T GetComponent<T>(Entity entity)
        {
            var pool = ProvideComponentPool<T>();
            return pool[entity];
        }

        public void RemoveComponent<T>(Entity entity)
        {
            var pool = ProvideComponentPool<T>();
            pool[entity] = default(T);
        }
    }
}
