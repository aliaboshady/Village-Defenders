using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform exitPoint;
    [SerializeField] GameObject path;
    [SerializeField] float navigationUpdate;
    int target = 1;

    float navigationTime = 0;
	Transform[] wayPoints;

	private void Start()
	{
		wayPoints = path.GetComponentsInChildren<Transform>();
	}

	private void Update()
	{
		if(wayPoints != null)
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
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Checkpoint")
		{
			target++;
		}
		else if(collision.tag == "Finish")
		{
			Destroy(gameObject);
			GameManager.instance.DecrementEnemies();
		}
	}
}
