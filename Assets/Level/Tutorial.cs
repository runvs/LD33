using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

    private UnityEngine.UI.Text _text;

    private float _timer = -3;


    // Use this for initialization
    void Start () {
        _text = gameObject.GetComponent<UnityEngine.UI.Text>();
        _text.color = new Color(0.5f, 0.5f, 0.5f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        float newTimer = _timer + Time.deltaTime;
        
        if (_timer> -1 && _timer <= 0)
        {
            _text.color = new Color(125, 125, 125, (float)PennerDoubleEquation.GetValue(PennerDoubleEquation.EquationType.Linear, _timer + 1.0f, 0, 1, 1));
        }

        if(_timer < 3 && newTimer > 3)
        {
            _text.text = "Hold LMB to jump";
        }

        if (_timer < 7 && newTimer > 7)
        {
            _text.text = "When jumping against a wall, hold LMB (in flight) to cling to the wall";
        }

        if(_timer < 15 && newTimer > 15)
        {
            _text.text = "Watch out for Parents. If they spot you, they will kill you";
        }

        if (_timer < 18 && newTimer > 18)
        {
            _text.text = "If you are quickly enough, press RMB to attack the parent.";
        }

        if (_timer > 25)
        {
            _text.color = new Color(125, 125, 125, 1.0f - (float)PennerDoubleEquation.GetValue(PennerDoubleEquation.EquationType.Linear, _timer -25, 0, 1, 1));
        }

        _timer = newTimer;
        
    }
}
