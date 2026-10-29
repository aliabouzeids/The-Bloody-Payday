using UnityEngine;
using TMPro;
public class Name_Setter : MonoBehaviour
{
    [SerializeField] Description d;
    TextMeshProUGUI my_name;

    void Awake()
    {
        my_name = GetComponent<TextMeshProUGUI>();
        d = transform.parent.transform.parent.GetComponent<Description>();
    }
    void Update()
    {
        if (my_name) my_name.text = d.i_name;
    }
}