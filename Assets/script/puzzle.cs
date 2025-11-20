using UnityEngine;

public class puzzle : MonoBehaviour
{
    private PlayerMovement pm;
    private bool ontrigger;
    private int clickTime;
    [SerializeField] private GameObject[] GUIs;
    [SerializeField] private GameObject puzz;
    [SerializeField] private Animator puz;
    void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        puzz.SetActive(false);
    }
    void Update()
    {
        if (pm.isActing && ontrigger && clickTime < 2)
        {
            clickTime += 1;
        }
        else if (clickTime >= 2)
        {
            clickTime = 0;
        }
        switch (clickTime)
        {
            case 1:
                showPuzzle();
                break;
            case 2:
            closePuzzle();
                break;

        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ontrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ontrigger = false;
        }
    }
    private void GUI(bool state)
    {
        for (int i = 0; i < GUIs.Length;i++)
        {
            GUIs[i].SetActive(state);
        }
    }
    private void showPuzzle()
    {
        GUI(false);
    }
    private void closePuzzle()
    {
        GUI(true);
    }

}
