using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform owner;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject fx;
    public Transform sp;
    public int ammo = 0;
    public int max_ammo = 50;
    [SerializeField] float bullet_speed;
    [SerializeField] float fire_rate = 1f;
    float fire_timer = 0f;
    [SerializeField] Transform aim;
    float deactivate_timer = 0f;
    [SerializeField] float deactivate_after = 10f;
    [SerializeField] bool new_way = true;

    void Register()
    {
        Weapons_Center wc = GameObject.Find("game_manager").GetComponent<Weapons_Center>();
        wc.weapons.Add(this);
    }
    void Awake()
    {
        aim = GameObject.Find("aim").transform;
    }
    void Start()
    {
        Register();
        Refill();
    }
    public void Refill()
    {
        ammo = max_ammo;
    }
    void Update()
    {
        fire_timer += Time.deltaTime;
        if (!owner && transform.parent)
        {
            transform.parent = null;
        }
        if (Is_Empty()) deactivate_timer += Time.deltaTime;
        else deactivate_timer = 0f;
        Deactivate_If_Empty();

    }
    Vector3 Direction()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {

            Vector3 scree_point = cam.WorldToScreenPoint(aim.position);
            Ray ray = cam.ScreenPointToRay(scree_point);
            Vector3 target_point = ray.GetPoint(1000f);
            return (target_point - sp.position).normalized;
        }
        var direction = aim.position - sp.position;
        direction.Normalize();
        return direction;
    }
    public void Shoot()
    {
        if (new_way)
        {
            New_Shoot();
            return;
        }
        if (!bullet || !sp || fire_timer <= fire_rate || ammo <= 0) return;

        fire_timer = 0f;
        Vector3 dir = Direction();
        GameObject b = Instantiate(bullet, sp.position, Quaternion.identity);
        if (fx) Instantiate(fx, sp);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb) rb.linearVelocity = dir * bullet_speed;
        Bullet bul = b.GetComponent<Bullet>();
        if (bul) bul.mine = owner;
        ammo--;

    }
    void New_Shoot()
    {
        if (!bullet || !sp || fire_timer <= fire_rate || ammo <= 0) return;

        fire_timer = 0f;
        GameObject b = Instantiate(bullet, sp.position, Quaternion.identity);
        Vector3 dir = aim.position - b.transform.position;
        dir.Normalize();
        if (fx) Instantiate(fx, sp);
        Bullet bul = b.GetComponent<Bullet>();
        bul.destination = aim.position;
        bul.speed = bullet_speed;
        if (bul) bul.mine = owner;
        ammo--;
    }
    public bool Is_Empty()
    {
        return ammo <= 0;
    }
    public void Character_Shoot(Vector3 destination)
    {
        if (!bullet || !sp || fire_timer <= fire_rate || ammo <= 0) return;

        fire_timer = 0f;
        Vector3 dir = (destination - sp.position).normalized;
        GameObject b = Instantiate(bullet, sp.position, Quaternion.identity);
        if (fx) Instantiate(fx, sp);
        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb) rb.linearVelocity = dir * bullet_speed;
        Bullet bul = b.GetComponent<Bullet>();
        if (bul) bul.mine = owner;
        ammo--;
    }
    void Deactivate_If_Empty()
    {
        if (Is_Empty() && deactivate_after <= deactivate_timer)
        {
            deactivate_timer = 0f;
            gameObject.SetActive(false);
        }
    }

}