using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public GameObject shipModel; //The gameobject containing the ship model

	public float minTiltResponse; //The min amount of tilt that will elicit a response
	public float maxTiltResponse; //The max amount of tilt that will elicit a response
	public float moveSpeed; //The movement speed
	public float tiltAmount; //Total tilt
	public float tiltSpeed; //The tilt speed
	public float stabiliseSpeed; //Speed to return to normal
	public float moveBounds; //Outer bounds of movement

	//Movement Values Values
	private float deltaX = 0f; //Increase of x
	private float rot = 0f; //rotation of player spacecraft
	private float deltaRotA = 0f; //Increase of rotation - when starting to bank
	private float deltaRotD = 0f; //Increase of rotation - when going straight

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool running = GameManager.gameRunning;
		if(running)
		{
			//Calculate tilt to one decimal place
			float tilt = (Mathf.Round(Input.acceleration.x * 10)/10);
			tilt = Mathf.Clamp(tilt, -maxTiltResponse,maxTiltResponse);

			if(tilt < minTiltResponse && tilt > -minTiltResponse)
				tilt = 0f;

			//Rotate ship
			Vector3 rotation = shipModel.transform.eulerAngles;
			rotation.z = -rot;
			if(rotation.z > 180f)
				rotation.z -= 360f;
			shipModel.transform.eulerAngles = rotation;
			//Translate ship
			transform.Translate(deltaX, 0, 0);
		
			//Add delta rotation acceleration to rotation
			rot += deltaRotA;
			//Rotation - when starting the banking animation
			if(rot > tiltAmount) //Is rotation higher than allowed rotation?
			{
				//Limit
				rot = tiltAmount;
				deltaRotA = 0;
			}
			else if(rot < -tiltAmount) //Is it lower than lowest allowed rotation?
			{
				//Limit
				rot = -tiltAmount;
				deltaRotA = 0;
			}
			else //Neither?
				Bank(tilt); //Calculate bank

			//Is it out of left bound?
			if(transform.position.x < -moveBounds && deltaX < 0)
			{
				Bank(0); //Stop
			}
			if(transform.position.x > moveBounds && deltaX > 0)
			{
				Bank(0); //Stop
			}

			//Rotation - when finishing the banking animation
			rot += deltaRotD; //Add delta rotation deceleration
			if(deltaRotD < 0 && rot < 0 || deltaRotD > 0 && rot > 0)
			{
				rot = 0;
				deltaRotD = 0;
			}
		}
	}

	void Bank(float val){
		deltaX = val * moveSpeed * Time.deltaTime;
		deltaRotA = val * tiltSpeed * Time.deltaTime;

		if(val == 0)
		{
			if(rot > 0)
				deltaRotD = -(stabiliseSpeed * Time.deltaTime);
			else
				deltaRotD = stabiliseSpeed * Time.deltaTime;
		}
		else
			deltaRotD = 0;
	}
}
