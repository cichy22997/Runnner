using UnityEngine;

public class GymBackground : MonoBehaviour
{
    private const float DISTANCE_TO_RESPAWN = 10.0f;

    public float scrollSpeed = -2.0f;
    public float totalLength;
    public bool isScrolling { set; get; }
    private float scrollLocation;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        isScrolling = true;

    }
    private void Update()
    {
        if (isScrolling)
            return;
        scrollLocation += scrollSpeed * Time.deltaTime;
        Vector3 newLocation = (playerTransform.position.x + scrollLocation) * new Vector3(1,0,0);
        transform.position = newLocation;

        if(transform.GetChild(0).transform.position.x < playerTransform.position.x - DISTANCE_TO_RESPAWN)
        {
            transform.GetChild(0).localPosition += new Vector3(1, 0, 0) * totalLength;
            transform.GetChild(0).SetSiblingIndex(transform.childCount);
        }
    }

}
