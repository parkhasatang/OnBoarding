using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform firePoint; // 화살이 발사될 위치
    public Arrow arrowPrefab; // 화살 프리팹
    private ObjectPool<Arrow> arrowPool; // 화살 풀

    public float fireInterval = 1f; // 화살 발사 간격 (초)

    private void Awake()
    {
        // 초기화 시 화살 프리팹을 넘겨주는 ObjectPool 생성
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
        arrow.Initialize(arrowPool); // 화살에 풀 참조를 넘겨줌
    }
}
