using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : Singleton<MonsterSpawnManager>
{
    private MonsterDataParser monsterDataParser;
    public List<MonsterData> monsterDatas = new List<MonsterData>();
    private List<ObjectPool<MonsterStateController>> monsterPools = new List<ObjectPool<MonsterStateController>>();
    private int currentMonsterIndex = 0;

    public Transform playerPosition;

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

        InitializePools();
        StartCoroutine(SpawnMonsters());
    }

    private void InitializePools()
    {
        int monsterCount = monsterDataParser.GetMonsterCount();

        for (int i = 0; i < monsterCount; i++)
        {
            MonsterData monsterData = monsterDataParser.GetMonsterData(i);
            monsterDatas.Add(monsterData);

            // ResourceManager�� ����Ͽ� ���� �������� �����ɴϴ�.
            MonsterStateController prefab = ResourceManager.Instance.GetResource<MonsterStateController>(monsterData.Name);
            if (prefab != null)
            {
                ObjectPool<MonsterStateController> pool = new ObjectPool<MonsterStateController>(prefab);
                monsterPools.Add(pool);
            }
            else
            {
                Debug.LogError($"{monsterData.Name}��� ���� ������Ʈ�� ���ҽ� ������ �������� �ʽ��ϴ�.");
            }
        }
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            MonsterData monsterData = monsterDatas[currentMonsterIndex];
            ObjectPool<MonsterStateController> pool = monsterPools[currentMonsterIndex];

            MonsterStateController monsterInstance = pool.GetObject();
            monsterInstance.transform.position = transform.position;  // ���� ��ġ ����

            // MonsterInfo �ʱ�ȭ
            if (monsterInstance.monsterInfo != null)
            {
                monsterInstance.monsterInfo.Initialize(monsterData);
                monsterInstance.ResetMonster();
            }
            else
            {
                Debug.LogError("MonsterInfo is null on the spawned monster.");
            }

            currentMonsterIndex = (currentMonsterIndex + 1) % monsterDatas.Count;

            yield return new WaitForSeconds(2); // 2�� �������� ���� ����
        }
    }


    public void ReturnMonsterToPool(MonsterStateController monster)
    {
        // �ش� ���͸� ��ȯ�� Ǯ�� ã���ϴ�.
        for (int i = 0; i < monsterDatas.Count; i++)
        {
            if (monster.name.Contains(monsterDatas[i].Name))
            {
                monsterPools[i].ReturnObject(monster);
                break;
            }
        }
    }
}
