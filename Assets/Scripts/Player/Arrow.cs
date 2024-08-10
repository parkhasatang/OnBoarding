using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 100;
    public Rigidbody2D rb;
    private ObjectPool<Arrow> pool;
    public float detectionRadius = 0.5f; // ���� �ݰ�

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
                monster.TakeDamage(damage, transform.right); // ȭ���� ���⿡ ���� �˹�
                ReturnToPool(); // �¾��� ��� ȭ���� Ǯ�� ��ȯ
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
