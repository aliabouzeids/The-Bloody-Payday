using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public Weapon weapon;
    bool player_in_range = false;
    [SerializeField] float attacking_distance = 20f;
    bool player_at_sight = false;
    [SerializeField] LayerMask gang_layer;

    void Awake()
    {
        player = GameObject.Find("player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    public void E_Update()
    {
        var dir = player.position - transform.position;
        dir.Normalize();
        player_in_range = Physics.Raycast(transform.position, dir, attacking_distance, gang_layer);
        player_at_sight = Physics.Raycast(transform.position, dir, attacking_distance * 3, gang_layer);
        Spagetti();
    }
    void Spagetti()
    {
        if (player_at_sight && !player_in_range) Chase();
        else if (player_in_range) Attack();
        else Idle();
    }
    void Idle()
    {
        agent.ResetPath();
        if (agent.destination != transform.position) agent.SetDestination(transform.position);
    }
    void Attack()
    {
        if (!weapon || weapon.Is_Empty() || !player_in_range) return;
        transform.LookAt(player);
        weapon.Character_Shoot(player.position + new Vector3(0f, 0.3f, 0f));
    }
    void Chase()
    {
        if (agent.destination != player.position) agent.SetDestination(player.position);
    }


}