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
            // Idle 상태에서는 아무것도 하지 않음.
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
            // 왼쪽으로 지속적으로 이동
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
            // 넉백 처리
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
            // 죽음 애니메이션이 끝난 후 오브젝트를 제거하거나 추가 로직을 처리합니다.
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
