using UnityEngine;
using System.Collections.Generic;


public class Gangsters_Manager : MonoBehaviour
{
    public List<Gangster> gangsters;
    void Update() => gangsters.RemoveAll(g => g == null);
}