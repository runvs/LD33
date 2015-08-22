using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float angle = 25;
    private Rigidbody2D _rigidBody;

    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	   if(Input.GetMouseButtonUp(0))
       {
            var force = JumpForce(GetClickPoint(), angle);
            
            _rigidBody.AddForce (force, ForceMode2D.Impulse);
            Debug.Log("Force " + force);
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
