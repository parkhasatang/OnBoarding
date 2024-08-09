using UnityEngine;
using UnityEngine.Tilemaps;

public class BackGroundScroller : MonoBehaviour
{
    public Tilemap[] tilemaps;
    public float scrollSpeed = 2f;
    public float tilemapWidth;

    private void Update()
    {
        for (int i = 0; i < tilemaps.Length; i++)
        {
            tilemaps[i].transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        if (tilemaps[0].transform.position.x <= -tilemapWidth)
        {
            RepositionTilemap(0, 1);
        }
        else if (tilemaps[1].transform.position.x <= -tilemapWidth)
        {
            RepositionTilemap(1, 0);
        }
    }

    private void RepositionTilemap(int leftIndex, int rightIndex)
    {
        Vector3 rightPosition = tilemaps[rightIndex].transform.position;
        Vector3 leftPosition = tilemaps[leftIndex].transform.position;

        leftPosition.x = rightPosition.x + tilemapWidth;
        tilemaps[leftIndex].transform.position = leftPosition;
    }
}
