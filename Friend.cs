using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Friend : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public Weapon weapon;
    Transform target;
    [SerializeField] float distance_tolerated_from_player = 10f;
    bool player_in_range = false;
    [SerializeField] float attacking_distance = 20f;
    [SerializeField] float offset = 0.3f;
    bool target_at_sight = false;
    bool target_in_range = false;
    [SerializeField] LayerMask gang_layer;
    Gangsters_Manager g_mng;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        g_mng = GameObject.Find("game_manager").GetComponent<Gangsters_Manager>();
        player = GameObject.Find("player").transform;
    }
    void Get_Target()
    {
        if (target) return;
        float d = Mathf.Infinity;
        foreach (Gangster g in g_mng.gangsters)
        {
            if (!g.betray) continue;
            if (g.IsDestroyed()) continue;
            float dis = Vector3.Distance(transform.position, g.transform.position);
            if (d > dis)
            {
                d = dis;
                target = g.transform;
            }
        }
    }
    public void F_Update()
    {
        Get_Target();
        player_in_range = Vector3.Distance(transform.position, player.position) <= distance_tolerated_from_player;
        if (target)
        {
            Vector3 dir = (target.position + new Vector3(0f, offset, 0f) - (transform.position + new Vector3(0f, offset, 0f))).normalized;
            Debug.DrawLine(transform.position, target.position + new Vector3(0f, offset, 0f), Color.cyan);
            target_at_sight = Physics.Raycast(transform.position, dir, attacking_distance * 2, gang_layer);
            target_in_range = Physics.Raycast(transform.position, dir, attacking_distance, gang_layer);
        }
        else Get_Target();
        Spagetti();
    }
    void Spagetti()
    {
        if (target)
        {
            if (target_in_range)
            {
                Attack();
            }
            else if (target_at_sight && !target_in_range) Chase();
            else Follow_Player();
        }
        else Follow_Player();
    }
    void Follow_Player()
    {
        if (target && target_in_range) return;
        if (agent.destination != player.position) agent.SetDestination(player.position);
        float d = Vector3.Distance(player.position, transform.position);
        if (d < 3f)
        {
            agent.ResetPath();
            if (agent.destination != transform.position) agent.SetDestination(transform.position);
        }
    }
    void Chase()
    {
        if (!target_at_sight || !target) return;
        if (agent.destination != target.position) agent.SetDestination(target.position);
    }
    void Attack()
    {
        if (!target || !weapon || weapon.Is_Empty()) return;
        if (agent.destination != transform.position) agent.SetDestination(transform.position);
        transform.LookAt(target);
        weapon.Character_Shoot(target.position + new Vector3(0f, offset, 0f));//note no transitioning from attacking state
    }

}