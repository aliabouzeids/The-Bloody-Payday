using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Load_Scene : MonoBehaviour
{
    public string scene_name;

    [SerializeField] float sec;
    public bool on_start = true;
    void Start()
    {
        if (on_start) Load_S();
        else Load_After();
    }
    IEnumerator Load_After()
    {
        yield return new WaitForSeconds(sec);
        Load_S();
    }
    void Load_S() => SceneManager.LoadScene(scene_name);


}