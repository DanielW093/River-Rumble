using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour {

	public float zMoveSpeed = 4f;

	
	// Update is called once per frame
	void Update () {
		if(GameManager.gameRunning)
		{
			float moveAmount = zMoveSpeed * Time.deltaTime;

			transform.Translate(0, 0, -moveAmount);
		}
	}
}
