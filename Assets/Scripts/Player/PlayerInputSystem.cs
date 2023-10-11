using Unity.Entities;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
[UpdateAfter(typeof(GetPlayerInputSystem))]
public partial class PlayerInputSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Movement movement, in PlayerInput input) =>
        {
            movement.InputDirection = input.MoveInput;
        }).Schedule();
        CompleteDependency();
    }
}