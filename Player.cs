using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int money;
    [SerializeField] TextMeshProUGUI money_text;


    void Update()
    {
        UI();
    }
    void UI()
    {
        if (money_text) money_text.text = money.ToString();
    }
}