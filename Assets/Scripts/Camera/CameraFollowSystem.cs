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
        float deltaTime = SystemAPI.Time.DeltaTime;
        
        var cameraTransform = CameraSingleton.Instance.transform;
        var target = SystemAPI.GetSingletonEntity<PlayerTag>();
        var targetSpeed = state.EntityManager.GetComponentData<Movement>(target).Speed;
        
        var targetPos = SystemAPI.GetComponent<LocalToWorld>(target).Position;
        targetPos.z = cameraTransform.transform.position.z;
        
        cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, targetPos, targetSpeed * deltaTime);
    }
}