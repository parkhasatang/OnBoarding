using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MonsterDataParser : MonoBehaviour
{
    public List<MonsterData> monsterDataList = new List<MonsterData>();

    private void Start()
    {
        LoadMonsterData("MonsterData"); // Resource 폴더 내의 CSV 파일 이름
    }

    private void LoadMonsterData(string fileName)
    {
        TextAsset data = ResourceManager.Instance.GetResource<TextAsset>(fileName);
        if (data == null)
        {
            Debug.LogError($"Failed to load data from {fileName}. Make sure the file is placed in the Resources folder.");
            return;
        }

        using (StringReader reader = new StringReader(data.text))
        {
            bool isFirstLine = true;
            while (reader.Peek() != -1)
            {
                var line = reader.ReadLine();
                Debug.Log($"Reading line: {line}"); // 읽은 라인을 로그로 출력

                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                var values = line.Split(',');
                if (values.Length == 4)
                {
                    // 각 필드가 제대로 읽히는지 확인
                    Debug.Log($"Name: '{values[0]}', Grade: '{values[1]}', Speed: '{values[2]}', Health: '{values[3]}'");

                    string name = values[0].Trim();    // 공백 제거
                    string grade = values[1].Trim();   // 공백 제거
                    float speed;
                    int health;

                    // 속도와 체력 파싱 시 예외 처리
                    if (!float.TryParse(values[2].Trim(), out speed))
                    {
                        Debug.LogWarning($"Failed to parse speed for line: {line}");
                    }
                    if (!int.TryParse(values[3].Trim(), out health))
                    {
                        Debug.LogWarning($"Failed to parse health for line: {line}");
                    }

                    // 파싱된 데이터를 사용하여 MonsterData 객체 생성
                    MonsterData monsterData = new MonsterData(name, grade, speed, health);
                    monsterDataList.Add(monsterData);

                    // 파싱된 데이터를 로그로 출력
                    Debug.Log($"Parsed MonsterData: Name={monsterData.Name}, Grade={monsterData.Grade}, Speed={monsterData.Speed}, Health={monsterData.Health}");
                }
                else
                {
                    Debug.LogWarning($"Invalid data format in line: {line}");
                }
            }
        }

        Debug.Log($"Total monsters parsed: {monsterDataList.Count}");
    }


    public int GetMonsterCount()
    {
        return monsterDataList.Count;
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
}
