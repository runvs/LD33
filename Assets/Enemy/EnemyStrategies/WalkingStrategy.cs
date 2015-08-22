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
        _WalkTimer = GameProperties.Enemy_WalkTimer;

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _WalkTimer -= Time.deltaTime;
        if (_WalkTimer <= 0)
        {
            _WalkTimer = GameProperties.Enemy_WalkTimer;
            _direction = !_direction;
        }
        float force = (_direction ? 1 : -1) * GameProperties.Enemy_AccelerationForce;
        _what.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 0));
	}

    public void Perform()
    {
        Update();
    }
}
