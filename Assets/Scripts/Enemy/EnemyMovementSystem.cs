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
        var player = SystemAPI.GetSingletonEntity<PlayerTag>();
        var playerPosition = EntityManager.GetComponentData<LocalToWorld>(player).Position;

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