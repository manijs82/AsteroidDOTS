using Unity.Entities;
using UnityEditor;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public Bound spawnBound;
    public float spawnRate;

#if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        Handles.DrawAAPolyLine(new Vector3(spawnBound.x, spawnBound.y), new Vector3(spawnBound.XPW, spawnBound.y));
        Handles.DrawAAPolyLine(new Vector3(spawnBound.x, spawnBound.y), new Vector3(spawnBound.x, spawnBound.YPH));
        Handles.DrawAAPolyLine(new Vector3(spawnBound.XPW, spawnBound.y), new Vector3(spawnBound.XPW, spawnBound.YPH));
        Handles.DrawAAPolyLine(new Vector3(spawnBound.x, spawnBound.YPH), new Vector3(spawnBound.XPW, spawnBound.YPH));
        Handles.DrawSolidDisc(spawnBound.Center, Vector3.back, .2f);
    }
    
#endif
    
    private class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                SpawnBound = authoring.spawnBound,
                SpawnPosition = authoring.transform.position,
                NextSpawnTime = 0.0f,
                SpawnRate = authoring.spawnRate
            });
            
            AddComponent<RandomData>(entity);
        }
    }
}


