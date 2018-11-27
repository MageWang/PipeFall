using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenerater : MonoBehaviour {
	public Transform parent;
	public GameObject target;
	public GameObject gen;
	public int x = 0;
	public int y = 0;
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
			gen.transform.parent = parent;
			gen.transform.position = target.transform.position;
			var brick = gen.GetComponent<Brick>();
			if(brick != null){
				brick.x = x;
				brick.y = y;
				brick.dirs = BrickMapGen.GetInstance().At(brick.x, brick.y);
				y++;
			}
		}
		var targetSprite = target.GetComponent<SpriteRenderer> ();
		var genSprite = gen.GetComponent<SpriteRenderer> ();
		if (!genSprite.bounds.Intersects (targetSprite.bounds)) {
			gen = null;
		}
		
	}
}
