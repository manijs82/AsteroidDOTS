using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyCountText;

    private void Update()
    {
        enemyCountText.text = SpawnSystem.ActiveSpawns.ToString();
    }
}