using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform Player;
    private float _deadZone = 0.25f;
    private float _speed = 1.5f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;;
    }
	
    void Update()
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
}