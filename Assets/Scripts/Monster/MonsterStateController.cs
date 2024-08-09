using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterOwnedState;

public enum MonsterState // 해당 State에 맞는 State를 전부 초기해줄 필요가 있음.
{
    Idle,
    Move,
    Attacked,
    Dead
}


public class MonsterStateController : MonoBehaviour
{
    public MonsterInfo MonsterInfo { get; private set; }
    private Animator animator;
    public MonsterState currentAnimationState;

    public State<MonsterStateController> currentState { get; private set; }
    private IdleState idleState;
    private MoveState moveState;
    private AttackedState attackedState;
    private DeadState deadState;

    private Vector3 knockbackDirection;
    private float knockbackDuration = 0.5f; // 넉백 지속 시간
    private float knockbackTimer;

    private void Awake()
    {
        idleState = new IdleState();
        moveState = new MoveState();
        attackedState = new AttackedState();
        deadState = new DeadState();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        ChangeState(moveState);
    }

    private void Update()
    {
        currentState?.Execute(this);
    }

    public void ChangeState(State<MonsterStateController> newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    public void ChangeAnimationState(MonsterState newState)
    {
        if (currentAnimationState == newState) return;

        animator.Play(newState.ToString());
        currentAnimationState = newState;
    }

    public void TakeDamage(int damage, Vector3 knockbackDir)
    {
        MonsterInfo.TakeDamage(damage);

        if (MonsterInfo.Health <= 0)
        {
            ChangeState(deadState);
        }
        else
        {
            knockbackDirection = knockbackDir;
            knockbackTimer = knockbackDuration;
            ChangeState(attackedState);
        }
    }

    public void ApplyKnockback()
    {
        if (knockbackTimer > 0)
        {
            transform.Translate(knockbackDirection * Time.deltaTime);
            knockbackTimer -= Time.deltaTime;
        }
        else
        {
            ChangeState(moveState);
        }
    }
}
