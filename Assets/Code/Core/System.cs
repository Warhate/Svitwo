using System.Collections.Generic;

namespace Svitwo
{
    public interface ISystem
    {
        int Priority { get; set; }

        bool Init();
        void Update(float dt);
        void LateUpdate(float dt);
        void FixedUpdate(float dt);
        void Shutdown();

        void AddEntities(List<Entity> entities);
        void RemoveEntities(List<Entity> entities);
    }
}
