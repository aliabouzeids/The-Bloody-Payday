using UnityEngine;


public class Bullet : MonoBehaviour
{
    public Transform mine;
    [SerializeField] int dmg = 1;
    public Vector3 destination;
    public float speed;
    public bool new_way = true;
    Vector3 direction;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Bullet>()) return;
        if (collider.gameObject.GetComponent<HP>() &&
        collider.transform != mine)
        {
            collider.gameObject.GetComponent<HP>().killer = mine;
            collider.gameObject.GetComponent<HP>().Take_Dmg(dmg);
        }
        Destroy(gameObject);
    }


    void Update()
    {
        if (new_way) Go_All_The_Way();
    }
    void Go_All_The_Way()
    {
        if (direction == Vector3.zero) direction = (destination - transform.position).normalized;
        transform.position += direction * speed;
    }
}