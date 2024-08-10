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
            Debug.LogError("MonsterDataParser 컴포넌트를 찾지 못하였습니다.");
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

            // ResourceManager를 사용하여 몬스터 프리팹을 가져옵니다.
            MonsterStateController prefab = ResourceManager.Instance.GetResource<MonsterStateController>(monsterData.Name);
            if (prefab != null)
            {
                ObjectPool<MonsterStateController> pool = new ObjectPool<MonsterStateController>(prefab);
                monsterPools.Add(pool);
            }
            else
            {
                Debug.LogError($"{monsterData.Name}라는 몬스터 오브젝트가 리소스 폴더에 존재하지 않습니다.");
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
            monsterInstance.transform.position = transform.position;  // 스폰 위치 설정

            // MonsterInfo 초기화
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

            yield return new WaitForSeconds(2); // 2초 간격으로 몬스터 생성
        }
    }


    public void ReturnMonsterToPool(MonsterStateController monster)
    {
        // 해당 몬스터를 반환할 풀을 찾습니다.
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
