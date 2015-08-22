using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public EnemyStrategy _strategy;

    // Use this for initialization
    void Start () {
        _strategy = new WalkingStrategy(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (_strategy != null)
        {
            _strategy.Perform();
        }
    }
}
