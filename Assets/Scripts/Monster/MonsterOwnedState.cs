using UnityEngine;

namespace MonsterOwnedState
{
    public class IdleState : State<MonsterStateController>
    {
        public override void Enter(MonsterStateController entity)
        {
            entity.ChangeAnimationState(MonsterState.Idle);
        }

        public override void Execute(MonsterStateController entity)
        {
            // Idle ���¿����� �ƹ��͵� ���� ����.
        }

        public override void Exit(MonsterStateController entity)
        {
        }
    }

    public class MoveState : State<MonsterStateController>
    {
        public float idleDistance = 1.5f; // �÷��̾���� �Ÿ��� �� �� ���Ϸ� ��������� Idle ���·� ��ȯ

        public override void Enter(MonsterStateController entity)
        {
            entity.ChangeAnimationState(MonsterState.Walk);
        }

        public override void Execute(MonsterStateController entity)
        {
            // �÷��̾���� �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(entity.transform.position, MonsterSpawnManager.Instance.playerPosition.position);

            if (distanceToPlayer <= idleDistance)
            {
                entity.ChangeState(entity.idleState); // �Ÿ��� ��������� Idle ���·� ��ȯ
            }
            else
            {
                // �������� ���������� �̵�
                entity.transform.Translate(entity.monsterInfo.Speed * Time.deltaTime * Vector3.left);
            }
        }

        public override void Exit(MonsterStateController entity)
        {
        }
    }

    public class AttackedState : State<MonsterStateController>
    {
        public override void Enter(MonsterStateController entity)
        {
            entity.ChangeAnimationState(MonsterState.Hurt);
        }

        public override void Execute(MonsterStateController entity)
        {
            // �˹� ó��
            entity.ApplyKnockback();
        }

        public override void Exit(MonsterStateController entity)
        {
        }
    }

    public class DeadState : State<MonsterStateController>
    {
        public override void Enter(MonsterStateController entity)
        {
            entity.ChangeAnimationState(MonsterState.Death);
            // ���� �ִϸ��̼��� ���� �� ������Ʈ�� �����ϰų� �߰� ������ ó���մϴ�.
            GameObject.Destroy(entity.gameObject);
        }

        public override void Execute(MonsterStateController entity)
        {
        }

        public override void Exit(MonsterStateController entity)
        {
        }
    }
}
