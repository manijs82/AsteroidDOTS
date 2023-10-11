using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    private class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<PlayerTag>(entity);
            AddComponent<PlayerInput>(entity);
        }
    }
}