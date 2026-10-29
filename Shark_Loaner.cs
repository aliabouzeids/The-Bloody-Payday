using UnityEngine;


public class Shark_Loaner : MonoBehaviour
{
    Player p;
    [SerializeField] GameObject shark;
    [SerializeField] int max_money_needed = 50000;
    void Awake() => p = GameObject.Find("player").GetComponent<Player>();
    void Update() => Spawn();
    void Spawn()
    {
        if (p.money >= max_money_needed)
        {
            shark.SetActive(true);
            Destroy(gameObject);
        }
    }
}