using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    public string MonsterName { get; private set; }
    public string Grade { get; private set; }
    public float Speed { get; private set; }
    public int Health { get; private set; }

    public void Initialize(MonsterData monsterData)
    {
        MonsterName = monsterData.Name;
        Grade = monsterData.Grade;
        Speed = monsterData.Speed;
        Health = monsterData.Health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
        }
    }
}
