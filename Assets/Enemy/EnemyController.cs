using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public EnemyStrategy _strategy;

    public eEnemyStragegy strategy;

    private Rigidbody2D _rigidBody;


    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        if (strategy == eEnemyStragegy.Sitting)
        {
            _strategy = new SittingStrategy();
        }
        else if (strategy == eEnemyStragegy.Walking)
        {
            _strategy = new WalkingStrategy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_strategy != null)
        {
            _strategy.Perform();
        }

        if (_rigidBody.velocity.x != 0)
        {
            transform.localScale = new Vector3((_rigidBody.velocity.x > 0 ? -1 : 1), 1, 1);
        }
    }
}


public enum eEnemyStragegy
{
    Sitting, Walking
}