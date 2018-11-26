using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGenGen : MonoBehaviour {
	public Transform parent;
	public GameObject target;
	public int countx, county;
	public Vector2 distance;
	// Use this for initialization
	void Start () {
		for (var i = 0; i < countx; i++) {
			for (var j = 0; j < county; j++) {
				var o = Instantiate (target);
				o.transform.parent = parent;
				o.transform.position = target.transform.position;
				o.transform.localPosition += new Vector3 (i * distance.x, j * distance.y);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
