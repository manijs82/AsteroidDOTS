using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct Laser : IComponentData
{
    public float Length;
    public float3 Position;
    public float3 Direction;
}

[UpdateAfter(typeof(MovementSystem))]
public partial class LaserSystem : SystemBase
{
    
    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<Laser>();
    }
    
    protected override void OnUpdate()
    {
        var player = SystemAPI.GetSingletonEntity<PlayerTag>();
        var playerPosition = EntityManager.GetComponentData<LocalToWorld>(player).Position;
        var playerDirection = EntityManager.GetComponentData<Movement>(player).InputDirection;
        if (math.length(playerDirection) < 0.01f)
            return;
        
        Entities.ForEach((ref LocalTransform transform, ref Laser laser) =>
        {
            laser.Direction = new float3(playerDirection, 0);
            laser.Position = playerPosition;

            transform.Position = laser.Position;
            float angle = math.atan2(laser.Direction.y, laser.Direction.x) - math.PI / 2f;
            transform.Rotation = quaternion.AxisAngle(math.forward(), angle);
            transform.Scale = laser.Length;
        }).Schedule();
    }
}

