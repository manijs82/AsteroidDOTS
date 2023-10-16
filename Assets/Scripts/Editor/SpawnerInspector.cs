using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(SpawnerAuthoring))]
    public class SpawnerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var spawner = (SpawnerAuthoring) target;
            spawner.spawnBound.x = spawner.transform.position.x;
            spawner.spawnBound.y = spawner.transform.position.y;
            Repaint();
        }
    }
}