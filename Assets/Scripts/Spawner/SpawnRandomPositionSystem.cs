using Unity.Entities;
using Unity.Mathematics;

public struct RandomData : IComponentData
{
    public Random Value;
}

public partial class SpawnRandomPositionSystem : SystemBase
{
    protected override void OnStartRunning()
    {
        Entities.ForEach((Entity e, int entityInQueryIndex, ref RandomData randomData) =>
        {
            randomData.Value = Random.CreateFromIndex((uint)entityInQueryIndex);
        }).ScheduleParallel();
    }

    protected override void OnUpdate()
    {
        Entities
            .ForEach((ref Spawner spawner, ref RandomData randomData) =>
            {
                if (spawner.NextSpawnTime < SystemAPI.Time.ElapsedTime)
                {
                    float xPos = randomData.Value.NextFloat(spawner.SpawnBound.x, spawner.SpawnBound.XPW);
                    float yPos = randomData.Value.NextFloat(spawner.SpawnBound.y, spawner.SpawnBound.YPH);

                    spawner.SpawnPosition = new float3(xPos, yPos, 0);
                }
                
            }).ScheduleParallel();
    }
}