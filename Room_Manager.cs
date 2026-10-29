using UnityEngine;
using System.Collections.Generic;
public class Room_Manager : MonoBehaviour
{
    public List<Room> rooms;

    [SerializeField] GameObject civilian;
    [SerializeField] float offset = 1f;
    void Update()
    {
        Manage_Civilians();
    }
    void Manage_Civilians()
    {
        foreach (Room r in rooms)
        {
            if (r.spawned_civilian || rooms.Count == 0) continue;
            GameObject c = Instantiate(civilian, r.transform.position + new Vector3(0f, offset, 0f), Quaternion.identity);
            r.spawned_civilian = c;
            int ran = Random.Range(2, 7);
            Civilian cv = c.GetComponent<Civilian>();
            for (int i = 0; i <= ran; i++)
            {
                Transform p = Get_Random_Point();
                cv.points.Add(p);
            }

        }
    }
    Transform Get_Random_Point()
    {
        int room_index = Random.Range(0, rooms.Count);
        return rooms[room_index].transform;
    }
}