using Unity.Entities;
using UnityEngine;

public class LaserAuthoring : MonoBehaviour
{
    public float length = 20f;
    
    private class LaserBaker : Baker<LaserAuthoring>
    {
        public override void Bake(LaserAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new Laser()
            {
                Length = authoring.length
            });
        }
    }
}