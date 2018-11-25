using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Brick : MonoBehaviour, IDragHandler, IEndDragHandler {
	RectTransform rectTransform;
	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
