using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

    private UnityEngine.UI.Text _text;


    public bool fadeIn = false;
    public bool fadeOut = false;
    public float _fadeInTimer = 0;
    public float _fadeOutTimer = 0;

    public float fadeTime;
    public bool startAutomatic = false;


    // Use this for initialization
    void Start () {
        _text = gameObject.GetComponent<UnityEngine.UI.Text>();
        _text.color = new Color(0.5f, 0.5f, 0.5f, 0);
        if (startAutomatic)
        {
            fadeIn = true;
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _fadeInTimer = 0;
            fadeIn = true;
            fadeOut = false;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _fadeOutTimer = 0;
            fadeOut = true;
            fadeIn = false;
        }
    }


    // Update is called once per frame
    void Update ()
    {
        if(fadeIn && _fadeInTimer < fadeTime)
        {
            _fadeInTimer += Time.deltaTime;
            _text.color = new Color(125, 125, 125, 
                (float)PennerDoubleEquation.Linear( _fadeInTimer, 0, 1, fadeTime));
        }
        else if (fadeOut && _fadeOutTimer < fadeTime)
        {
            _fadeOutTimer += Time.deltaTime;
            _text.color = new Color(125, 125, 125,
                1.0f - (float)PennerDoubleEquation.Linear(_fadeOutTimer, 0, 1, fadeTime));
        }
    }
}
