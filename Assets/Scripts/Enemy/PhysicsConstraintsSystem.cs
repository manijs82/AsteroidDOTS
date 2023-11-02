using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public struct PhysicsConstraints : IComponentData
{
    public bool3 PositionConstraint;
    public bool3 RotationConstraint;
}

[UpdateAfter(typeof(MovementSystem))]
public partial class PhysicsConstraintsSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LocalTransform transform, in PhysicsConstraints constraints) =>
        {
            transform.Rotation = quaternion.identity;
        }).ScheduleParallel();
    }
}