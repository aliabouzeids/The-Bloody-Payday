using UnityEngine;
public class Room : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    GameObject spawned_weapon;//used in the room manager;
    public GameObject spawned_civilian;
    [SerializeField] float offset = 1.5f;
    void Start()
    {
        GameObject.Find("game_manager").GetComponent<Room_Manager>().rooms.Add(this);
    }
    void Spawn_Weapon() => spawned_weapon = Instantiate(weapon, transform.position + new Vector3(0f, offset, 0f), Quaternion.identity);



    void Update()
    {
        if (!spawned_weapon) Spawn_Weapon();
        else if (!spawned_weapon.activeSelf)
        {
            spawned_weapon.transform.position = transform.position + new Vector3(0f, offset, 0f);
            if (spawned_weapon.TryGetComponent<Weapon>(out var w)) w.Refill();
            spawned_weapon.SetActive(true);
        }
    }
}