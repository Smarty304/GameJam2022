using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Points;

    [SerializeField]
    private Transform GroundCheck, WallCheck;

    [SerializeField]
    private Transform GroundCheckDistance, WallCheckDistance;

    private bool GroundDectected, WallDetected;

    private enum State
    {
        WALKING,
        KNOCKBACK,
        DEAD
    }

    private State CurrentState;

    private void Update()
    {
        switch (CurrentState)
        {
            case State.WALKING:
                UpdateWalkingState();
                break;
            case State.KNOCKBACK:
                UpdateKnockbackState();
                break;
            case State.DEAD:
                UpdateDeadState();
                break;
        }
    }

    private void EnterWalkingState()
    {
        CurrentState = State.WALKING;
    }

    private void UpdateWalkingState()
    {

    }
    private void ExitWalkingState()
    {

    }

    private void EnterKnockbackState()
    {

    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }

    private void EnterDeadState()
    {

    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    private void SwitchState(State state)
    {
        switch (CurrentState)
        {
            case State.WALKING:
                ExitWalkingState();
                break;
            case State.KNOCKBACK:
                ExitKnockbackState();
                break;
            case State.DEAD:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.WALKING:
                EnterWalkingState();
                break;
            case State.KNOCKBACK:
                EnterKnockbackState();
                break;
            case State.DEAD:
                EnterDeadState();
                break;
        }

        CurrentState = state;
    }
}
