using UnityEngine;
using UnityEngine.AI;
public class fmm : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    private bool ontrigged;
    private bool reach= true;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ontrigged)
        {
            agent.SetDestination(target.position);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ontrigged = true;  
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ontrigged = false;
        }
    }
}
