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
        public float idleDistance = 1.5f; // 플레이어와의 거리가 이 값 이하로 가까워지면 Idle 상태로 전환

        public override void Enter(MonsterStateController entity)
        {
            entity.ChangeAnimationState(MonsterState.Walk);
        }

        public override void Execute(MonsterStateController entity)
        {
            // 플레이어와의 거리 계산
            float distanceToPlayer = Vector3.Distance(entity.transform.position, MonsterSpawnManager.Instance.playerPosition.position);

            if (distanceToPlayer <= idleDistance)
            {
                entity.ChangeState(entity.idleState); // 거리가 가까워지면 Idle 상태로 전환
            }
            else
            {
                // 왼쪽으로 지속적으로 이동
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
            entity.ChangeAnimationState(MonsterState.Death);
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
