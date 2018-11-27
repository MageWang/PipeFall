using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Brick : MonoBehaviour, IDragHandler, IEndDragHandler {
	public enum Direction
	{
		none,
		top,
		bot,
		left,
		right
	}
	static public BrickManager brickManager{
		get{
			return BrickManager.GetInstance();
		}
	}
	RectTransform rectTransform;
	public float progress = 0.0f;
	public float speed = 0.0f;
	public Direction[] dirs = new Direction[2];
	public Direction incomeDir = Direction.none;
	public Direction outcomeDir = Direction.none;
	public Brick[] neighbors = new Brick[2];
	public int _x = -1, _y = -1;
	public int x{
		get{
			return _x;
		}
		set{
			brickManager.Remove(this);
			_x = value;
			brickManager.Add(this);
		}
	}
	public int y{
		get{
			return _y;
		}
		set{
			brickManager.Remove(this);
			_y = value;
			brickManager.Add(this);
		}
	}
	
	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
	}

	void OnEnable(){
		if(brickManager != null) brickManager.Add(this);
	}
	void OnDisable(){
		if(brickManager != null) brickManager.Remove(this);
	}
	
	// Update is called once per frame
	void Update () {
		ResetNeighbors();
		ResetSprite();
		UpdateProgress();
	}

	void ResetNeighbors(){
		if(x < 0 || y < 0)return;

		for (var j = 0; j < dirs.Length; j++){
			var dir = dirs[j];
			if(dir == Direction.none)continue;
			if(dir == Direction.left){
				neighbors[j]=brickManager.At(x-1,y);
				continue;
			}
			if(dir == Direction.right){
				neighbors[j]=brickManager.At(x+1,y);
				continue;
			}
			if(dir == Direction.top){
				neighbors[j]=brickManager.At(x,y-1);
				continue;
			}
			if(dir == Direction.bot){
				neighbors[j]=brickManager.At(x,y+1);
				continue;
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
