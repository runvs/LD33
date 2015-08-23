using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public EnemyStrategy _strategy;

    public eEnemyStragegy strategy;


    // Use this for initialization
    void Start () {
        if(strategy == eEnemyStragegy.Sitting)
        {
            _strategy = new SittingStrategy();
        }
        else if (strategy == eEnemyStragegy.Walking)
        {
            _strategy = new WalkingStrategy(this);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (_strategy != null)
        {
            _strategy.Perform();
        }
    }
}


public enum eEnemyStragegy
{
    Sitting, Walking
}