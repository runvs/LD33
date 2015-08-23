using UnityEngine;
using System.Collections;

public class ClearDefaultSceneStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (GameObject.FindGameObjectWithTag("DefaultCamera") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
