using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject[] roadPrefabs;

    private Transform playerTransform;

    private float spawnX = 10.0f;
    public float roadLength = 192.0f;
    public int amountRoadsOnScreen;
    public float safeZone = 200.0f;

    private List<GameObject> activeRoadsMiddle;
    private List<GameObject> activeRoadsLeft;
    private List<GameObject> activeRoadsRight;

    // Start is called before the first frame update
    void Start()
    {
        activeRoadsMiddle = new List<GameObject>();
        activeRoadsLeft = new List<GameObject>();
        activeRoadsRight = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amountRoadsOnScreen; i++)
        {
            SpawnRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x - safeZone > (spawnX - amountRoadsOnScreen * roadLength))
        {
            SpawnRoad();
            DeleteRoad();
        }
    }
    private void SpawnRoad(int prefabIndex = -1)
    {
        GameObject goM;

        goM = Instantiate(roadPrefabs[0]) as GameObject;
        goM.transform.SetParent(transform);
        goM.transform.position = new Vector3(1, 0, 0) * spawnX;

        activeRoadsMiddle.Add(goM);

        spawnX += roadLength;
    }



    private void DeleteRoad()
    {
        Destroy(activeRoadsMiddle[0]);
        activeRoadsMiddle.RemoveAt(0);

    }


}
