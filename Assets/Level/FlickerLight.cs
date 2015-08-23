using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public FlickerType FlickerType;
    public float Frequency = 5.0f;
    public float MinIntensity = 1f;
    public float MaxIntensity = 2f;

    private Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
    }
    
    void Update()
    {
        if(FlickerType == FlickerType.FLICKER)
        {
            UpdateFlicker();
        }
        else if(FlickerType == FlickerType.PULSE)
        {
            UpdatePulse();
        }
    }

    private void UpdateFlicker()
    {
        var random = Random.Range(0.0f, 1.0f);

        if(random <= Frequency / 100)
        {
            _light.intensity = MinIntensity;
        }
        else
        {
            _light.intensity = MaxIntensity;
        }
    }
    
    private void UpdatePulse()
    {
        _light.intensity = (Mathf.Sin(Time.timeSinceLevelLoad * Frequency) + 1) / 2 * (MaxIntensity - MinIntensity) + MinIntensity;
    }
}

public enum FlickerType
{
    FLICKER,
    PULSE
}