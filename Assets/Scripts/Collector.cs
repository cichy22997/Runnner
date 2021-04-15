using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Sounds.Instance.PlayCollect();
            gameObject.SetActive(false);
            Score.Instance.cashAmount += 1;
            Invoke("Appear", 15.0f);
        }    
    }
    private void Appear()
    {
        gameObject.SetActive(true);
    }
}
