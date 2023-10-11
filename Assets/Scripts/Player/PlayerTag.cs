using Unity.Entities;
using Unity.Mathematics;

public struct PlayerTag : IComponentData { }

public struct PlayerInput : IComponentData
{
    public float2 MoveInput;
}