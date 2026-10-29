using UnityEngine;

public class Fall_Death : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        HP hp = col.gameObject.GetComponent<HP>();
        if (hp) hp.hp = 0;
    }
}