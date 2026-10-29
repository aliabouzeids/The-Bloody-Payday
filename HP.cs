using UnityEngine;
using TMPro;
public class HP : MonoBehaviour
{


    public int hp;
    public int max_hp = 100;
    public Transform killer;
    public int money_on_death = 100;
    public Friend_UI fui;
    [SerializeField] GameObject fx;
    [SerializeField] GameObject lost_menu;
    [SerializeField] GameObject load_ending;
    void Start()
    {
        hp = max_hp;
    }
    void Update()
    {
        if (hp <= 0) Die();
        if (hp > 0 && GetComponent<Player>() && !GetComponent<Player_Movement>().pause_menu.activeSelf) Time.timeScale = 1f;
    }
    public void Take_Dmg(int amount)
    {
        hp = amount >= hp ? 0 : hp - amount;
        if (hp == 0 && killer)
        {
            if (killer.GetComponent<Gangster>()) killer.GetComponent<Money>().money += money_on_death;
            else if (killer.GetComponent<Player>()) killer.GetComponent<Player>().money += money_on_death;
            money_on_death = 0;


        }
    }
    public void Heal(int amount)
    {
        hp = amount + hp > max_hp ? max_hp : hp + amount;
    }
    void Die()
    {
        if (fui) Destroy(fui.gameObject);
        if (fx) Instantiate(fx, transform.position, fx.transform.rotation);
        if (GetComponent<Gangster>() && GetComponent<Gangster>().shark_loaner) load_ending.SetActive(true);
        if (GetComponent<Player>())
        {
            Time.timeScale = 0f;
            if (lost_menu) lost_menu.SetActive(true);
            TextMeshProUGUI cause_of_death = lost_menu.GetComponentInChildren<TextMeshProUGUI>();
            bool betrayed = killer && killer.GetComponent<Gangster>().accepted_friendship && killer.GetComponent<Gangster>().betray;
            string name_of_killer = killer.GetComponent<Description>().i_name;
            string tell = betrayed ? $"Your Friend {name_of_killer} BETRAYED YOU! I'll <TRUST-NO-ONE> anymore" : $"Your Enemy {name_of_killer} Killed You, Your friends were loyal till the end";
            cause_of_death.text = tell;

        }
        else Destroy(gameObject);
    }
}