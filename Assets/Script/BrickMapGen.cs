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
		if(directions.Count < y+1){
			// gen new
			for(var i = directions.Count; i < y+1; i++){
				directions.Add(GenRow(i));
			}
		}
		if(x > width -1){
			Debug.LogWarning("x > width -1");
			return new Brick.Direction[2]{
				Brick.Direction.none, Brick.Direction.none
			};
		}
		return directions[y][x];
	}
	List<Brick.Direction[]> GenRow(int y){
		var res = new List<Brick.Direction[]>();
		if(y == 0){
			for(var j = 0; j < width; j++){
				if(j == 0){
					res.Add(new Brick.Direction[2]{
						Brick.Direction.top, Brick.Direction.bot
					});
					continue;
				}
				res.Add(new Brick.Direction[2]{
					Brick.Direction.none, Brick.Direction.none
				});
			}
		}
		else{
			List<int> arr = new List<int>();
			for(var i = 0; i < width; i++){
				if(At(i,y-1)[0] != Brick.Direction.none)continue;
				arr.Add(i);
			}
			// choose 1 - 2 from arr
			arr.Sort((a,b)=>{
				return Random.Range(-1,2);
			});
			for(var i = 0; i < width; i++){
				if(i == arr[0]){
					res.Add(Brick.RandomDirs());
				}
				else{
					res.Add(new Brick.Direction[2]{
						Brick.Direction.none, Brick.Direction.none
					});
				}
			}
		}
		return res;
	}
	

}
