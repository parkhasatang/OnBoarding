using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 100;
    public Rigidbody2D rb;
    private ObjectPool<Arrow> pool;
    public float detectionRadius = 0.5f; // 감지 반경

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    public void Initialize(ObjectPool<Arrow> pool)
    {
        this.pool = pool;
    }

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        if (hits.Length > 0)
        {
            Collider2D closestCollider = null;
            float closestDistance = Mathf.Infinity;

            foreach (var hit in hits)
            {
                if (hit.GetComponent<MonsterStateController>() != null)
                {
                    float distance = Vector2.Distance(transform.position, hit.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCollider = hit;
                    }
                }
            }

            if (closestCollider != null)
            {
                MonsterStateController monster = closestCollider.GetComponent<MonsterStateController>();
                monster.TakeDamage(damage, transform.right); // 화살의 방향에 따라 넉백
                ReturnToPool(); // 맞았을 경우 화살을 풀로 반환
            }
        }
    }

    void ReturnToPool()
    {
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        pool.ReturnObject(this);
    }
}
