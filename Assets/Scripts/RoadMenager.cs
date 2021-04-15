
using System.Collections.Generic;
using UnityEngine;


public class RoadMenager : MonoBehaviour
{

    public GameObject[] roadPrefabs;

    private Transform playerTransform;

    private float spawnX = 1.0f;
    public float roadLength;
    public int amountRoadsOnScreen;
    public float safeZone;

    private List<GameObject> activeGround;

    // Start is called before the first frame update
    void Start()
    {

        activeGround = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i = 0; i<amountRoadsOnScreen; i++)
        {
            SpawnGround();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.x - safeZone>(spawnX-amountRoadsOnScreen*roadLength))
        {
            SpawnGround();
   
            DeleteGround();
        }
    }



    private void SpawnGround(int prefabIndex = -1)
    {
        GameObject ground;

        ground = Instantiate(roadPrefabs[0]) as GameObject;
        ground.transform.SetParent(transform);
        ground.transform.position = new Vector3(1 * spawnX, -0.1f, 0);

        activeGround.Add(ground);
        spawnX += roadLength;
    }




    private void DeleteGround()
    {
        Destroy(activeGround[0]);
        activeGround.RemoveAt(0);

    }

}
