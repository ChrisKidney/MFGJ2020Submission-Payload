using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider volumeSlider;

    private const string VolumeKey = "GlobalVolume";


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Load saved volume
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.25f);

        // Set slider value without triggering events
        volumeSlider.SetValueWithoutNotify(savedVolume);

        // Apply volume
        SetGlobalVolume(savedVolume);

        // Add listener for runtime changes
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
    }

    public void SetGlobalVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        masterMixer.SetFloat("Volume", dB);
        PlayerPrefs.SetFloat(VolumeKey, volume);
        Debug.Log($"Setting volume {volume} → {dB} dB");
    }
}
