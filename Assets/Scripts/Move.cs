using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Move Instance { set; get; }

    public bool isRunning;
    public bool isJumping;
    private bool isDraging;
    private bool firstTouch;

    private int countDashes;
    public int aditionalDashes;

    public float firstFreeFallSpeed;
    public float laneDistance;
    public float sideJumpSpeed;
    public float gravity;
    public float gravityMultiplier;
    private float baseGravity;
    public float verticalJumpForce;
    public float verticalVelocity;
    public float movingSpeed;

    private float speedIncreaseLastTick;
    public float speedIncreaseTime;
    public float speedIncreaseAmount;
    private int desiredLane = 1;


    public Animator anim { set; get; }


    public Vector3 targetPosition, jumpDir;
    private Vector2 startTouch, swipeDelta;

    public Quaternion baseRotation;

    private CharacterController controller;

    public ParticleSystem dashEffectLeft;
    public ParticleSystem dashEffectRight;
    public ParticleSystem jumpEffect;
    public ParticleSystem dashDownEffect;
    public ParticleSystem collectEffect;



    private void Start()
    {
        Instance = this;
        isRunning = false;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        baseGravity = gravity;
        movingSpeed = Math.Abs(movingSpeed);
    }


    void Update()
    {

        if(!firstTouch)
        {
            verticalVelocity -= firstFreeFallSpeed * Time.deltaTime;
            controller.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));

            if (controller.isGrounded)
            {
                isRunning = true;
                firstTouch = true;
            }
        }


        if (isRunning)
        {


            

            if (controller.isGrounded)
            {
                verticalVelocity = -0.01f;
                isJumping = false;
                countDashes = 0;
                gravity = baseGravity;
                anim.SetBool("isGrounded", true);
                anim.SetBool("slideJump", false);
            }

            else
            {
                isJumping = true;
                anim.SetBool("isGrounded", false);
                verticalVelocity -= gravity * Time.deltaTime;
 
            }

            WsadControll();

            //SwipeControll();
            SpeedIcrease();

            //Where we should be

            Vector3 targerPosition = transform.position.x * new Vector3 (1, 0, 0);
            if (desiredLane == 0)
                targerPosition += new Vector3(0, 0, 1) * laneDistance;
            else if (desiredLane == 2)
                targerPosition += new Vector3(0, 0, -1) * laneDistance;

            //Move delta

            Vector3 moveVector = Vector3.zero;
            moveVector.x = movingSpeed;
            moveVector.y = verticalVelocity;
            moveVector.z = (targerPosition - transform.position).z * sideJumpSpeed;

            //Move Gimper

            controller.Move(moveVector * Time.deltaTime);


            anim.SetInteger("Speed", (int)movingSpeed);



        }
        else
        {
            verticalVelocity = -5.0f;
            jumpDir = new Vector3(0, verticalVelocity, 0);
            controller.Move(jumpDir * Time.deltaTime);
        }




    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += goingRight ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    private void CreateDashEffect(int whichOne)
    {
        if (whichOne == 0)
            dashEffectRight.Play();
        if (whichOne == 1)
            dashEffectLeft.Play();
        if (whichOne == 2)
            jumpEffect.Play();
        if (whichOne == 3)
            dashDownEffect.Play();
    }

    private void StartSliding()
    {
        if (!anim.GetBool("sliding"))
        {
            controller.height = (controller.height / 4) * 3;
            controller.center = new Vector3(controller.center.x, (controller.center.y / 4) * 3, controller.center.z);
            anim.SetBool("sliding", true);
            Invoke("StopSliding", 1.0f);
        }
    }

    private void StopSliding()
    {
        anim.SetBool("sliding", false);
        controller.height = (controller.height / 3) * 4;
        controller.center = new Vector3(controller.center.x, (controller.center.y / 3) * 4, controller.center.z);
    }

    private void Crash()
    {
        isRunning = false;
        Sounds.Instance.PlayDying();
        controller.transform.position = new Vector3(controller.transform.position.x -1, controller.transform.position.y, controller.transform.position.z);
        anim.SetTrigger("death");
        GameMenager.Instance.OnDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Coin":
                collectEffect.Play();
                break;
            case "Obstacle":
                Crash();
                break;
        }
    }

    private void SpeedIcrease()
    {
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            movingSpeed += speedIncreaseAmount;
            Sounds.Instance.mainMusic.pitch += 0.01f;
        }
    }    

    private void WsadControll()
    {

            if (Input.GetKeyDown("a") && countDashes <= aditionalDashes)
            {

                MoveLane(false);
                if (controller.isGrounded)
                    
                CreateDashEffect(1);
                anim.SetTrigger("dashLeft");
                countDashes++;
            }

            if (Input.GetKeyDown("d") && countDashes <= aditionalDashes)
            {
                MoveLane(true);
                if(controller.isGrounded)
                    CreateDashEffect(0);
                anim.SetTrigger("dashRight");
                countDashes++;
            }


            if (Input.GetKeyDown("w"))
            {
                if (!isJumping)
                {
                    isJumping = true;
                    verticalVelocity = verticalJumpForce;
                    CreateDashEffect(2);
                    if (anim.GetBool("sliding"))
                    {
                        anim.SetTrigger("slideJump");
                    }
                    else
                    {
                        anim.SetTrigger("Jump");
                    }
                }
            }
            if (Input.GetKeyDown("s"))
            {
                if (isJumping)
                {
                    gravity = gravity * gravityMultiplier;
                    CreateDashEffect(3);
                }
                if (controller.isGrounded)
                {
                    StartSliding();
                }
            
            }
    }

    private void SwipeControll()
    {
        #region Swipe Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = Input.touches[0].position;
                isDraging = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }

        #endregion

        #region Swipe conditions
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;

        }

        if (swipeDelta.magnitude > 40)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            #endregion


            #region Swipe directions
            if (Mathf.Abs(x) > Math.Abs(y) && countDashes <= aditionalDashes)
            {

                if (x < 0 && controller.transform.position.z < laneDistance)
                {

                    CreateDashEffect(1);
                    anim.SetTrigger("dashLeft");
                }
                else if (x >= 0 && controller.transform.position.z > -laneDistance)
                {

                    CreateDashEffect(0);
                    anim.SetTrigger("dashRight");
                }


            }

            if (Mathf.Abs(x) <= Math.Abs(y))
            {
                if (y < 0)
                {
                    if (isJumping)
                    {
                        gravity = gravity * gravityMultiplier;
                        CreateDashEffect(3);
                    }
                    if (controller.isGrounded)
                    {
                        StartSliding();
                    }
                }
                else
                {
                    if (!isJumping)
                    {
                        isJumping = true;
                        verticalVelocity = verticalJumpForce;
                        CreateDashEffect(2);
                        if (anim.GetBool("sliding"))
                        {
                            anim.SetBool("slideJump", true);
                        }
                        else
                        {
                            anim.SetBool("Jump", true);
                        }
                    }
                }

            }
            Reset();
        }
        #endregion
    }

    public void ContinueFromDeath()
    {
        anim.SetTrigger("FromDeath");
        isRunning = true;
        gravity = baseGravity;
        controller.Move(new Vector3(0.0f, 15.0f, 0.0f));
    }


}

