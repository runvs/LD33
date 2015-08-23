using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float Angle = 25;
	public Direction Direction = Direction.RIGHT;
	public float Force;

    private Rigidbody2D _rigidBody;
    private Vector3? _lastClickPoint;
    private float _forceMultiplier = 0.0f;
    private bool _canJump = true;
    private bool _clingsToWall = false;
    private bool _canWalk = true;

    public Material TrajectoryMaterial;
    private LineRenderer _trajectory;
    private float _initialGravityScale;
    private Animator _animator;

    void Start ()
	{
        _rigidBody = GetComponent<Rigidbody2D>();
        _initialGravityScale = _rigidBody.gravityScale;

        _trajectory = this.gameObject.AddComponent<LineRenderer>();
        _trajectory.SetWidth(0.06f, 0.0f);
        _trajectory.material = TrajectoryMaterial;
        
        _animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _canJump)
        {
            if (!_lastClickPoint.HasValue)
            {
                _lastClickPoint = GetClickPoint();
            }

            _forceMultiplier += Time.deltaTime * 2;
            _forceMultiplier = _forceMultiplier >= 1.2f ? 1.2f : _forceMultiplier;
            
            var jumpForce = JumpForce(_lastClickPoint.Value, Angle);
            UpdateTrajectory(transform.position, jumpForce * _forceMultiplier / _rigidBody.mass, Physics.gravity);

            TrajectoryMaterial.SetColor("_TintColor", Color.red);
            _canWalk = false;
            
            _animator.SetBool("ducking", true);
        }
        else
        {
            var color = TrajectoryMaterial.GetColor("_TintColor");
            color.a = color.a * 0.8f;

            TrajectoryMaterial.SetColor("_TintColor", color);
            _canWalk = true;
            
            _animator.SetBool("ducking", false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(_rigidBody.gravityScale == 0.0f)
            {
                _rigidBody.gravityScale = _initialGravityScale;
                _clingsToWall = false;
            }

            if(_lastClickPoint.HasValue && _canJump)
            {
                if(_forceMultiplier > 0.2f)
                {
                    var jumpForce = JumpForce(_lastClickPoint.Value, Angle);
                    _rigidBody.AddForce(jumpForce * _forceMultiplier, ForceMode2D.Impulse);
                }
    
                _lastClickPoint = null;
                _forceMultiplier = 0.0f;
    
                _canJump = false;
            }
        }
        
        if (Input.GetAxis ("Horizontal") > 0 && _canWalk)
		{
			MoveRight ();
            _animator.SetBool("walking", true);
        }
		else if (Input.GetAxis ("Horizontal") < 0 && _canWalk)
		{
			MoveLeft();
            _animator.SetBool("walking", true);
        }
        else
        {
            _animator.SetBool("walking", false);
        }

        if (_rigidBody.velocity.x != 0)
        {
            this.transform.localScale = new Vector3((_rigidBody.velocity.x > 0 ? 1 : -1), 1, 1);
        }

        _animator.SetBool("clingsToWall", _clingsToWall);
        _animator.SetBool("falling", !(_canWalk && _canJump));
        _animator.SetFloat("speed", Mathf.Abs(( 0.25f + _rigidBody.velocity.x ) / 2.0f));
	}
    
    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        var trajectoryPoints = new List<Vector3>();
        var maxSteps = 100;
        var timeDelta = 1.0f / initialVelocity.magnitude;
    
        var position = initialPosition;
        var velocity = initialVelocity;
        var i = 0;
        
        while(i < maxSteps && position.y >= initialPosition.y)
        {
            position += velocity * timeDelta * _rigidBody.drag + 0.5f * gravity * timeDelta * timeDelta / _rigidBody.mass;
            velocity += gravity * timeDelta / _rigidBody.drag / _rigidBody.mass;

            trajectoryPoints.Add(position);
        }

        _trajectory.SetVertexCount(trajectoryPoints.Count);
        i = 0;
        
        foreach(var point in trajectoryPoints)
        {
            _trajectory.SetPosition(i, point);
            i++;
        }
    }
    
    void OnCollisionEnter2D(Collision2D hit)
    {
        if(hit.gameObject.tag == "Floor" || hit.gameObject.tag == "Enemy")
        {
            _canJump = true;
        }
        else if(hit.gameObject.tag == "Wall" && Input.GetMouseButton(0))
        {
            _rigidBody.gravityScale = 0.0f;
            _rigidBody.velocity = new Vector2(0.0f, 0.0f);

            _clingsToWall = true;
        }
    }
	
	public void MoveRight()
	{
        if(!_clingsToWall)
        {
            var force = _canJump ? Force : 0.25f * Force;
            
            _rigidBody.AddForce (new Vector2 (force, 0));
            this.Direction = Direction.RIGHT;
        }
	}
	
	
	public void MoveLeft()
	{
        if (!_clingsToWall)
        {
            var force = _canJump ? -Force : -0.25f * Force;

            _rigidBody.AddForce(new Vector2(force, 0));
            this.Direction = Direction.LEFT;
        }
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