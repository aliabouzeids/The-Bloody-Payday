using UnityEngine;
using UnityEngine.UI;



public class Buttons_Sounds : MonoBehaviour
{
    AudioSource button_sound;
    Button[] buttons;

    [System.Obsolete]
    void Start()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        buttons = FindObjectsOfType<Button>(true);
        button_sound = GetComponent<AudioSource>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(button_sound.Play);
        }
    }
}