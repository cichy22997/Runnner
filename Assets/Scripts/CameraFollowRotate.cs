using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRotate : MonoBehaviour
{


    public Transform Gimper;
    public Transform Lysy;
    public Transform SonGimper;
    public Transform Tata;
    private Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

    private void Awake()
    {
        switch (CameraMenu.Instance.whichChar)
        {
            case 0:
                target = Gimper;
                break;
            case 1:
                target = Lysy;
                break;
            case 2:
                target = SonGimper;
                break;
            case 3:
                target = Tata;
                break;
        }
        
    }

    void Update()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
		

	}


}


