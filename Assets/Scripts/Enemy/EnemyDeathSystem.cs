using Unity.Entities;

[UpdateAfter(typeof(LaserCollisionSystem))]
public partial class EnemyDeathSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(World.Unmanaged);

        Entities.ForEach((Entity e, in EnemyTag enemy) =>
        {
            if (enemy.IsDead)
            {
                ecb.DestroyEntity(e);
            }
        }).Schedule();
    }
}