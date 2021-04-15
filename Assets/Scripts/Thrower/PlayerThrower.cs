using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrower : MonoBehaviour
{

    public int maxHealth = 10;
    public int currentHealth;
    public int hp;
    public int score;
    public int coins;

    public static PlayerThrower Instance { set; get; }

    public HealthBar healthBar;
    public TimeAttackBar timeBar;
    public ParticleSystem collectAnim;

    private void Start()
    {
        Instance = this;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }
    private void Update()
    {
        timeBar.SetTime(tMovement.Instance.timeFromLastAttack);

        if (currentHealth <= 0)
        {
           ThrowerMenager.Instance.OnDeath();
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
