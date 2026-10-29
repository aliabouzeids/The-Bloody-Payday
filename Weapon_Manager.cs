using UnityEngine;


public class Weapon_Manager : MonoBehaviour
{
    [SerializeField] float close_enough = 3f;
    [SerializeField] Transform weapon_default_position;
    Weapon to_grab_weapon;
    Weapons_Center weapon_center;
    Weapon_Holder weapon_holder;

    void Awake()
    {
        weapon_holder = GetComponent<Weapon_Holder>();
        weapon_center = GameObject.Find("game_manager").GetComponent<Weapons_Center>();
    }
    public void Grab_Weapon()
    {
        float distance = Mathf.Infinity;

        foreach (Weapon w in weapon_center.weapons)
        {
            if (w.owner) continue;
            float d = Vector3.Distance(transform.position, w.transform.position);
            if (d > close_enough) continue;
            if (d < distance)
            {
                distance = d;

                to_grab_weapon = w;
            }
            if (to_grab_weapon)
            {
                weapon_holder.weapon = to_grab_weapon;
                weapon_holder.weapon.owner = transform;
                Rigidbody rb = weapon_holder.weapon.GetComponent<Rigidbody>();
                if (rb) rb.constraints = RigidbodyConstraints.FreezeAll;
                if (rb) rb.useGravity = false;
                if (rb) rb.isKinematic = true;
                Collider col = weapon_holder.weapon.GetComponent<Collider>();
                if (col) col.enabled = false;
                weapon_holder.weapon.transform.position = weapon_default_position.position;
                weapon_holder.weapon.transform.forward = weapon_default_position.forward;
                weapon_holder.weapon.transform.parent = weapon_default_position;

            }
        }
        to_grab_weapon = null;
    }
    public void Throw_Weapon()
    {
        weapon_holder.weapon.owner = null;
        weapon_holder.weapon.transform.parent = null;
        Rigidbody rb = weapon_holder.weapon.GetComponent<Rigidbody>();
        if (rb) rb.constraints = RigidbodyConstraints.None;
        if (rb) rb.useGravity = true;
        if (rb) rb.isKinematic = false;
        Collider col = weapon_holder.weapon.GetComponent<Collider>();
        if (col) col.enabled = true;
        weapon_holder.weapon = null;


    }

}