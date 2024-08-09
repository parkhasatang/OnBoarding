public class MonsterData
{
    public string Name { get; }
    public string Grade { get; }
    public float Speed { get; }
    public int Health { get; }

    public MonsterData(string name, string grade, float speed, int health)
    {
        Name = name;
        Grade = grade;
        Speed = speed;
        Health = health;
    }
}
