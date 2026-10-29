using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class Invitation : MonoBehaviour
{
    public string i_name;
    public string description;
    [SerializeField] TextMeshProUGUI name_text;
    [SerializeField] TextMeshProUGUI description_text;
    public List<int> senders;
    public int current_sender_index;

    Gangsters_Manager g_mng;
    void Awake()
    {
        g_mng = GameObject.Find("game_manager").GetComponent<Gangsters_Manager>();
    }
    void Update()
    {
        See_Sender_Invitation();
        if (i_name.Length > 0 && name_text) name_text.text = i_name;
        if (description.Length > 0 && description_text) description_text.text = description;
    }
    void See_Sender_Invitation()
    {
        if (senders.Count <= 0) return;
        Gangster g = g_mng.gangsters[senders[current_sender_index]];
        Description d = g.GetComponent<Description>();
        if (d)
        {
            i_name = d.i_name;
            description = d.description;
        }
    }
    public void Next()
    {
        current_sender_index = current_sender_index == senders.Count - 1 ? 0 : current_sender_index++;
        Debug.Log("Invitation::Next()");
    }
    public void Previous()
    {
        int last_index = senders.Count > 1 ? senders[senders.Count - 1] : 0;
        current_sender_index = current_sender_index == 0 ? last_index : current_sender_index--;
        Debug.Log("Invitation::Previous()");
    }

}