using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(Main.instance == null)return;
		if(Main.instance.gridMgr == null)return;
		if(Main.instance.gridMgr.grids.Contains(this))return;
		Main.instance.gridMgr.grids.Add(this);
	}
	void OnEnable(){
		if(Main.instance == null)return;
		if(Main.instance.gridMgr == null)return;
		if(Main.instance.gridMgr.grids.Contains(this))return;
		Main.instance.gridMgr.grids.Add(this);
	}
	void OnDisable(){
		Main.instance.gridMgr.grids.Remove(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
