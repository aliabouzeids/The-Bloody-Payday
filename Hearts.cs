using UnityEngine;

public class Hearts : MonoBehaviour
{
    [SerializeField] HP hp;
    [SerializeField] int number;

    void Update()
    {
        if (hp && hp.hp < number) Destroy(gameObject);
    }
}