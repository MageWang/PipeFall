using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broad : MonoBehaviour {
	[System.Serializable]
	public class Bound{
		public Vector2 min;
		public Vector2 max;
	}
	public float speed = 1.0f;
	public Bound escapeRange;
	public bool isDebug = true;
	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update(){
		for (var i = 0; i < transform.childCount; i++) {
			var child = transform.GetChild (i);
			var pos = child.localPosition;
			pos.y += (Time.deltaTime * speed);
			child.localPosition = pos;
			if (IsOutOfBound (child)) {
				Destroy (child.gameObject);
			}
		}

		if (isDebug){
			Debug.DrawLine(escapeRange.max, new Vector3 (escapeRange.max.x, escapeRange.min.y));
			Debug.DrawLine(escapeRange.max, new Vector3 (escapeRange.min.x, escapeRange.max.y));
			Debug.DrawLine(escapeRange.min, new Vector3 (escapeRange.max.x, escapeRange.min.y));
			Debug.DrawLine(escapeRange.min, new Vector3 (escapeRange.min.x, escapeRange.max.y));
		}
	}

	bool IsOutOfBound(Transform transform){
		var spriteRenderer = transform.GetComponent<SpriteRenderer> ();
		if (spriteRenderer.bounds.min.x > escapeRange.max.x)
			return true;
		if (spriteRenderer.bounds.max.x < escapeRange.min.x)
			return true;
		if (spriteRenderer.bounds.min.y > escapeRange.max.y)
			return true;
		if (spriteRenderer.bounds.max.y < escapeRange.min.y)
			return true;
		
//		if (transform.localPosition.x > escapeRange.max.x)
//			return true;
//		if (transform.localPosition.x < escapeRange.min.x)
//			return true;
//		if (transform.localPosition.y > escapeRange.max.y)
//			return true;
//		if (transform.localPosition.y < escapeRange.min.y)
//			return true;		

		return false;
	}
}
