using System.Collections;
using UnityEngine;

public class MonsterSpawnManager : Singleton<MonsterSpawnManager>
{
    private MonsterDataParser monsterDataParser;

    protected override void Awake()
    {
        base.Awake();
        monsterDataParser = GetComponent<MonsterDataParser>();
    }

    private void Start()
    {
        if (monsterDataParser == null)
        {
            Debug.LogError("MonsterDataParser ������Ʈ�� ã�� ���Ͽ����ϴ�.");
            return;
        }

        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        for (int i = 0; i < monsterDataParser.GetMonsterCount(); i++)
        {
            MonsterData monsterData = monsterDataParser.GetMonsterData(i);
            if (monsterData != null)
            {
                SpawnMonster(monsterData);
            }
            yield return new WaitForSeconds(2); // 2�� �������� ���� ����
        }
    }

    void SpawnMonster(MonsterData monsterData)
    {
        // ���⿡ ���͸� �����ϴ� ������ �߰��մϴ�.
        Debug.Log($"Spawning {monsterData.Name} with {monsterData.Grade} grade, {monsterData.Speed} speed, and {monsterData.Health} health");
        // ����: Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}
