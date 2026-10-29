using UnityEngine;
using UnityEngine.AI;
public class Gangster : MonoBehaviour
{
    public bool shark_loaner = false;
    NavMeshAgent agent;
    [SerializeField] int money_to_betray_for = 400;
    [SerializeField] int money_to_befriend_for = 200;
    Friend friend;
    Enemy enemy;
    Player p;
    public bool betray = false;
    public bool befriend = false;
    public bool accepted_friendship = false;
    [SerializeField] float friend_distance = 4f;
    Friend_Manager friends_mng;
    bool sent_invitation = false;
    Weapon weapon;
    Weapon to_get_weapon;
    [SerializeField] Transform weapon_place;
    Weapons_Center weapons_center;
    [SerializeField] float weapon_close_to_pick_up = 3f;
    [SerializeField] Material red;
    [SerializeField] Material green;
    [SerializeField] Material gray;
    Gangsters_Manager gang_mng;
    Renderer render;
    void Awake()
    {
        render = GetComponent<Renderer>();
        p = GameObject.Find("player").GetComponent<Player>();
        friends_mng = p.GetComponent<Friend_Manager>();
        agent = GetComponent<NavMeshAgent>();
        weapons_center = GameObject.Find("game_manager").GetComponent<Weapons_Center>();
        friend = GetComponent<Friend>();
        enemy = GetComponent<Enemy>();
        gang_mng = weapons_center.transform.GetComponent<Gangsters_Manager>();
    }
    void Start()
    {
        GameObject.Find("game_manager").GetComponent<Gangsters_Manager>().gangsters.Add(this);
    }
    void Check_To_Betray()
    {
        if (betray) return;
        if (p.money >= money_to_betray_for) betray = true;
    }
    void Check_To_Befriend()
    {
        if (befriend) return;
        if (p.money >= money_to_befriend_for) befriend = true;
    }
    void Manage_Weapons()
    {
        if (weapon && weapon.Is_Empty()) Throw_Weapon();
        if (!weapon) Search_For_Weapon();
        if (weapon && !weapon.Is_Empty()) to_get_weapon = null;
        friend.weapon = weapon;
        enemy.weapon = weapon;
    }
    void Update()
    {
        Check_To_Befriend();
        Check_To_Betray();
        Manage_Weapons();
        Gangster_Plan();
    }
    void Gangster_Plan()
    {
        if (!weapon || weapon.Is_Empty()) return;
        if (!accepted_friendship && befriend && !sent_invitation) Send_Friend_Invite();
        if (betray) Enemy();
        else if (accepted_friendship && !betray) Friend();
        else Neutral();
    }

    void Friend()
    {
        if (green && render.material != green) render.material = green;
        friend.F_Update();
    }
    void Send_Friend_Invite()
    {
        if (!befriend) return;
        float distance = Vector3.Distance(p.transform.position, transform.position);
        if (distance <= friend_distance && friends_mng.friends_number < friends_mng.max_friends_number)
        {
            int my_id = Get_My_Id();
            friends_mng.Set_Invite(my_id);
            sent_invitation = true;
            Debug.Log("Gangster::Send_Friend_Invite()");

        }
    }
    int Get_My_Id()
    {
        int i = 0;
        foreach (Gangster g in gang_mng.gangsters)
        {
            if (g != this) i++;
            if (g == this) break;
        }
        return i;
    }
    void Enemy()
    {
        if (red && render.material != red) render.material = red;
        enemy.E_Update();
    }
    void Neutral()
    {
        if (gray && render.material != gray) render.material = gray;
        agent.ResetPath();
        if (agent.destination != transform.position) agent.SetDestination(transform.position);
    }
    void Grab_Weapon(Weapon to_grab_weapon)
    {
        if (to_grab_weapon.owner)
        {
            to_get_weapon = null;
            return;
        }
        weapon = to_grab_weapon;
        weapon.owner = transform;
        Rigidbody rb = weapon.GetComponent<Rigidbody>();
        if (rb) rb.useGravity = false;
        if (rb) rb.isKinematic = true;
        Collider col = weapon.GetComponent<Collider>();
        if (col) col.enabled = false;
        weapon.transform.position = weapon_place.position;
        weapon.transform.forward = weapon_place.forward;
        weapon.transform.parent = weapon_place;
        to_get_weapon = null;
    }
    void Throw_Weapon()
    {
        weapon.owner = null;
        weapon.transform.parent = null;
        Rigidbody rb = weapon.GetComponent<Rigidbody>();
        if (rb) rb.useGravity = true;
        if (rb) rb.isKinematic = false;
        Collider col = weapon.GetComponent<Collider>();
        if (col) col.enabled = true;
        weapon = null;
    }
    void Search_For_Weapon()
    {
        if (weapon) return;
        float distance = Mathf.Infinity;

        if (!to_get_weapon)
        {
            foreach (Weapon w in weapons_center.weapons)
            {
                if (w.owner || w.Is_Empty()) continue;
                float dis = Vector3.Distance(transform.position, w.transform.position);
                if (dis < distance)
                {
                    distance = dis;
                    to_get_weapon = w;
                }

            }
        }
        if (to_get_weapon)
        {
            agent.SetDestination(to_get_weapon.transform.position);
            float d = Vector3.Distance(transform.position, to_get_weapon.transform.position);
            if (d <= weapon_close_to_pick_up) Grab_Weapon(to_get_weapon);
        }

    }
}