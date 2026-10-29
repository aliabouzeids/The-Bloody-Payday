using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Civilian : MonoBehaviour
{

    public List<Transform> points;
    int goal_point_index = 0;

    NavMeshAgent agent;
    bool been_attacked = false;
    [SerializeField] float run_speed = 25f;
    HP hp;


    void Awake()
    {
        hp = GetComponent<HP>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        been_attacked = hp.hp == hp.max_hp;

        if (been_attacked) Run();
        else Have_A_Walk();
    }

    void Run()
    {
        if (!been_attacked) return;
        agent.speed = run_speed;
        agent.SetDestination(points[points.Count - 1].position);
    }
    void Have_A_Walk()
    {
        if (been_attacked) return;
        if (agent.destination != points[goal_point_index].position) agent.SetDestination(points[goal_point_index].position);
        float d = Vector3.Distance(transform.position, agent.destination);
        if (d <= 4f)
        {
            goal_point_index++;
            if (goal_point_index == points.Count) Destroy(gameObject);//reached its final destination safely so yeah they disappear...
        }//its final goal is to reach the finish line without getting killed...

    }
}