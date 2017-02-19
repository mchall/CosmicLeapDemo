using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// HTExplosion editor.
/// </summary>
[CustomEditor(typeof(HTExplosion))]
public class HTExplosionEditor : Editor {

	// Use this for initialization
	public override void OnInspectorGUI(){
		
		GUIStyle style;
		HTExplosion t;
		
		t = (HTExplosion)target;
		style = new GUIStyle();
		style.fontStyle =FontStyle.Bold;
			
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		// Turret properties
		GUILayout.Label("Sprite sheet properties",style);	
		EditorGUILayout.Space();
		t.spriteSheetMaterial = (Material)EditorGUILayout.ObjectField("Sprite sheet material", t.spriteSheetMaterial,typeof(Material),true); 
		t.uvAnimationTileX = EditorGUILayout.IntField("Tile X",t.uvAnimationTileX);
		t.uvAnimationTileY = EditorGUILayout.IntField("Tile Y",t.uvAnimationTileY);
		t.spriteCount = EditorGUILayout.IntField("Number of sprite",t.spriteCount);
		t.framesPerSecond = EditorGUILayout.IntField("Frames per second",t.framesPerSecond);
		t.isOneShot = EditorGUILayout.Toggle( "One shot",t.isOneShot);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		
		GUILayout.Label("Sprite properties",style);
		EditorGUILayout.Space();
		t.size = EditorGUILayout.Vector3Field("Size",t.size);
		t.speedGrowing = EditorGUILayout.FloatField("Speed growing",t.speedGrowing);
		t.randomRotation = EditorGUILayout.Toggle( "Random rotation",t.randomRotation);
		
		
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		// Refresh
		if (GUI.changed){
			EditorUtility.SetDirty (target);
		}
		 		 	
		EditorGUILayout.Space();
		EditorGUILayout.Space();
	}
}
