using UnityEngine;
using System.Collections.Generic;


public class Gang_Sp : MonoBehaviour
{
    [SerializeField] List<GameObject> gangsters;
    [SerializeField] List<Transform> points;
    List<GameObject> gangsters_alive;
    [SerializeField] int max_number = 0;
    float timer;
    [SerializeField] float interval = 0.4f;


    void Awake()
    {
        gangsters_alive = new List<GameObject>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        Cleaner(); Spawn();
    }

    void Spawn()
    {
        if (gangsters.Count <= 0 || points.Count <= 0 || timer < interval || gangsters_alive.Count >= max_number) return;
        int g_index = Random.Range(0, gangsters.Count);
        int p_index = Random.Range(0, points.Count);
        GameObject gangster = Instantiate(gangsters[g_index], points[p_index].position, Quaternion.identity);
        gangsters_alive.Add(gangster);
    }

    void Cleaner()
    {
        if (gangsters_alive.Count > 0) gangsters_alive.RemoveAll(g => g == null);
        gangsters.RemoveAll(g => g == null);
        points.RemoveAll(p => p == null);
    }
}