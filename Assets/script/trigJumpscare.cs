using UnityEngine;

public class trigJumpscare : MonoBehaviour
{
    public Animator jumpscare;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player");
            jumpscare.SetTrigger("jumpscare");
        }
    }
}
