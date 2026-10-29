using UnityEngine;
using System.Collections.Generic;


public class Weapons_Center : MonoBehaviour
{
    public List<Weapon> weapons;

    void Update()
    {
        weapons.RemoveAll(w => w == null);
    }
}