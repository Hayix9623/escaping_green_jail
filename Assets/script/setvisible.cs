using UnityEngine;

public class setvisible : MonoBehaviour
{
    public bool statement;
    void Start()
    {
        gameObject.SetActive(statement); 
    }
}
