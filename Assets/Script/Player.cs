using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameObject debugObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!Game.instance.isRunning)return;
		if(Game.instance.isEnd){
			StopAllCoroutines();
			StartCoroutine(Up());
			return;
		}
		
		if (Input.GetMouseButtonDown(0)){
			Debug.Log("GetMouseButtonDown(0)");
			StartCoroutine(Down());
		}

		if(Input.GetMouseButtonUp(0)){
			Debug.Log("GetMouseButtonUp(0)");
			StopAllCoroutines();
			StartCoroutine(Up());
		}
	}
	Brick selectedBrick;
	Vector3 selectedOriginalPosition;
	Vector3 selectedOffset;
	IEnumerator Down(){
		while(true){
			var mousePosition = Input.mousePosition;
			var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
			debugObj.transform.position = worldPosition;
			if(selectedBrick == null){
				var rayHits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
				foreach (var rayHit in rayHits){
					if (rayHit.collider == null) continue;
					var brick = rayHit.collider.GetComponent<Brick>();
					if(brick == null)continue;
					if(!brick.inQueue)continue;
					selectedBrick = brick;
					selectedOriginalPosition = brick.transform.position;
					selectedOffset = worldPosition - selectedOriginalPosition;
					Debug.Log("selectedBrick:" + selectedBrick.name);
					break;
				}
			}
			else{
				// drag
				selectedBrick.transform.position = worldPosition-selectedOffset;
			}
			
			yield return null;
		}
		yield break;
	}

	IEnumerator Up(){
		if(selectedBrick!=null){
			bool isDestroy = false;
			// check position
			var brick = BrickManager.GetInstance().At(selectedBrick.transform.position);
			if(brick != null && brick.dirs[0] == Brick.Direction.none){
				Debug.Log("selectedBrick:"+brick.x+","+brick.y);
				brick.dirs = selectedBrick.dirs;
				var parent = selectedBrick.transform.parent;
				if(parent!=null){
					var queue = parent.GetComponent<BrickQueue>();
					queue.gen = null;
					isDestroy = true;
					Destroy(selectedBrick.gameObject);
				}
			}
			selectedBrick.transform.position = selectedOriginalPosition;
			selectedBrick = null;
		}
		yield break;
	}
}
