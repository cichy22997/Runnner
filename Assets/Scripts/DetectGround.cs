using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    public GameObject player;
    public float deploymentHeight;


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * deploymentHeight);


        if (Physics.Raycast(landingRay, out hit, deploymentHeight))
        {
            if (hit.collider.tag == "Ground" && Move.Instance.anim.GetFloat("verticalVelocity") < 5.0f /*&& Move.Instance.anim.GetFloat("Height") >= 0.3f*/)
            {
                Land();
            }
        }

    }
    private void Land()
    {
        //Move.Instance.anim.SetBool("landing",true);
       // Move.Instance.anim.SetBool("Jump", false);
    }
}
