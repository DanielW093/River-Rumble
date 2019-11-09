using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerData : MonoBehaviour {

	public AudioSource collision;

	public int maxHealth = 4;
	public Image healthBar;
	public int health;
	public Text scoreText;

	private int score = 0;
	private float multiple;
	private float startTime;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		multiple = healthBar.rectTransform.rect.width / maxHealth;
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		bool running = GameManager.gameRunning;
		if(running)
		{
			healthBar.rectTransform.sizeDelta = new Vector2(health*multiple, 65);
			healthBar.rectTransform.localPosition = new Vector3(0 - ((maxHealth-health)*multiple)/2, 0, 0);

			score = Mathf.RoundToInt(Time.time - startTime);
			scoreText.text = score.ToString();

			if(health <= 0)
			{
				GameObject.Find("FinalScoreText").GetComponent<Text>().text = score.ToString();
				GameObject.Find("GameOverScreen").GetComponent<Canvas>().enabled = true;
				GameManager.gameRunning = false;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Rocks") || other.CompareTag("Barrel") || other.CompareTag("EnemyShip"))
		{
			Vector3 newPos = other.transform.position;
			newPos.z = -12f; other.transform.position = newPos;
			collision.Play();
			ReduceHealth(1);
			Handheld.Vibrate();
		}
	}

	public void SetHealth(int h)
	{
		health = h;

		if(health < 0)
			health = 0;

		if(health > maxHealth)
			health = maxHealth;
	}

	public void ReduceHealth(int h)
	{
		health -= h;

		if(health < 0)
			health = 0;

		if(health > maxHealth)
			health = maxHealth;
	}

	public void IncreaseHealth(int h)
	{
		health += h;

		if(health < 0)
			health = 0;

		if(health > maxHealth)
			health = maxHealth;
	}
}
