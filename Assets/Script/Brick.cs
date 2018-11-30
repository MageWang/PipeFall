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
	public bool inQueue = false;
	public GameObject pipeObject;
	public Material waterMaterial;
	public SpriteRenderer waterSpriteRenderer;
	public SpriteRenderer pipeSpriteRenderer;
	public SpriteRenderer spriteRenderer;

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
	void Start(){
		rectTransform = GetComponent<RectTransform>();

		pipeObject = Instantiate(pipeObject);
		pipeObject.transform.parent = this.transform;
		pipeObject.transform.localPosition = new Vector3();
		
		waterSpriteRenderer = pipeObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
		waterMaterial = new Material(waterSpriteRenderer.material);
		
		waterSpriteRenderer.material = waterMaterial;
		pipeSpriteRenderer = pipeObject.GetComponent<SpriteRenderer>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = Config.instance.spriteBricks[0];
	}

	void OnEnable(){
		if(brickManager != null) brickManager.Add(this);
	}

	void OnDisable(){
		if(brickManager != null) brickManager.Remove(this);
		if(progress>0&&progress<1){
			Game.instance.isEnd = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ResetSprite();
		if(inQueue)return;
		ResetNeighbors();
		
		UpdateProgress();
	}

	Brick GetBrickFromDir(Direction direction){
		if(direction == Direction.left){
			return brickManager.At(x-1,y);
		}
		else if(direction == Direction.right){
			return brickManager.At(x+1,y);
		}
		else if(direction == Direction.top){
			return brickManager.At(x,y-1);
		}
		else if(direction == Direction.bot){
			return brickManager.At(x,y+1);
		}
		else{
			return null;
		}
	}

	void ResetNeighbors(){
		if(x < 0 || y < 0)return;

		for (var j = 0; j < dirs.Length; j++){
			var dir = dirs[j];
			var brick = GetBrickFromDir(dir);
			if(brick != null){
				neighbors[j] = brick;
			}
			else{
				neighbors[j] = null;
			}
		}
	}

	void ResetSprite(){
		waterMaterial.SetFloat("_Opacity", Mathf.Lerp(1.0f, 0.5f,progress));

		bool isLeft, isRight, isTop, isBot;
		isLeft = isRight = isTop = isBot = false;
		
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
			waterSpriteRenderer.sprite = Config.instance.spriteWaters[0];
			pipeSpriteRenderer.sprite = Config.instance.spritePipes[0];
			waterMaterial.SetTexture("_ColorTex", Config.instance.textWaters[0]);
			if(incomeDir==Direction.top){
				pipeObject.transform.localRotation = Quaternion.Euler(0,0,90);
			}
			else{
				pipeObject.transform.localRotation = Quaternion.Euler(180,0,180);
			}
		}
		else if(isTop && isRight){
			waterSpriteRenderer.sprite = Config.instance.spriteWaters[0];
			pipeSpriteRenderer.sprite = Config.instance.spritePipes[0];
			waterMaterial.SetTexture("_ColorTex", Config.instance.textWaters[0]);
			if(incomeDir==Direction.top){
				pipeObject.transform.localRotation = Quaternion.Euler(0,180,90);
			}
			else{
				pipeObject.transform.localRotation = Quaternion.Euler(180,180,180);
			}
		}
		else if(isBot && isRight){
			waterSpriteRenderer.sprite = Config.instance.spriteWaters[0];
			pipeSpriteRenderer.sprite = Config.instance.spritePipes[0];
			waterMaterial.SetTexture("_ColorTex", Config.instance.textWaters[0]);
			if(incomeDir==Direction.bot){
				pipeObject.transform.localRotation = Quaternion.Euler(0,0,-90);
			}
			else{
				pipeObject.transform.localRotation = Quaternion.Euler(0,180,-180);
			}
		}
		else if(isBot && isLeft){
			waterSpriteRenderer.sprite = Config.instance.spriteWaters[0];
			pipeSpriteRenderer.sprite = Config.instance.spritePipes[0];
			waterMaterial.SetTexture("_ColorTex", Config.instance.textWaters[0]);
			if(incomeDir==Direction.bot){
				pipeObject.transform.localRotation = Quaternion.Euler(0,180,-90);
			}
			else{
				pipeObject.transform.localRotation = Quaternion.Euler(0,0,-180);
			}
		}
		else if(isTop && isBot){
			waterSpriteRenderer.sprite = Config.instance.spriteWaters[1];
			pipeSpriteRenderer.sprite = Config.instance.spritePipes[1];
			waterMaterial.SetTexture("_ColorTex", Config.instance.textWaters[1]);
			if(incomeDir==Direction.top){
				pipeObject.transform.localRotation = Quaternion.Euler(0,0,-180);
			}
			else{
				pipeObject.transform.localRotation = Quaternion.Euler(180,0,-180);
			}
		}
		else if(isLeft && isRight){
			waterSpriteRenderer.sprite = Config.instance.spriteWaters[1];
			pipeSpriteRenderer.sprite = Config.instance.spritePipes[1];
			waterMaterial.SetTexture("_ColorTex", Config.instance.textWaters[1]);
			if(incomeDir==Direction.left){
				pipeObject.transform.localRotation = Quaternion.Euler(0,0,-90);
			}
			else{
				pipeObject.transform.localRotation = Quaternion.Euler(0,180,-90);
			}
		}
		else{
			pipeObject.SetActive(false);
		}
	}
	public Brick GetIncomingBrick(){
		for(var i = 0; i < dirs.Length; i++){
			if(incomeDir == dirs[i]){
				return neighbors[i];
			}
		}
		return null;
	}

	public Brick GetOutcomingBrick(){
		for(var i = 0; i < dirs.Length; i++){
			if(outcomeDir == dirs[i]){
				return neighbors[i];
			}
		}
		return null;
	}

	void UpdateProgress(){
		if(progress >= 1.0f)return;
		//var incomingBrick = GetIncomingBrick();
		// if(speed <= 0.0f && incomingBrick!=null && Mathf.Abs(incomingBrick.progress - 1.0f) < 0.001f){
		// 	speed = 0.5f;
		// }
		progress+=(Time.deltaTime*speed);
		if(progress>1.0f){
			if(!SetNeighborStart()){
				Game.instance.isEnd = true;
			}
			progress = 1.0f;
			speed = 0.0f;
		}
	}

	bool SetNeighborStart(){
		var brick = GetOutcomingBrick();
		if(brick == null)return false;
		if(!IsMatchNeighborDir(outcomeDir, brick))return false;
		brick.speed=speed;
		brick.SetIncomingDir(GetReverseDir(outcomeDir));
		return true;
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
			incomeDir = Direction.none;
			outcomeDir = Direction.none;
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

	public bool IsMatchNeighborDir(Direction dir, Brick b){
		var revDir = GetReverseDir(dir);
		for(var i = 0; i < b.dirs.Length; i++){
			if(b.dirs[i]==revDir)return true;
		}
		return false;
	}

	static public Direction GetReverseDir(Direction dir){
		switch(dir){
			case Direction.none:
				return Direction.none;
			case Direction.left:
				return Direction.right;
			case Direction.right:
				return Direction.left;
			case Direction.top:
				return Direction.bot;
			case Direction.bot:
				return Direction.top;
			default:
				return Direction.none;
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
	static public Direction[] RandomAvailableDirs(){
		var r = Random.Range(1,7);
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
