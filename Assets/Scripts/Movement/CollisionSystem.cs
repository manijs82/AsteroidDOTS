using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

//[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
//[UpdateAfter(typeof(PhysicsSystemGroup))]
public partial struct CollisionSystem : ISystem
{
    private EntityCommandBuffer commandBuffer;
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<SimulationSingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Dependency = new TriggerJob
        {
            PlayerGroup = SystemAPI.GetComponentLookup<PlayerTag>(),
            EnemyGroup = SystemAPI.GetComponentLookup<EnemyTag>(),
            ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged)
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }
    
    [BurstCompile]
    struct TriggerJob : ITriggerEventsJob
    {
        public ComponentLookup<PlayerTag> PlayerGroup;
        public ComponentLookup<EnemyTag> EnemyGroup;
        public EntityCommandBuffer ecb;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entityA = triggerEvent.EntityA;
            Entity entityB = triggerEvent.EntityB;

            bool entityAIsPlayer = PlayerGroup.HasComponent(entityA);
            bool entityBIsPlayer = PlayerGroup.HasComponent(entityB);
            bool entityAIsEnemy = EnemyGroup.HasComponent(entityA);
            bool entityBIsEnemy = EnemyGroup.HasComponent(entityB);

            if (entityAIsPlayer && entityBIsEnemy)
            {
                // destroy entity b
                //ecb.DestroyEntity(entityA);
                //Debug.Log("VAR");
            }

            if (entityAIsEnemy && entityBIsPlayer)
            {
                // destroy entity a
                //ecb.DestroyEntity(entityB);
                //Debug.Log("VAR");
            }
        }
    }
}