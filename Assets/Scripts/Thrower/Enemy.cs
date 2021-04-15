using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance { get; set; }
    public float movementSpeed = 0.5f;
    public int damage = 1;
    private Rigidbody enemy;
    private CapsuleCollider colliderCapsule;
    private BoxCollider colliderBox;
    public float baseMovement;
    public float timeOfDeath;
    private bool hitted;

    public ParticleSystem onDeath1;
    public ParticleSystem onDeath2;

    public Animator EnemyAnim;

    private void Start()
    {
        Instance = this;
        enemy = GetComponent<Rigidbody>();
        colliderCapsule = GetComponent<CapsuleCollider>();
        colliderBox = GetComponent<BoxCollider>();
        baseMovement = movementSpeed;
        hitted = false;

    }

    private void Update()
    {
        enemy.transform.position += new Vector3(-1f, 0f, 0f) * movementSpeed;
        if (ThrowerMenager.Instance.gamePaused)
            movementSpeed = 0f;
        else
        {
            if(!hitted)
                movementSpeed = baseMovement;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hitted = true;
            movementSpeed = 0f;

            colliderCapsule.enabled = false;
            colliderBox.enabled = false;

            onDeath1.Play();
            onDeath2.Play();

            Sounds.Instance.PlayCollect();

            PlayerThrower.Instance.collectAnim.Play();

            ScoreThrower.Instance.subs++;

            EnemyAnim.SetTrigger("Dying");

            Invoke("Destroy", timeOfDeath);
        }
        if (other.tag == "PlayerWall")
        {
            Sounds.Instance.PlayDying();
            PlayerThrower.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}