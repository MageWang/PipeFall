using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMgr : MonoBehaviour {
	public List<PipeGrid> grids = new List<PipeGrid>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public PipeGrid GetNearestGrid(Vector3 v){
		var dist = float.MaxValue;
		PipeGrid nearest = null;
		foreach (var g in grids){
			var d = Vector3.Distance(v,g.transform.position);
			if(d<dist){
				dist = d;
				nearest = g;
			}
		}
		return nearest;
	}
}
