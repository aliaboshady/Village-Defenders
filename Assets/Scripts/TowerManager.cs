using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    TowerButton towerButtonPressed;
	List<Transform> filledPositions = new List<Transform>();

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if (towerButtonPressed != null && hit && hit.transform.tag == "Buildground")
			{
				if (!filledPositions.Contains(hit.transform))
				{
					filledPositions.Add(hit.transform);
					PlaceTower(hit.transform.position);
				}
			}
		}
	}

	void PlaceTower(Vector2 point)
	{
		GameObject newTower = Instantiate(towerButtonPressed.towerObject, point, Quaternion.identity);
	}

	public void SelectedTower(TowerButton selectedButton)
	{
		towerButtonPressed = selectedButton;
	}
}
