using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PhysicsConstraintsAuthoring : MonoBehaviour
{
    public bool freezePositionX;
    public bool freezePositionY;
    public bool freezePositionZ;
    public bool freezeRotationX;
    public bool freezeRotationY;
    public bool freezeRotationZ;
    
    private class PhysicsConstraintsBaker : Baker<PhysicsConstraintsAuthoring>
    {
        public override void Bake(PhysicsConstraintsAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new PhysicsConstraints()
            {
                PositionConstraint = new bool3(authoring.freezePositionX, authoring.freezePositionY, authoring.freezePositionZ),
                RotationConstraint = new bool3(authoring.freezeRotationX, authoring.freezeRotationY, authoring.freezeRotationZ)
            });
        }
    }
}