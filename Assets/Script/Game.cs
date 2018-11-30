using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public bool isStart = false;
	public bool isRunning = false;
	public bool isEnd = false;
	public bool isRestart = false;
	public Broad broad;
	public float waterSpeed = 0.3f;
	static public Game instance;
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isRestart) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("1");
		}
		if (isStart && !isRunning) {
			isRunning = true;
			StartCoroutine(Starting());
		}
		if(isEnd){
			broad.isStart = false;
		}
	}
	IEnumerator Starting(){
		var b = broad;
		var brick = BrickManager.GetInstance().At(0,0);
		yield return new WaitForSeconds(5.0f);
		brick.speed = waterSpeed;
		brick.SetIncomingDir(Brick.Direction.top);
		yield return new WaitForSeconds(10.0f);
		b.isStart = true;
		
	}

}
