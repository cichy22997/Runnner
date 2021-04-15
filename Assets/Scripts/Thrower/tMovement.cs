using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tMovement : MonoBehaviour
{

    private int desiredLane;
    public int laneDistance = 14;
    public float changingLaneSpeed;
    private CharacterController controller;

    public static tMovement Instance { set; get; }

    public Rigidbody projectile;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public int lineSegment = 10;
    private Camera cam;
    private Vector3 targetPoint;
    public float distanceIncrease = 400;
    public float increaseSpeed;
    private bool increaseRising;
    public float minDistance = 200;

    public float timeFromLastAttack;
    public float timeBetweenAttacks = 0.5f;

    public Animator anim;






    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        controller = GetComponent<CharacterController>();
        desiredLane = 1;
        increaseRising = true;
        cam = Camera.main;
        anim = GetComponent<Animator>();
        lineVisual.positionCount = lineSegment;

    }

    // Update is called once per frame
    void Update()
    {
        timeFromLastAttack += Time.deltaTime;

        Attack();
        if (Input.GetKeyDown("a"))
        {
            MoveLane(false);
            anim.SetTrigger("DashLeft");
        }
        if (Input.GetKeyDown("d"))
        {
            MoveLane(true);
            anim.SetTrigger("DashRight");
        }
            //Where we should be

            Vector3 targerPosition = transform.position.x * new Vector3(1, 0, 0);
        if (desiredLane == 0)
            targerPosition += new Vector3(0, 0, 1) * laneDistance;
        else if (desiredLane == 2)
            targerPosition += new Vector3(0, 0, -1) * laneDistance;

        //Move delta

        Vector3 moveVector = Vector3.zero;

        moveVector.z = (targerPosition - transform.position).z * changingLaneSpeed;

        //Move Gimper

        controller.Move(moveVector * Time.deltaTime);

    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += goingRight ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private void Attack()
    {

        targetPoint = new Vector3(960.0f, distanceIncrease, 0.0f);
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(targetPoint);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, 1f);

            Visualize(vo);


            if (Input.GetKey("space"))
            {
                if (distanceIncrease > 1100.0f)
                    increaseRising = false;

                if (distanceIncrease < minDistance)
                    increaseRising = true;

                if (increaseRising)
                    distanceIncrease += increaseSpeed;
                else
                    distanceIncrease -= increaseSpeed;
            }
            if (Input.GetKeyUp("space") && ThrowerMenager.Instance.alive == true)
            {
                if (timeFromLastAttack > timeBetweenAttacks)
                {
                    timeFromLastAttack = 0f;
                    Rigidbody obj = Instantiate(projectile, shootPoint.position, Quaternion.identity);
                    obj.velocity = vo;
                    anim.SetTrigger("Throw");
                }
                distanceIncrease = minDistance;
                increaseRising = true;
            }



        }
    }

    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);
        }
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz * time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

        result.y = sY;

        return result;
    }
}
