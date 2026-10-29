using UnityEngine;

public class Canvas_Turn_To_Camera : MonoBehaviour
{
    Transform t;
    void Awake()
    {
        t = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(t);
        var rot = transform.eulerAngles;
        rot.x = 0f; rot.z = 0f;
        transform.eulerAngles = rot;

    }
}