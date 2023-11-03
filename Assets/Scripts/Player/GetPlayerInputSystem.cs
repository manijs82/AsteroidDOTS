using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

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
        TouchSimulation.Enable();
    }

    protected override void OnUpdate()
    {
        var isHoldingTouch = inputActions.Actions.Touch.ReadValue<float>();
        if (isHoldingTouch > 0.01f)
        {
            var mouseStart = inputActions.Actions.TouchStart.ReadValue<Vector2>();
            var mousePos = inputActions.Actions.TouchPos.ReadValue<Vector2>();
            var mouseDelta = (mousePos - mouseStart).normalized;
            
            SystemAPI.SetSingleton(new PlayerInput
            {
                MoveInput = mouseDelta
            });
        }
        else
        {
            SystemAPI.SetSingleton(new PlayerInput
            {
                MoveInput = 0
            });
        }
    }

    protected override void OnStopRunning()
    {
        inputActions.Disable();
    }
}