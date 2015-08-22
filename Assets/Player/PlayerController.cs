using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float angle = 25;
	public Direction Direction = Direction.RIGHT;
	public float Force;

    private Rigidbody2D _rigidBody;
    private Vector3? _lastClickPoint;
    private float _forceMultiplier = 0.0f;

    public Material TrajectoryMaterial;
    private LineRenderer _trajectory;
    private List<Vector3> _trajectoryPoints = new List<Vector3>();

    void Start ()
	{
        _rigidBody = GetComponent<Rigidbody2D>();

        _trajectory = this.gameObject.AddComponent<LineRenderer>();
        _trajectory.SetWidth(0.01f, 0.01f);
        _trajectory.SetVertexCount(5);
        _trajectory.material = TrajectoryMaterial; 
    }
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0))
        {
            if(!_lastClickPoint.HasValue)
            {
                _lastClickPoint = GetClickPoint();

                var mass = GetComponent<Rigidbody2D>().mass;

                _trajectory.SetPosition(0, transform.position);
                _trajectory.SetPosition(1, _lastClickPoint.Value);
                _trajectory.SetPosition(2, _lastClickPoint.Value * mass);
                _trajectory.SetPosition(3, _lastClickPoint.Value * mass * mass);
            }

            _forceMultiplier += Time.deltaTime * 2;
            _forceMultiplier = _forceMultiplier >= 1.2f ? 1.2f : _forceMultiplier;
        }

		if(Input.GetMouseButtonUp(0) && _lastClickPoint.HasValue)
		{
            var jumpForce = JumpForce(_lastClickPoint.Value, angle);
            _rigidBody.AddForce (jumpForce * _forceMultiplier, ForceMode2D.Impulse);
            
            _lastClickPoint = null;
            _forceMultiplier = 0.0f;
        }

		if (Input.GetAxis ("Horizontal") > 0)
		{
			MoveRight ();
		}
		else if (Input.GetAxis ("Horizontal") < 0)
		{
			MoveLeft();
		}
	}
	
	public void MoveRight()
	{
		_rigidBody.AddForce (new Vector2 (Force, 0));
		this.Direction = Direction.RIGHT;
	}
	
	
	public void MoveLeft()
	{
		_rigidBody.AddForce (new Vector2 (-Force, 0));
		this.Direction = Direction.LEFT;
	}
    
    Vector3 GetClickPoint()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var point = ray.origin + ray.direction;
        return point;
    }
    
    Vector3 JumpForce(Vector3 destination, float angle)
    {
       // get target direction
       var dir = destination - transform.position;
       
       // get height difference
       var h = dir.y;
       
       // retain only the horizontal direction
       dir.y = 0;
       
       // get horizontal distance
       var dist = dir.magnitude;  
       
       // convert angle to radians
       var a = angle * Mathf.Deg2Rad;
       
       // set dir to the elevation angle
       dir.y = dist * Mathf.Tan(a);
       
       // correct for small height differences
       dist += h / Mathf.Tan(a);
       
       // calculate the velocity magnitude
       var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));

        var mass = _rigidBody.mass;
        return vel * dir.normalized * mass;
    }
}

public enum Direction
{
	LEFT, RIGHT
}