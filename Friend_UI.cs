using UnityEngine;


public class Friend_UI : MonoBehaviour
{
    public HP hp;
    public void Cancel_Friendship()
    {
        hp.GetComponent<HP>().GetComponent<Gangster>().betray = true;
        Destroy(gameObject);
    }

}