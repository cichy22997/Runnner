using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private Vector3 rotationSpeed;
    private float basePosition;
    float speed = 0.5f;
    float delta = 1f;  //delta is the difference between min y to max y.

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = new Vector3(0, 0, 2);
        basePosition = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed);

        float positionY = basePosition +Mathf.PingPong(speed * Time.time, delta);
        Vector3 pos = new Vector3(transform.position.x, positionY, transform.position.z);
        transform.position = pos;


    }
}
