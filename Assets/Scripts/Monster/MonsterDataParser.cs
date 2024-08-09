using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MonsterDataParser : MonoBehaviour
{
    private List<MonsterData> monsterDataList = new List<MonsterData>();

    private void Start()
    {
        LoadMonsterData("MonsterData");
    }

    private void LoadMonsterData(string fileName)
    {
        // ResourceManager를 통해 CSV 파일 불러오기
        TextAsset monsterDataFile = ResourceManager.Instance.GetResource<TextAsset>(fileName);
        if (monsterDataFile == null)
        {
            Debug.LogError($"{fileName}라는 몬스터 데이터 파일이 존재 하지 않습니다.");
            return;
        }

        using (StringReader reader = new StringReader(monsterDataFile.text))
        {
            bool isFirstLine = true;
            while (reader.Peek() != -1)
            {
                var line = reader.ReadLine();
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                var values = line.Split(',');
                string name = values[0];
                string grade = values[1];
                float speed = float.Parse(values[2]);
                int health = int.Parse(values[3]);

                MonsterData monsterData = new MonsterData(name, grade, speed, health);
                monsterDataList.Add(monsterData);
            }
        }
    }

    public MonsterData GetMonsterData(int index)
    {
        if (index < 0 || index >= monsterDataList.Count)
        {
            Debug.LogError("Index out of range in MonsterDataParser.");
            return null;
        }
        return monsterDataList[index];
    }

    public int GetMonsterCount()
    {
        return monsterDataList.Count;
    }
}
