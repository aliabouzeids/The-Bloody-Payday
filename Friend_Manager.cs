using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Friend_Manager : MonoBehaviour
{
    public int friends_number;
    public int max_friends_number = 5;
    [SerializeField] Invitation invitation_template;
    Gangsters_Manager g_mng;
    [SerializeField] TextMeshProUGUI friends_number_text;
    [SerializeField] GameObject friend_obj;
    [SerializeField] GameObject no_spots_available;
    [SerializeField] Transform canvas_parent;
    [SerializeField] Transform friends_list;
    void UI()
    {
        if (friends_number_text) friends_number_text.text = friends_number.ToString();
    }
    void Awake()
    {
        g_mng = GameObject.Find("game_manager").GetComponent<Gangsters_Manager>();
    }

    public void Set_Invite(int id)
    {
        Debug.Log("Friend_Manager::Set_Invite()");
        invitation_template.gameObject.SetActive(true);
        invitation_template.senders.Add(id);
        invitation_template.current_sender_index = invitation_template.senders.Count - 1;
    }
    public void Accept_Friendship()
    {
        if (friends_number >= max_friends_number)
        {
            Instantiate(no_spots_available, canvas_parent);
            return;
        }
        int id = invitation_template.senders[invitation_template.current_sender_index];
        g_mng.gangsters[id].accepted_friendship = true;
        invitation_template.senders.Remove(id);
        friends_number++;
        invitation_template.current_sender_index = invitation_template.current_sender_index <= 0 ? 0 : invitation_template.current_sender_index--;
        GameObject f = Instantiate(friend_obj, friends_list);
        TextMeshProUGUI friend_name = f.GetComponentInChildren<TextMeshProUGUI>();
        friend_name.text = g_mng.gangsters[id].GetComponent<Description>().i_name;
        g_mng.gangsters[id].GetComponent<HP>().fui = f.GetComponent<Friend_UI>();
        f.GetComponent<Friend_UI>().hp = g_mng.gangsters[id].GetComponent<HP>();//back and forth with this heiehehehe

        Debug.Log("Friend_Manager::Accept_Friendship()");
    }
    public void Decline_Friendship()
    {
        int id = invitation_template.senders[invitation_template.current_sender_index];
        g_mng.gangsters[id].accepted_friendship = false;
        invitation_template.senders.Remove(id);
        invitation_template.current_sender_index = invitation_template.current_sender_index <= 0 ? 0 : invitation_template.current_sender_index--;
        Debug.Log("Friend_Manager::Decline_Friendship()");

    }

}