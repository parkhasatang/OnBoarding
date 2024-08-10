using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform firePoint; // ȭ���� �߻�� ��ġ
    public Arrow arrowPrefab; // ȭ�� ������
    private ObjectPool<Arrow> arrowPool; // ȭ�� Ǯ

    public float fireInterval = 1f; // ȭ�� �߻� ���� (��)

    private void Awake()
    {
        // �ʱ�ȭ �� ȭ�� �������� �Ѱ��ִ� ObjectPool ����
        arrowPool = new ObjectPool<Arrow>(arrowPrefab);
    }

    private void Start()
    {
        StartCoroutine(AutoShoot());
    }

    IEnumerator AutoShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    void Shoot()
    {
        Arrow arrow = arrowPool.GetObject();
        arrow.transform.position = firePoint.position;
        arrow.transform.rotation = firePoint.rotation;
        arrow.gameObject.SetActive(true);

        arrow.rb.velocity = firePoint.right * arrow.speed;
        arrow.Initialize(arrowPool); // ȭ�쿡 Ǯ ������ �Ѱ���
    }
}
