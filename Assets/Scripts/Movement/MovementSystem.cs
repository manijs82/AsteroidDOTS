using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class MovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
    
        Entities
            .ForEach((ref LocalTransform transform, in Movement movement) =>
            {
                transform = transform.Translate(math.up() * (movement.Speed * deltaTime));
            })
            .ScheduleParallel();
    }
}