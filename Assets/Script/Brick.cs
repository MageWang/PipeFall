using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BrickManager{
	Dictionary<int,Dictionary<int,Brick>> bricks = new Dictionary<int, Dictionary<int, Brick>>();
	Dictionary<int, int> countX = new Dictionary<int, int>();
	Dictionary<int, int> countY = new Dictionary<int, int>();
	public int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
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
public class Brick : MonoBehaviour, IDragHandler, IEndDragHandler {
	public enum Direction
	{
		none,
		top,
		bot,
		left,
		right
	}
	static public BrickManager brickManager = new BrickManager();
	RectTransform rectTransform;
	public float progress = 0.0f;
	public float speed = 0.0f;
	public Direction[] dirs = new Direction[2];
	public Direction incomeDir = Direction.none;
	public Direction outcomeDir = Direction.none;
	public Brick[] neighbors = new Brick[2];
	public int x = -1, y = -1;
	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
	}

	void OnEnable(){
		brickManager.Add(this);
	}
	void OnDisable(){
		brickManager.Remove(this);
	}
	
	// Update is called once per frame
	void Update () {
		ResetNeighbors();
		ResetSprite();
		UpdateProgress();
	}

	void ResetNeighbors(){
		if(x < 0 || y < 0)return;
		for(var i = 0; i < brickManager.Count; i++){
			var brick = brickManager[i];
			for (var j = 0; j < dirs.Length; j++){
				var dir = dirs[j];
				if(dir == Direction.none)continue;
				if(dir == Direction.left){
					if(brick.x == x-1 && brick.y == y && x-1 > 0){
						neighbors[j]=brick;
					}
					continue;
				}
				if(dir == Direction.right){
					if(brick.x == x+1 && brick.y == y){
						neighbors[j]=brick;
					}
					continue;
				}
				if(dir == Direction.top){
					if(brick.x == x && brick.y == y-1 && y-1 > 0){
						neighbors[j]=brick;
					}
					continue;
				}
				if(dir == Direction.bot){
					if(brick.x == x && brick.y == y+1){
						neighbors[j]=brick;
					}
					continue;
				}
			}
		}
	}

	void ResetSprite(){
		bool isLeft, isRight, isTop, isBot;
		isLeft = isRight = isTop = isBot = false;
		Sprite texture;
		foreach(var dir in dirs){
			if(dir == Direction.top){
				isTop = true;
			}
			else if(dir == Direction.bot){
				isBot = true;
			}
			else if(dir == Direction.left){
				isLeft = true;
			}
			else if(dir == Direction.right){
				isRight = true;
			}
		}
		if(isTop && isLeft){
			texture = Config.instance.texturePipes[1];
		}
		else if(isTop && isRight){
			texture = Config.instance.texturePipes[2];
		}
		else if(isBot && isRight){
			texture = Config.instance.texturePipes[3];
		}
		else if(isBot && isLeft){
			texture = Config.instance.texturePipes[4];
		}
		else if(isTop && isBot){
			texture = Config.instance.texturePipes[5];
		}
		else if(isLeft && isRight){
			texture = Config.instance.texturePipes[6];
		}
		else{
			texture = Config.instance.texturePipes[0];
		}
		var sprietRender = GetComponent<SpriteRenderer>();
		sprietRender.sprite = texture;
	}

	void UpdateProgress(){
		if(progress >= 1.0f)return;
		progress+=(Time.deltaTime*speed);
		if(progress>1.0f){
			progress = 1.0f;
		}
	}
	public void OnDrag(PointerEventData pointer){
		var p = Main.instance.uiCamera.ScreenToWorldPoint(Input.mousePosition);
		p.z = rectTransform.position.z;
		rectTransform.position = p;
		Debug.Log("p:"+p);
	}

	public void OnEndDrag(PointerEventData pointer){
		// move to nearest grid
		var p = Main.instance.uiCamera.ScreenToWorldPoint(Input.mousePosition);
		p.z = rectTransform.position.z;
		var nearest = Main.instance.gridMgr.GetNearestGrid(p).transform.position;
		nearest.z = rectTransform.position.z;
		rectTransform.position = nearest;
	}

	public void SetIncomingDir(Direction direction){
		if(direction!=dirs[0] && direction!=dirs[1]){
			Debug.LogWarning("set wrong direction: " + direction);
			return;
		}
		incomeDir = direction;
		if(direction==dirs[0]){
			outcomeDir = dirs[1];
		}
		else{
			outcomeDir = dirs[0];
		}
	}

	static public Direction[] RandomDirs(){
		var r = Random.Range(0,7);
		if(r == 0){
			return new Direction[] {Direction.none, Direction.none};
		}
		else if(r == 1){
			return new Direction[] {Direction.top, Direction.left};
		}
		else if(r == 2){
			return new Direction[] {Direction.top, Direction.right};
		}
		else if(r == 3){
			return new Direction[] {Direction.bot, Direction.right};
		}
		else if(r == 4){
			return new Direction[] {Direction.bot, Direction.left};
		}
		else if(r == 5){
			return new Direction[] {Direction.top, Direction.bot};
		}
		else if(r == 6){
			return new Direction[] {Direction.left, Direction.right};
		}
		return new Direction[] {Direction.none, Direction.none};
	}
}
