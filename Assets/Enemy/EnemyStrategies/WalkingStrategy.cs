using UnityEngine;
using System.Collections;

public class WalkingStrategy : MonoBehaviour, EnemyStrategy
{
    private MonoBehaviour _what;

    public WalkingStrategy ( MonoBehaviour what)
    {
        _what = what;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _what.GetComponent<Rigidbody2D>().AddForce(new Vector2(80, 0));
	}

    public void Perform()
    {
        Update();
    }
}
