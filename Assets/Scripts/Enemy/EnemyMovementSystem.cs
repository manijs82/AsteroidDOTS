using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(GetPlayerInputSystem))]
public partial class EnemyMovementSystem : SystemBase
{
    protected override void OnStartRunning()
    {
        RequireForUpdate<EnemyTag>();
        RequireForUpdate<PlayerTag>();
    }

    protected override void OnUpdate()
    {
        var hasPlayer = SystemAPI.TryGetSingletonEntity<PlayerTag>(out Entity player);
        var playerPosition = EntityManager.GetComponentData<LocalToWorld>(player).Position;
        if (!hasPlayer) 
            return;

        Entities
            .WithAll<EnemyTag>()
            .ForEach((ref Movement movement, in LocalTransform transform) =>
            {
                float3 dirToPlayer = playerPosition - transform.Position;
                dirToPlayer = math.normalize(dirToPlayer);

                movement.InputDirection = dirToPlayer.xy;
            })
            .ScheduleParallel();
    }
}