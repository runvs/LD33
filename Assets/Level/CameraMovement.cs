using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform Player = null;
    private float _deadZone = 0.25f;
    public float _speed = 1.5f;

    public float _timer = 0;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        
        if(player != null)
        {    
            Player = player.transform;
        }
    }
	
    void Update()
    {

        _timer += Time.deltaTime;

        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(_timer * 1.0f) * 0.75f);

        if (Player != null)
        {
            var playerPosition = Player.position;
            var cameraPosition = transform.position;
            cameraPosition.z = playerPosition.z;
    
            var v3 = playerPosition;
            v3.z = transform.position.z;
    
            if (Vector3.Distance(cameraPosition, playerPosition) > _deadZone)
            {
                transform.position = Vector3.Lerp(transform.position, v3, _speed * Time.deltaTime);
            }
        }
        else
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            
            if(player != null)
            {    
                Player = player.transform;
            }
        }
    }
}