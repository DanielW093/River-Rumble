using UnityEngine;
using System.Collections;

public class ObstacleSpawning : MonoBehaviour {

	public float spawnDelay = 1f;
	public float minSpawnDelay = 0.85f;
	public float xSpawnBounds = 2.5f;
	public float zSpawnPos = 15f;

	public GameObject[] rocks = new GameObject[5];
	public GameObject[] barrels = new GameObject[5];
	public GameObject[] ships = new GameObject[5];

	private float lastSpawnTime;
	private float startTime;

	private int rockIt = 0;
	private int barrelIt = 0;
	private int shipIt = 0;
	private int level = 3;


	// Use this for initialization
	void Start () {
		lastSpawnTime = spawnDelay;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.gameRunning)
		{
			if(Time.time - startTime > 3 && spawnDelay - 0.025 > minSpawnDelay)
			{
				spawnDelay -= 0.025f;
				startTime = Time.time;
				Debug.Log("Spawn Time Reduced");
			}


			//Spawn obstacles
			if(Time.time - lastSpawnTime > spawnDelay)
			{
				int obstacleType = Random.Range(0, level);

				GameObject obstacle;

				switch (obstacleType)
				{
				case 0:
					//Debug.Log("Rocks " + rockIt);
					obstacle = rocks[rockIt]; //Set obstacle
					rockIt++; //Iterate the rock iterator
					if(rockIt >= rocks.Length) //Set iterator back to 0 when necessary
						rockIt = 0;
					break;
				case 1:
					//Debug.Log("Barrels " + barrelIt);
					obstacle = barrels[barrelIt]; //Set obstacle
					obstacle.GetComponent<BobScript>().enabled = true;
					barrelIt++; //Iterate the barrel iterator
					if(barrelIt >= barrels.Length) //Set iterator back to 0 when necessary
						barrelIt = 0;
					break;
				case 2:
					//Debug.Log("Ships: " + shipIt);
					obstacle = ships[shipIt]; //Set obstacle
					obstacle.GetComponent<BobScript>().enabled = true;
					shipIt++; //Iterate the ship iterator
					if(shipIt >= ships.Length) //Set iterator back to 0 when necessary
						shipIt = 0;
					break;
				default:
					//Debug.Log("Rocks " + rockIt);
					obstacle = rocks[rockIt]; //Set obstacle
					rockIt++; //Iterate the rock iterator
					if(rockIt >= rocks.Length) //Set iterator back to 0 when necessary
						rockIt = 0;
					break;
				}
					
				float xPos = Random.Range(-xSpawnBounds, xSpawnBounds); //Decide random x position

				//Set position of obstacle
				Vector3 newPos = obstacle.transform.position;
				newPos.x = xPos; newPos.z = zSpawnPos;
				obstacle.transform.position = newPos;

				//float rotation = Random.Range(0,360);
	//			//Set rotation of obstacle
	//			Vector3 newRot = obstacle.transform.eulerAngles;
	//			newRot.y = rotation; obstacle.transform.eulerAngles = newRot;

				//Enable movement
				obstacle.GetComponent<ObstacleMovement>().enabled = true;

				//Reset start time
				lastSpawnTime = Time.time;
			}

			//Pause obstacles
			foreach(GameObject g in rocks)
			{
				if(g.transform.position.z <= -11)
					g.GetComponent<ObstacleMovement>().enabled = false;
			}
			foreach(GameObject g in barrels)
			{
				if(g.transform.position.z <= -11)
				{
					g.GetComponent<ObstacleMovement>().enabled = false;
					g.GetComponent<BobScript>().enabled = false;
				}
			}
			foreach(GameObject g in ships)
			{
				if(g.transform.position.z <= -11)
				{
					g.GetComponent<ObstacleMovement>().enabled = false;
					g.GetComponent<BobScript>().enabled = false;
				}
			}
		}
	}
}
