using UnityEngine;
using System.Collections;

public class ClearDefaultSceneStuff : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject[] lgo = GameObject.FindGameObjectsWithTag("DefaultCamera");
        foreach (var go in lgo)
        {
            if (go.name != "CameraFromMenu")
            {
                Destroy(go);
            }
        }
	}
}
