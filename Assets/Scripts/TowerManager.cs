using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
	[SerializeField, Range(0, 1)] float selectedTowerAlpha = 0.5f;
    TowerButton towerButtonPressed;
	List<Transform> filledPositions = new List<Transform>();
	GameObject selectedObject;
	SpriteRenderer selectedObjectRenderer;

	private void Start()
	{
		selectedObject = new GameObject();
		selectedObject.name = "Selected Object";
		selectedObjectRenderer = selectedObject.AddComponent<SpriteRenderer>();
		selectedObjectRenderer.sortingLayerName = "Towers";
		selectedObjectRenderer.sortingOrder = 10;
		selectedObjectRenderer.color = new Vector4(1, 1, 1, selectedTowerAlpha);
	}

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
					if(GameManager.instance.currentMoney >= towerButtonPressed.cost)
					{
						GameManager.instance.currentMoney -= towerButtonPressed.cost;
						filledPositions.Add(hit.transform);
						PlaceTower(hit.transform.position);
					}
				}
			}
		}

		if(towerButtonPressed != null)
		{
			SelectedTowerStickToMouse();
		}
	}

	void PlaceTower(Vector2 point)
	{
		Instantiate(towerButtonPressed.towerObject, point, Quaternion.identity);
	}

	void SelectedTowerStickToMouse()
	{
		Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		selectedObject.transform.position = new Vector3(newPos.x, newPos.y, 0);
	}

	public void ShowSelectedTower()
	{
		selectedObjectRenderer.sprite = towerButtonPressed.sprite;
	}

	public void SelectedTower(TowerButton selectedButton)
	{
		towerButtonPressed = selectedButton;
	}
}
