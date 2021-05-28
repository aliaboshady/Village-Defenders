using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform exitPoint;
    [SerializeField] GameObject path;
    [SerializeField] float navigationUpdate;
    [SerializeField] int maxHealth;
    [SerializeField] float hurtWait;
    [SerializeField] int cost;
    int target = 1;

    float navigationTime = 0;
    float hurtWaitPassed = 0;
	int currentHealth = 0;
	Transform[] wayPoints;
	Animator animator;
	bool canMove = true;
	bool isHurt = false;
	bool isDead = false;

	private void Start()
	{
		wayPoints = path.GetComponentsInChildren<Transform>();
		animator = GetComponent<Animator>();
		currentHealth = maxHealth;
	}

	private void Update()
	{
		if(wayPoints != null && canMove)
		{
			navigationTime += Time.deltaTime;
			if(navigationTime >= navigationUpdate)
			{
				if(target < wayPoints.Length)
				{
					transform.position = Vector2.MoveTowards(transform.position, wayPoints[target].position, navigationTime);
				}
				else
				{
					transform.position = Vector2.MoveTowards(transform.position, exitPoint.position, navigationTime);
				}

				navigationTime = 0;
			}
		}

		if (isHurt)
		{
			hurtWaitPassed += Time.deltaTime;
			if(hurtWaitPassed >= hurtWait)
			{
				isHurt = false;
			}
		}

		if(currentHealth <= 0 && !isDead)
		{
			isDead = true;
			GameManager.instance.currentMoney += cost;
			Die();
		}

		if (isDead)
		{
			canMove = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Checkpoint")
		{
			target++;
		}
		else if(collision.tag == "Finish")
		{
			GameManager.instance.DecrementEnemies(gameObject);
			Destroy(gameObject);
		}
		else if(collision.tag == "Projectile")
		{
			int damage = collision.GetComponent<Projectile>().attackStrength;
			DecreaseHealth(damage);
			Destroy(collision.gameObject);
		}
	}

	void DecreaseHealth(int damage)
	{
		if (!isHurt)
		{
			isHurt = true;
			currentHealth -= damage;
			Hurt();
		}
	}

	void Hurt()
	{
		canMove = false;
		animator.SetTrigger("Hurt");
	}

	void CanMove()
	{
		canMove = true;
	}

	void Die()
	{
		animator.SetTrigger("Die");
		canMove = false;
		GameManager.currentEnemies.Remove(gameObject);
		GameManager.instance.currentEnemiesOnScreen--;
		GetComponent<BoxCollider2D>().enabled = false;
	}
}
