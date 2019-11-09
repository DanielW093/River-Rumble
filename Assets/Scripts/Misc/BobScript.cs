using UnityEngine;
using System.Collections;

public class BobScript : MonoBehaviour {

	public float bobSpeed;
	public float amplitude;

	private float startY;

	// Use this for initialization
	void Start () {
		startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.gameRunning)
		{
			Vector3 newPos = transform.position;

			newPos.y = startY + amplitude*Mathf.Sin(bobSpeed * Time.time);

			transform.position = newPos;
		}
	}
}
