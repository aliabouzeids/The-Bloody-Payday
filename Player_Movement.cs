using UnityEngine;


enum camera_type
{
    FIRST_PERSON,
    THIRD_PERSON,
}
public class Player_Movement : MonoBehaviour
{
    public Vector2 look;
    Transform main_camera;
    public Input_System input_system;
    [SerializeField] float move_speed = 1f;
    Vector2 move;
    Vector3 move_direction;
    float move_x;
    float move_y;
    [SerializeField] float ground_distance = 0.4f;
    [SerializeField] float ground_offset = 0.5f;
    [SerializeField] bool is_grounded;
    [SerializeField] LayerMask ground_layer;
    public float jump_force = 5f;
    public bool is_shooting = false;
    Rigidbody rb;


    bool is_moving;
    AudioSource walk_sound;
    [SerializeField] GameObject jump_sound;
    Weapon_Holder weapon_holder;
    Weapon_Manager weapon_manager;
    public GameObject pause_menu;
    void Awake()
    {
        walk_sound = GetComponent<AudioSource>();
        if (input_system == null) input_system = new Input_System();
        rb = GetComponent<Rigidbody>();
        weapon_holder = GetComponent<Weapon_Holder>();
        weapon_manager = GetComponent<Weapon_Manager>();
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Input_Values()
    {
        move_x = move.x;
        move_y = move.y;

    }
    void OnEnable()
    {

        input_system.Inputs.Move.performed += _ => move = _.ReadValue<Vector2>();
        input_system.Inputs.Move.canceled += _ => move = Vector2.zero;
        input_system.Inputs.Look.performed += _ => look = _.ReadValue<Vector2>();
        input_system.Inputs.Jump.performed += _ => Jump();
        input_system.Inputs.Shoot.performed += _ => is_shooting = true;
        input_system.Inputs.Shoot.canceled += _ => is_shooting = false;
        input_system.Inputs.Hand_Weapon.performed += _ => Hand_Weapon();
        input_system.Inputs.Cursor.performed += _ => Cursor_Open_Close();
        input_system.Inputs.Pause.performed += _ => Pause();
        input_system.Enable();
    }
    void Pause()
    {
        Time.timeScale = 0f;
        pause_menu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void OnDisable()
    {
        input_system.Disable();
    }


    void Update()
    {
        is_moving = move_x != 0 || move_y != 0;


        Input_Values();

        Check_Ground();
        if (is_shooting) weapon_holder.Shoot();

    }
    void FixedUpdate()
    {
        if (is_moving) Move();
        else if (walk_sound.isPlaying || !is_grounded) walk_sound.Stop();

    }

    ///======================================================================================================
    ///================================MOVEMENT FUNCTIONS====================================================
    ///======================================================================================================








    void Move()
    {
        if (walk_sound && !walk_sound.isPlaying && is_grounded) walk_sound.Play();
        move_direction = Camera.main.transform.forward * move_y;
        move_direction += Camera.main.transform.right * move_x;
        move_direction.Normalize();
        move_direction.y = 0;
        move_direction = move_direction * move_speed;


        Vector3 movement_Velocity = move_direction;
        movement_Velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = movement_Velocity;

    }
    public void Jump()
    {
        if (!is_grounded) return;
        if (jump_sound) Instantiate(jump_sound);
        rb.AddForce(Vector3.up * jump_force, ForceMode.VelocityChange);
    }
    void Check_Ground()
    {
        Vector3 player_foots = transform.position - new Vector3(0f, ground_offset, 0f);
        is_grounded = Physics.CheckSphere(player_foots, ground_distance, ground_layer);
    }

    void Hand_Weapon()
    {
        if (weapon_holder.weapon) weapon_manager.Throw_Weapon();
        else weapon_manager.Grab_Weapon();
    }
    void OnDrawGizmos()
    {
        Vector3 player_foots = transform.position - new Vector3(0f, ground_offset, 0f);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(player_foots, ground_distance);
    }

    void Cursor_Open_Close()
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
