using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
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
        Entities.ForEach((ref LocalTransform transform, ref PhysicsMass mass, in PhysicsConstraints constraints) =>
        {
            mass.InverseInertia.x = 0;
            mass.InverseInertia.y = 0;
            mass.InverseInertia.z = 0;

            transform = LocalTransform.FromPosition(transform.Position);
        }).ScheduleParallel();
    }
}