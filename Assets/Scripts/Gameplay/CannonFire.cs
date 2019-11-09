using UnityEngine;
using System.Collections;

public class CannonFire : MonoBehaviour {

	public Transform rightFront;
	public Transform rightBack;
	public Transform leftFront;
	public Transform leftBack;

	public float fireDelay = 1.0f;

	public AudioSource cannonFire;
	public AudioSource collision;

	private float startTime;

	private Vector2 startPos;
	private Vector2 swipeDirection;
	private bool directionChosen;

	// Use this for initialization
	void Start () {
		startTime = fireDelay;
	}
	
	// Update is called once per frame
	void Update () {
		bool running = GameManager.gameRunning;
		if(running)
		{
			if(Input.touchCount > 0)
			{
				if(Time.time - startTime > fireDelay)
				{
					Touch touch = Input.GetTouch(0);

					switch(touch.phase)
					{
						//Record initial touch position
					case TouchPhase.Began:
						startPos = touch.position;
						directionChosen = false;
						break;
					case TouchPhase.Moved:
						swipeDirection = touch.position - startPos;
						swipeDirection.y = 0;
						break;
					case TouchPhase.Ended:
						directionChosen = true;
						startTime = Time.time;
						break;
					}
				}
			}

			if(directionChosen)
			{
				if(swipeDirection.x < 0)
				{
					//Debug.Log("SHOOT LEFT");
					cannonFire.Play();
					RaycastHit hit1;
					RaycastHit hit2;
					if(Physics.Raycast(leftFront.position, -leftFront.right, out hit1, 30))
					{
						if(hit1.transform.CompareTag("EnemyShip"))
						{
							Vector3 newPos = hit1.transform.position;
							newPos.z = -12f; hit1.transform.position = newPos;
							collision.Play();
						}
					}
					if(Physics.Raycast(leftBack.position, -leftBack.right, out hit2, 30))
					{
						if(hit2.transform.CompareTag("EnemyShip"))
						{
							Vector3 newPos = hit2.transform.position;
							newPos.z = -12f; hit2.transform.position = newPos;
							collision.Play();
						}
					}
				}
				else if (swipeDirection.x > 0)
				{
					//Debug.Log("SHOOT RIGHT");
					cannonFire.Play();
					RaycastHit hit1;
					RaycastHit hit2;
					if(Physics.Raycast(rightFront.position, rightFront.right, out hit1, 30))
					{
						if(hit1.transform.CompareTag("EnemyShip"))
						{
							Vector3 newPos = hit1.transform.position;
							newPos.z = -12f; hit1.transform.position = newPos;
							collision.Play();
						}
					}
					if(Physics.Raycast(rightBack.position, rightBack.right, out hit2, 30))
					{
						if(hit2.transform.CompareTag("EnemyShip"))
						{
							Vector3 newPos = hit2.transform.position;
							newPos.z = -12f; hit2.transform.position = newPos;
							collision.Play();
						}
					}
				}

				directionChosen = false;
			}
		}
	}
}
