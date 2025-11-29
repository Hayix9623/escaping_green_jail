using UnityEngine;

public class light : MonoBehaviour
{
    private Animator am;
    void Start()
    {
        am = GetComponent<Animator>();
    }
    public void appear()
    {
        am.SetTrigger("appear");
    }
}
