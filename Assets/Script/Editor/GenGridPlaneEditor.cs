using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenGridPlane))]
public class GenGridPlaneEditor : Editor {
	public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
		var instance = this.target as GenGridPlane;
        if (GUILayout.Button("gen"))
        {
			for(var i = 0; i < instance.row; i++){
				for(var j = 0; j < instance.col; j++){
					var g = Instantiate(instance.refGrid);
					var rectTransform = g.GetComponent<RectTransform>();
					var p = new Vector2( instance.width*j, instance.height*i);
					rectTransform.parent = instance.plane.GetComponent<RectTransform>();
					rectTransform.localPosition = p;
					rectTransform.localScale = new Vector3(1,1,1);
					
				}
			}
        }
    }
}
