using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public bool isStart = false;
	public bool isEnd = false;
	public bool isRestart = false;
	public Broad broad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isRestart) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("1");
		}
		if (isStart && broad!=null) {
			broad.isStart = true;
			broad = null;
		}
	}
}
