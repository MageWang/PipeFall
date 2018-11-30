using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour {

	Dictionary<int,Dictionary<int,Brick>> bricks = new Dictionary<int, Dictionary<int, Brick>>();
	Dictionary<int, int> countX = new Dictionary<int, int>();
	Dictionary<int, int> countY = new Dictionary<int, int>();
	public int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
	public bool isDebug = false;
	[System.Serializable]
	public class DebugBrick{
		public int x;
		public int y;
		public Brick brick;
	}
	public List<DebugBrick> debugBricks = new List<DebugBrick>();
	public float minDistance = 0.05f;
	static private BrickManager instance;
	static public BrickManager GetInstance(){
		return instance;
	}
	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		if(instance!=null){
			Debug.LogWarning("instance!=null");
			return;
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDebug)return;
		debugBricks = new List<DebugBrick>();
		foreach(var p in bricks){
			foreach(var q in p.Value){
				debugBricks.Add(new DebugBrick(){x = p.Key, y = q.Key, brick=q.Value});
			}
		}
	}

	
	public void Add(Brick brick){
		if(!bricks.ContainsKey(brick.x)){
			bricks.Add(brick.x, new Dictionary<int,Brick>());
		}
		bricks[brick.x][brick.y] = brick;
		if(!countX.ContainsKey(brick.x)){
			countX[brick.x] = 0;
		}
		countX[brick.x]++;
		if(brick.x<minX)
		{
			minX = brick.x;
		}
		if(brick.x>maxX)
		{
			maxX = brick.x;
		}

		if(!countY.ContainsKey(brick.y)){
			countY[brick.y] = 0;
		}
		countY[brick.y]++;
		if(brick.y<minY)
		{
			minY = brick.y;
		}
		if(brick.y>maxY)
		{
			maxY = brick.y;
		}
	}

	public void Remove(Brick brick){
		if(!bricks.ContainsKey(brick.x)){
			return;
		}
		if(!bricks[brick.x].ContainsKey(brick.y)){
			return;
		}
		bricks[brick.x].Remove(brick.y);
		countX[brick.x]--;
		countY[brick.y]--;
	}

	public Brick At(int x, int y){
		if(!bricks.ContainsKey(x)){
			return null;
		}
		Brick brick = null;
		if(bricks[x].TryGetValue(y, out brick)){
			return brick;
		}
		return null;
	}

	public Brick At(Vector2 vector2){
		foreach(var p in bricks){
			if(p.Key < 0)continue;
			foreach(var q in p.Value){
				if(q.Key < 0)continue;
				if(Vector3.Distance(q.Value.transform.position, vector2) <  minDistance){
					return q.Value;
				}
			}
		}
		return null;
	}

	public int MinX(){
		return minX;
	}

	public int MaxX(){
		return maxX;
	}

	public int MinY(){
		return minY;
	}
	
	public int MaxY(){
		return maxY;
	}
}
