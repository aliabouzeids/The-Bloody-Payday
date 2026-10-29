using UnityEngine;
using UnityEngine.UI;
public class Continue : MonoBehaviour
{
    void Start()
    {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(Continue_Button);
    }
    void Continue_Button()
    {
        Time.timeScale = 1f;
        transform.parent.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}