using UnityEngine;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour
{
    public bool BeginFade = false;

    private Image _image;

    // Use this for initialization
    void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _image.enabled = BeginFade;

        if (BeginFade)
        {
            _image.color = Color.Lerp(_image.color, Color.red, Time.deltaTime);
        }
    }
}
