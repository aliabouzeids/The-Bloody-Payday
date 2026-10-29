using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
public class Weapon_Holder : MonoBehaviour
{
    public Weapon weapon;

    [SerializeField] TextMeshProUGUI ammo_text;
    [SerializeField] TextMeshProUGUI max_ammo_text;

    public void Shoot()
    {
        if (weapon) weapon.Shoot();
    }
    void Update()
    {
        UI();
    }
    void UI()
    {
        if (!weapon) return;
        if (ammo_text) ammo_text.text = weapon.ammo.ToString();
        if (max_ammo_text) max_ammo_text.text = weapon.max_ammo.ToString();
        else if (ammo_text) ammo_text.text = "0";
        max_ammo_text.text = weapon.max_ammo.ToString();
    }
}