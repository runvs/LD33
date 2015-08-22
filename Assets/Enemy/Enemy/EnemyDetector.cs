using UnityEngine;
using System.Collections;

public class EnemyDetector : MonoBehaviour {
    

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("enter");
        if (coll.gameObject.tag == "Player" )
        {
            this.transform.parent.FindChild("VisorLight").GetComponent<Light>().color = new Color(1.0f, 0.1f, 0.1f, 1);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("leave");
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Set player");
            this.transform.parent.FindChild("VisorLight").GetComponent<Light>().color = new Color(0.1f, 1.0f, 0.1f, 1);

        }
    }

}
