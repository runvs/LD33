using UnityEngine;
using System.Collections;

public class ClearDefaultSceneStuff : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        // obtain list of gameobjects
        GameObject[] lgo = GameObject.FindGameObjectsWithTag("MainCamera");
        Debug.Log(lgo.Length);

        // check if the camera from the menu is active:
        bool camFromMenu = false;
        foreach (var go in lgo)
        {
            if (go.name == "CameraFromMenu")
            {
                camFromMenu = true;
            }
        }


        if (camFromMenu)
        {
            foreach (var go in lgo)
            {
                if (go.name != "CameraFromMenu")
                {
                    Destroy(go);
                }
            }
        }

        lgo = GameObject.FindGameObjectsWithTag("MainCamera");
        Debug.Log(lgo.Length);
    }
}
