using UnityEngine;

public class gunTrigger : MonoBehaviour
{
    private LoadScene ls;
    void Start()
    {
        ls = GetComponent<LoadScene>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ls.istrigged = true;
            ls.ending2 = true;
        }
    }
}
