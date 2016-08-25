using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Svitwo
{
    public class TestSystem : ISystem
    {
        public int Priority { get; set; }

        public bool Init()
        {
            throw new System.NotImplementedException();
        }

        public void Update(float dt)
        {
            throw new System.NotImplementedException();
        }

        public void LateUpdate(float dt)
        {
            throw new System.NotImplementedException();
        }

        public void FixedUpdate(float dt)
        {
            throw new System.NotImplementedException();
        }

        public void Shutdown()
        {
            throw new System.NotImplementedException();
        }

        public void AddEntities(List<Entity> entities)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEntities(List<Entity> entities)
        {
            throw new System.NotImplementedException();
        }
    }


}
