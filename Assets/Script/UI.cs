﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
	public GameObject restartButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Game.instance.isEnd){
			restartButton.SetActive(true);
		}
	}

	public void OnClickRestart(){
		Game.instance.isRestart = true;
	}
}
