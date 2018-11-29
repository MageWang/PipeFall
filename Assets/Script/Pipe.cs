using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
	public GameObject water;
	SpriteRenderer spriteRenderer;
	Material material;
	public float fill = 1.0f;
	public float max = 0.5f;
	public float min = 1.0f;
	// Use this for initialization
	void Start () {
		spriteRenderer = water.GetComponent<SpriteRenderer>();	
		material = new Material(spriteRenderer.material);
		spriteRenderer.material = material;
		//material.SetFloat("_Opacity", 1);
		//material.SetVector("_Coord", new Vector4(-1,-1,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		material.SetFloat("_Opacity", min+(max-min)*fill);
	}
}
