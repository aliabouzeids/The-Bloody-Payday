using UnityEngine;
using TMPro;
public class Money : MonoBehaviour
{
    public int money;
    [SerializeField] TextMeshProUGUI money_text;
    HP hp;
    void Awake()
    {
        hp = GetComponent<HP>();
    }
    void Update()
    {
        if (money_text) money_text.text = money.ToString();
        if (hp) hp.money_on_death = money;
    }
}