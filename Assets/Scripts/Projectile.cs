using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType {rock, arrow, fireball};

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float aliveTime = 5f;

	[HideInInspector] public Vector2 direction;

	public int attackStrength;
    public ProjectileType projectileType;

	private void Start()
	{
		Destroy(gameObject, aliveTime);
	}

	private void Update()
	{
		transform.position = (Vector2)transform.position + direction.normalized * speed * Time.deltaTime;
	}
}
