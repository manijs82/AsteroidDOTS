using Unity.Entities;
using Unity.Mathematics;

public struct Movement : IComponentData
{
    public float Speed;
    public float2 InputDirection;
}