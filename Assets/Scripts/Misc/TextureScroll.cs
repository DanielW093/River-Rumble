using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {

	public float scrollXSpeed;
	public float scrollYSpeed;

	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.gameRunning)
		{
			float xOffset = Time.time * scrollXSpeed;
			float yOffset = Time.time * scrollYSpeed;
			rend.material.SetTextureOffset("_MainTex", new Vector2(xOffset,yOffset));
		}
	}
}
