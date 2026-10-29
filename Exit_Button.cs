using UnityEngine;
using UnityEngine.UI;

public class Exit_Button : MonoBehaviour
{
    public void Exit() => Application.Quit();
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(Exit);
    }
}