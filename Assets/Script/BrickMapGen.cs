using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMapGen : MonoBehaviour {
	static private BrickMapGen instance;
	static public BrickMapGen GetInstance(){
		return instance;
	}
	public List<List<Brick.Direction[]>> directions = new List<List<Brick.Direction[]>>();
	// Use this for initialization
	public int width = 6;
	void Awake(){
		if(instance!=null){
			Debug.LogWarning("instance!=null");
			return;
		}
		instance = this;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Brick.Direction[] At(int x, int y){
		if(y > 2000){
			
		}
		if(directions.Count < y-1){
			// gen new
		}
		if(x > width -1){
			Debug.LogWarning("x > width -1");
			return new Brick.Direction[2]{Brick.Direction.none, Brick.Direction.none};
		}
		return directions[y][x];
	}
}
