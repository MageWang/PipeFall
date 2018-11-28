using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickQueue : MonoBehaviour {
	public Transform parent;
	public GameObject target;
	public GameObject gen;
	// Use this for initialization
	void Start () {
		if (parent == null) {
			var o = FindObjectOfType<Broad> ();
			parent = o.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gen == null) {
			gen = Instantiate(target);
			//gen.transform.parent = parent;
			gen.transform.position = transform.position;
			var brick = gen.GetComponent<Brick>();
			if(brick != null){
				brick.inQueue = true;
				brick.dirs = Brick.RandomAvailableDirs();
			}
		}
		// var targetSprite = GetComponent<SpriteRenderer> ();
		// var genSprite = gen.GetComponent<SpriteRenderer> ();
		// if (!genSprite.bounds.Intersects (targetSprite.bounds)) {
		// 	gen = null;
		// }
	}
}
