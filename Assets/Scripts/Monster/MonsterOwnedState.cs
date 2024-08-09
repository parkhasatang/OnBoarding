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
        public override void Enter(MonsterStateController entity)
        {
            entity.ChangeAnimationState(MonsterState.Move);
        }

        public override void Execute(MonsterStateController entity)
        {
            // �������� ���������� �̵�
            entity.transform.Translate(Vector3.left * entity.MonsterInfo.Speed * Time.deltaTime);
        }

        public override void Exit(MonsterStateController entity)
        {
        }
    }

    public class AttackedState : State<MonsterStateController>
    {
        public override void Enter(MonsterStateController entity)
        {
            entity.ChangeAnimationState(MonsterState.Attacked);
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
            entity.ChangeAnimationState(MonsterState.Dead);
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
