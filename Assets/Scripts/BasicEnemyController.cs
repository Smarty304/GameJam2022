using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Points;
    private int NextPointIndex = 0;

    [SerializeField]
    private float Speed;
    private float CurrentSpeed;

    private float BottleDuration;

    private enum State
    {
        WALKING,
        KNOCKBACK,
        DEAD
    }

    private State CurrentState = State.WALKING;

    private void Start()
    {
        CurrentSpeed = Speed;
    }

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

        UpdateBottleEffect();
    }

    private void UpdateBottleEffect()
    {
        BottleDuration = Mathf.Min(BottleDuration - Time.deltaTime, 0f);

        if (BottleDuration == 0f)
        {
            CurrentSpeed = Speed;
        }
    }

    public void Kill()
    {
        SwitchState(State.DEAD);
    }

    public void GetHitBy(Bottle.BottleType bottle)
    {
        switch(bottle)
        {
            case Bottle.BottleType.blue:
                CurrentSpeed = 0.0f;
                break;
            case Bottle.BottleType.red:
                CurrentSpeed = 2.0f * Speed;
                break;
            case Bottle.BottleType.yellow:
                CurrentSpeed = 0.5f * Speed;
                break;
        }

        BottleDuration = 5f;
    }

    private void EnterWalkingState()
    {
        CurrentState = State.WALKING;
    }

    private void UpdateWalkingState()
    {
        Vector2 pathToPoint = Points[NextPointIndex].transform.position - transform.position;
        float distance = pathToPoint.magnitude;
        Vector2 direction = pathToPoint.normalized;

        float stepSize = Mathf.Min(distance, Time.deltaTime * CurrentSpeed);
        Vector2 step =  stepSize * direction;

        transform.position += new Vector3(step.x, step.y, 0);

        if (stepSize == distance)
        {
            NextPointIndex = NextPointIndex + 1 < Points.Count ? NextPointIndex + 1 : 0;
        }
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
