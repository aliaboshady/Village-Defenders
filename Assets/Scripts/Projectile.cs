using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType {rock, arrow, fireball};

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float aliveTime = 5f;
    [SerializeField] float rockRotationSpeed = 5f;

	[HideInInspector] public Vector2 direction;

	public int attackStrength;
    public ProjectileType projectileType;

	private void Start()
	{
		if(projectileType != ProjectileType.rock)
		{
			transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);
		}
		Destroy(gameObject, aliveTime);
	}

	private void Update()
	{
		if(projectileType == ProjectileType.rock)
		{
			transform.Rotate(new Vector3(0, 0, rockRotationSpeed * Time.deltaTime));
		}
		transform.position = (Vector2)transform.position + direction.normalized * speed * Time.deltaTime;
	}
}
