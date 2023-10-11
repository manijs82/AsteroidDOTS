using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct CameraFollowSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var cameraTransform = CameraSingleton.Instance.transform;
        var target = SystemAPI.GetSingletonEntity<PlayerTag>();
        var targetPos = SystemAPI.GetComponent<LocalToWorld>(target).Position;
        cameraTransform.position = new Vector3(targetPos.x, targetPos.y, cameraTransform.position.z);
    }
}