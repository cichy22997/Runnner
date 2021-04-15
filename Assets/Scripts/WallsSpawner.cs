using System.Collections.Generic;
using UnityEngine;

public class WallsSpawner : MonoBehaviour
{
    public GameObject[] roadPrefabs;

    private Transform playerTransform;

    private float spawnX = 10.0f;
    public float roadLength;
    public int amountRoadsOnScreen;
    public float safeZone;
    public float roadsDistance;
    public float wallDistance = 19.0f;
    public float rotationX = -90.0f;
    public float rotattionY;
    public float rotationZ = 180.0f;
    public float wallHeight = 10.0f;

    private List<GameObject> activeWallRight;
    private List<GameObject> activeWallLeft;

    // Start is called before the first frame update
    void Start()
    {
        activeWallRight = new List<GameObject>();
        activeWallLeft = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amountRoadsOnScreen; i++)
            SpawnWall();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x - safeZone > (spawnX - amountRoadsOnScreen * roadLength))
        {
            SpawnWall();
            DeleteWalls();

        }
    }

    private void SpawnWall(int prefabIndex = -1)
    {
        GameObject wallRight;
        GameObject wallLeft;

        wallRight = Instantiate(roadPrefabs[0]) as GameObject;
        wallRight.transform.SetParent(transform);
        wallRight.transform.position = new Vector3(1 * spawnX, wallHeight, -wallDistance);
        wallRight.transform.rotation = Quaternion.Euler(rotationX, rotattionY, rotationZ);

        wallLeft = Instantiate(roadPrefabs[0]) as GameObject;
        wallLeft.transform.SetParent(transform);
        wallLeft.transform.position = new Vector3(1 * spawnX, wallHeight, wallDistance);

        activeWallRight.Add(wallRight);
        activeWallLeft.Add(wallLeft);
        spawnX += roadLength;

    }

    private void DeleteWalls()
    {
        Destroy(activeWallLeft[0]);
        activeWallLeft.RemoveAt(0);

        Destroy(activeWallRight[0]);
        activeWallRight.RemoveAt(0);

    }
}
