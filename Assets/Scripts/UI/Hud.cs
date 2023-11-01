using TMPro;
using Unity.Entities;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyCountText;

    EntityQuery enemyQuery;
    
    private void Start()
    {
        enemyQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(
            ComponentType.ReadOnly<EnemyTag>());
    }

    private void Update()
    {
        int enemyCount = enemyQuery.CalculateEntityCount();
        enemyCountText.text = enemyCount.ToString();
    }
}