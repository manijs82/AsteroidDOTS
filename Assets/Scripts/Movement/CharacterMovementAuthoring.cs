using Unity.Entities;
using UnityEngine;

public class CharacterMovementAuthoring : MonoBehaviour
{
    public float initialSpeed;
    
    private class CharacterMovementBaker : Baker<CharacterMovementAuthoring>
    {
        public override void Bake(CharacterMovementAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Movement
            {
                Speed = authoring.initialSpeed
            });
            
        }
    }
}