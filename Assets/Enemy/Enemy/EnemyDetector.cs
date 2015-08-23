using UnityEngine;
using System.Collections;
using System;

public class EnemyDetector : MonoBehaviour {


    public EnemyStrategy _strategy;

    public float _detectionTimer = GameProperties.Enemy_DetectionTimeMax;
    private bool _playerInDetection = false;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_playerInDetection)
        {
            _detectionTimer -= Time.deltaTime;
            if (_detectionTimer <= 0)
            {
                EndGame();
            }
        }
        else
        {
            _detectionTimer += Time.deltaTime * 0.125f;
            if (_detectionTimer >= GameProperties.Enemy_DetectionTimeMax)
            {
                _detectionTimer = GameProperties.Enemy_DetectionTimeMax;
            }
        }
    }

    private void EndGame()
    {
        // TODO Fade
        Application.LoadLevel("Menu");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" )
        {
            this.transform.parent.FindChild("VisorLight").GetComponent<Light>().color = new Color(1.0f, 0.1f, 0.1f, 1);
            _playerInDetection = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("leave");
        if (coll.gameObject.tag == "Player")
        {
            this.transform.parent.FindChild("VisorLight").GetComponent<Light>().color = new Color(0.1f, 1.0f, 0.1f, 1);
            _playerInDetection = false;
        }
    }

}
