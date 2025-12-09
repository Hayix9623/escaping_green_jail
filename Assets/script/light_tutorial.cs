using UnityEngine;

public class light_tutorial : MonoBehaviour
{
    private bool trigged;
    public Animator fade;
    void Update()
    {
        if (trigged)
        {
            fade.SetTrigger("next");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigged = true;
        }
    }
    public void destrooy()
    {
        Destroy(gameObject);  
    }
}
