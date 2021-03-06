﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickInitGen : MonoBehaviour {
	public Transform parent;
	public GameObject target;
	public int countx, county;
	public Vector2 distance;
	// Use this for initialization
	void Start () {
		for (var j = county; j >= 0; j--) 
		{
			for (var i = 0; i < countx; i++) 
			{
				var o = Instantiate (target);
				o.transform.parent = parent;
				o.transform.position = transform.position;
				o.transform.localPosition += new Vector3 (i * distance.x, j * distance.y);
				var brick = o.GetComponent<Brick>();
				if(brick == null)continue;
				brick.x = i;
				brick.y = county-j;
				brick.dirs = BrickMapGen.GetInstance().At(brick.x, brick.y);
				if(brick.x == 0 && brick.y == 0){
					brick.SetIncomingDir(Brick.Direction.top);
					//brick.speed = 0.01f;
				}
			}
		}
	}
}
