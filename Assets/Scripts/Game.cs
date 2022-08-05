using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Client
{
    [RequireComponent(typeof(SceneData))]
    sealed class Game : MonoBehaviour
    {
        EcsSystems systems;

        void Start()
        {
            var world = new EcsWorld();
            systems = new EcsSystems(world);

            systems.Add(new InitSystem());

            systems.Add(new PeriodicTimerSystem());

            systems.Add(new VelocityByKeyboardSystem());
            systems.Add(new RotateToDirectionSystem());
            systems.Add(new TargetingAtPlayerSystem());

            systems.Add(new DirectionByMouseSystem());
            systems.Add(new RotationLikeParentSystem());
            systems.Add(new PositionLikeParentSystem());
            systems.Add(new DirectionAtTargetSystem());
            systems.Add(new VelocityByDirectionSystem());
            systems.Add(new AnimationSpeedByVelocitySystem());

            systems.Add(new CreateWeaponSystem());
            systems.Add(new SetWeaponSystem());

            systems.Add(new FireAutoClearSystem());
            systems.Add(new FireByMouseSystem());
            systems.Add(new FireIfTargetSystem());
            systems.Add(new ParticlesPlayIfFireSystem());
            systems.Add(new GunControlSystem());

            systems.Add(new DamageParticleCollidedSystem());
            systems.Add(new ImpactDamageSystem());

            systems.Add(new DeadSystem());
            systems.Add(new DeadByHealthSystem());
            systems.Add(new DeadLikeParentSystem());
            systems.Add(new BulletControlSystem());
            systems.Add(new BugControlSystem());
            systems.Add(new DistributorComponentSystem());
            systems.Add(new InventoryComponentSystem());
            

            systems.Add(new SpawnerObjectControlSystem());
            systems.Add(new PlayerSpawnerControlSystem());
            systems.Add(new SpawnIfTargetSystem());
            systems.Add(new SpawnIfDeadSystem());
            systems.Add(new SpawnSystem());


            #if UNITY_EDITOR
                systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            #endif
            systems.Inject(); // засовывет компоненты в системы
            systems.Inject(GetComponent<SceneData>());

            systems.Init();
        }

        void Update()
        {
            systems?.Run();
        }

        void OnDestroy()
        {
            systems?.GetWorld()?.Destroy();
            systems?.Destroy();
            systems = null;
        }
    }
}
