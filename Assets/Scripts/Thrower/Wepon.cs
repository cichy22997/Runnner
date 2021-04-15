using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    private int countHits;
    private float lifeTime;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            countHits++;

            if (countHits >= 2)
                Destroy(gameObject);
        }
    }
    public void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= 5.0f)
            Destroy(gameObject);

    }
}
