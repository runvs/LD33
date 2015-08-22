using UnityEngine;
using System.Collections;

public class WalkingStrategy : MonoBehaviour, EnemyStrategy
{
    private MonoBehaviour _what;
    private float _WalkTimer;
    private bool _direction;

    public WalkingStrategy ( MonoBehaviour what)
    {
        _what = what;
        _WalkTimer = 2;

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _WalkTimer -= Time.deltaTime;
        if (_WalkTimer <= 0)
        {
            _WalkTimer = 2;
            _direction = !_direction;
        }
        float force = (_direction ? 1 : -1) * 80;
        _what.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0));
	}

    public void Perform()
    {
        Update();
    }
}
