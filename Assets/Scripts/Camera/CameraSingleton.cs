using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSingleton : MonoBehaviour
{
    public static Camera Instance;
    
    private void Awake()
    {
        Instance = GetComponent<Camera>();
    }
}