using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Client
{
    sealed class InitSystem : IEcsInitSystem
    {
        readonly EcsWorldInject ecsWorld = default;

        // Ищет всех EcsMonoBehaviour на сцене, и инициализирует (вызывает InitEntity) //
        public void Init(IEcsSystems systems)
        {
            foreach (var roots in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
            {
                var initializers = roots.GetComponentsInChildren<EcsMonoBehaviour>();
                foreach (var initializer in initializers)
                    initializer.InitEntity(ecsWorld.Value);
            }
        }
    }
}
