using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public struct RandomData : IComponentData
{
    public Random Value;
}

public partial class SpawnRandomPositionSystem : SystemBase
{
    protected override void OnStartRunning()
    {
        RequireForUpdate<RandomData>();
        
        Entities.ForEach((Entity e, int entityInQueryIndex, ref RandomData randomData) =>
        {
            randomData.Value = Random.CreateFromIndex((uint)entityInQueryIndex);
        }).ScheduleParallel();
    }

    protected override void OnUpdate()
    {
        var hasPlayer = SystemAPI.TryGetSingletonEntity<PlayerTag>(out Entity player);
        var playerPosition = EntityManager.GetComponentData<LocalTransform>(player).Position;
        if (!hasPlayer) 
            return;
        
        Entities
            .ForEach((ref Spawner spawner, ref RandomData randomData) =>
            {
                if (spawner.NextSpawnTime < SystemAPI.Time.ElapsedTime)
                {
                    float xPos = randomData.Value.NextFloat(spawner.SpawnBound.x, spawner.SpawnBound.XPW);
                    float yPos = randomData.Value.NextFloat(spawner.SpawnBound.y, spawner.SpawnBound.YPH);
                    
                    if(math.distance(playerPosition, new float3(xPos, yPos, 0)) >= 20)
                        spawner.SpawnPosition = new float3(xPos, yPos, 0);
                    else
                        spawner.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.SpawnRate;
                }
                
            }).ScheduleParallel();
    }
}