using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] float attackWait;
    [SerializeField] float attackRadius;
    [SerializeField] GameObject projectile;

	GameObject closestEnemy = null;
	float attackWaitPassed = 0;

	private void Update()
	{
		ChooseClosestEnemy();

		attackWaitPassed += Time.deltaTime;
		if (attackWaitPassed >= attackWait && closestEnemy != null)
		{
			attackWaitPassed = 0;
			Attack();
		}
	}

	void Attack()
	{
		GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
		newProjectile.GetComponent<Projectile>().direction = closestEnemy.transform.position - transform.position;
	}

	void ChooseClosestEnemy()
	{
		float closestDistance = 10000000f;

		for (int i = 0; i < GameManager.currentEnemies.Count; i++)
		{
			GameObject enemy = GameManager.currentEnemies[i];
			float distance = Vector2.Distance(transform.position, enemy.transform.position);
			if (distance <= attackRadius && distance < closestDistance)
			{
				closestEnemy = enemy;
				closestDistance = distance;
			}
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}
}
