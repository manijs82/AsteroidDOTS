using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
public partial class GetPlayerInputSystem : SystemBase
{
    private AstroidsInputActions inputActions;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<Movement>();
        
        inputActions = new AstroidsInputActions();
    }

    protected override void OnStartRunning()
    {
        inputActions.Enable();
    }

    protected override void OnUpdate()
    {
        var currentMoveInput = inputActions.Actions.Move.ReadValue<Vector2>();
        
        SystemAPI.SetSingleton(new PlayerInput
        {
            MoveInput = currentMoveInput
        });
    }

    protected override void OnStopRunning()
    {
        inputActions.Disable();
    }
}