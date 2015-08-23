using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingScript : MonoBehaviour
{
    List<GameObject> _enemies;
    public float _inputTimer = 0.0f;

    public AudioClip AttackSound;
    public AudioClip HitSound;

    void Start()
    {
        _enemies = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_inputTimer <= 0.0f)
            {
                bool hasHit = false;
                _inputTimer += 0.5f;
               
                foreach (var enemy in _enemies)
                {
                    if (enemy == null)
                        continue;

                    Debug.Log("Hit enemy!");
                    hasHit = true;

                    // Attack Enemy
                    Destroy(enemy);
                }

                if (hasHit)
                {
                   // GetComponent<AudioSource>().PlayOneShot(HitSound);
                }
                else
                {
                   // GetComponent<AudioSource>().PlayOneShot(AttackSound);
                }
            }
        }

        if (_inputTimer > 0)
        {
            _inputTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Adding enemy");
            _enemies.Add(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Removing enemy");
            _enemies.Remove(coll.gameObject);
        }
    }
}
