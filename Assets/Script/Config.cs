using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour{
	/**
		0 empty 1 top-left 2 top-right 3 bot-right 4 bot-left 5 top-bot 6 left-right
	 */
	public Sprite[] spriteBricks;
	public Sprite[] spritePipes;
	public Sprite[] spriteWaters;
	public Texture2D[] textWaters;
	public static Config instance;
	void Awake(){
		if (instance != null){
			Debug.LogWarning("instance!=null");
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update() {
	}
}
