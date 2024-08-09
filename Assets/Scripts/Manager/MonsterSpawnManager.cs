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
            Debug.LogError("MonsterDataParser 컴포넌트를 찾지 못하였습니다.");
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
            yield return new WaitForSeconds(2); // 2초 간격으로 몬스터 생성
        }
    }

    void SpawnMonster(MonsterData monsterData)
    {
        // 여기에 몬스터를 생성하는 로직을 추가합니다.
        Debug.Log($"Spawning {monsterData.Name} with {monsterData.Grade} grade, {monsterData.Speed} speed, and {monsterData.Health} health");
        // 예시: Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}
