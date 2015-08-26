using UnityEngine;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour
{
    public bool BeginFade = false;



    private float timer = 0.0f;
    private string nextLevel;

    public void SetNextLevel(string str)
    {
        nextLevel = str;
    }

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
            timer += Time.deltaTime;
            if (timer >= 1.5f)
            {
                Application.LoadLevel(nextLevel);
            }
            
        }
    }
}
