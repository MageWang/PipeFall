using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Brick : MonoBehaviour, IDragHandler, IEndDragHandler {
	public enum BrickDir{
		up,
		down,
		left,
		right,
		none
	}
	static public List<Brick> bricks = new List<Brick>();
	RectTransform rectTransform;
	public float progress = 0.0f;
	public float speed = 0.0f;
	public BrickDir[] brickID = new BrickDir[2];
	public BrickDir incomeDir = BrickDir.none;
	public BrickDir outcomeDir = BrickDir.none;
	public Brick[] neighbors = new Brick[2];
	public int x = -1, y = -1;
	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
	}

	void OnEnable(){
		bricks.Add(this);
	}
	void OnDisable(){
		bricks.Remove(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ResetNeighbors(){
		for(var i = 0; i < bricks.Count; i++){
			
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
}
