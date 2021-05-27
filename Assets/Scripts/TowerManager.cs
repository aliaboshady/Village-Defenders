using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    TowerButton towerButtonPressed;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if (hit && hit.transform.tag == "Buildground")
			{
				PlaceTower(hit.transform.position);
			}
		}
	}

	void PlaceTower(Vector2 point)
	{
		if (towerButtonPressed == null) return;
		GameObject newTower = Instantiate(towerButtonPressed.towerObject, point, Quaternion.identity);
	}

	public void SelectedTower(TowerButton selectedButton)
	{
		towerButtonPressed = selectedButton;
	}
}
