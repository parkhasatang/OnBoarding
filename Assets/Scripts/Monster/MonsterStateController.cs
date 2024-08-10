using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterOwnedState;

public enum MonsterState // �ش� State�� �´� State�� ���� �ʱ����� �ʿ䰡 ����.
{
    Idle,
    Walk,
    Hurt,
    Death
}


public class MonsterStateController : MonoBehaviour
{
    internal MonsterInfo monsterInfo;
    private HealthBar healthBar;
    private Animator animator;
    public MonsterState currentAnimationState;

    public State<MonsterStateController> CurrentState { get; private set; }
    internal IdleState idleState;
    private MoveState moveState;
    private AttackedState attackedState;
    private DeadState deadState;

    private Vector3 knockbackDirection;
    private float knockbackDuration = 0.5f; // �˹� ���� �ð�
    private float knockbackTimer;

    private void Awake()
    {
        idleState = new IdleState();
        moveState = new MoveState();
        attackedState = new AttackedState();
        deadState = new DeadState();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (monsterInfo == null)
        {
            monsterInfo = GetComponent<MonsterInfo>();
        }

        if (healthBar == null)
        {
            healthBar = GetComponent<HealthBar>();
        }
    }

    private void Start()
    {
        ChangeState(moveState);
    }

    private void Update()
    {
        CurrentState?.Execute(this);
    }

    public void ChangeState(State<MonsterStateController> newState)
    {
        CurrentState?.Exit(this);
        CurrentState = newState;
        CurrentState?.Enter(this);
    }

    public void ChangeAnimationState(MonsterState newState)
    {
        if (currentAnimationState == newState) return;

        animator.Play(newState.ToString());
        currentAnimationState = newState;
    }

    public void TakeDamage(int damage, Vector3 knockbackDir)
    {
        monsterInfo.TakeDamage(damage);
        healthBar.SetHealth(monsterInfo.CurrentHealth);

        if (monsterInfo.CurrentHealth <= 0)
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

    public void ResetMonster()
    {
        healthBar.SetMaxHealth(monsterInfo.Health); // ü�¹� �ʱ�ȭ
        ChangeState(idleState); // �ʱ� ���·� ����
    }
}
