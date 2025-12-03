using UnityEngine;
using UnityEngine.SceneManagement;
public class doorTrigger : MonoBehaviour
{
    private LoadScene ls;
    void Start()
    {
        ls = GameObject.FindGameObjectWithTag("fade").GetComponent<LoadScene>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ls.istrigged = true;
        }
    }
}
