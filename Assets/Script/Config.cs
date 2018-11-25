using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigImpelment{
	static ConfigImpelment instance;
	static public ConfigImpelment GetInstance(){
		if (instance == null) {
			instance = new ConfigImpelment ();
		}
		return instance;
	}
	private ConfigImpelment(){
	}
}

public class Config : MonoBehaviour {
	public ConfigImpelment implement = null;
	static Config instance;
	void Awake(){
		if (instance != null){
			Debug.LogWarning("instance!=null");
			return;
		}
		instance = this;
		implement = ConfigImpelment.GetInstance ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
