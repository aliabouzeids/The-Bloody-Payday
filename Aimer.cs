using UnityEngine;

public class Aimer : MonoBehaviour
{
    [SerializeField] LayerMask layers;
    void Update() => Aim();

    void Aim()
    {
        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(center);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, layers))
        {
            transform.position = hit.point;
        }
    }

}