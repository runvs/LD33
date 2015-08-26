using UnityEngine;
using UnityEngine.UI;

public class ChildController : MonoBehaviour
{

    public string NextLevel;

    // Use this for initialization
    void Start()
    {
        if (string.IsNullOrEmpty(NextLevel))
        {
            NextLevel = "Menu";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        var background = GameObject.FindGameObjectWithTag("ChildBackground");
        var material = background.GetComponent<Renderer>().material;

        material.SetColor("_Color", Color.red);

        var fader = GameObject.Find("FadeCanvas").transform.GetChild(0);
        if (fader != null)
        {
            fader.gameObject.SetActive(true);
            var image = fader.GetComponent<Image>();
            image.enabled = true;

            var fadeScript = fader.GetComponent<GameOverFade>();
            fadeScript.SetNextLevel(NextLevel);
            fadeScript.BeginFade = true;
        }

    }
}
