using System.Collections;
using UnityEngine;

public class puzzle : MonoBehaviour
{
    private PlayerMovement pm;
    private bool ontrigger;
    private int clickTime;
    [SerializeField] private GameObject[] GUIs;
    [SerializeField] private GameObject PuzzleGUI;
    private Animator puzAnimator;
    [SerializeField] private float ClickCoolTime = 1f;

    private Coroutine clicking;

    void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        puzAnimator = PuzzleGUI.GetComponent<Animator>();
    }

    void Update()
    {
        // 玩家按下（或你的 pm 的按鍵條件）
        if (pm.isActing && ontrigger)
        {
            StartClickCoroutine();
        }
    }

    private void StartClickCoroutine()
    {
        if (clicking != null)
            return; // ← 正在冷卻中，不允許再次

        clicking = StartCoroutine(DelayClickTime());
    }

    private IEnumerator DelayClickTime()
    {
        clickTime++;

        switch (clickTime)
        {
            case 1:
                showPuzzle();
                break;

            case 2:
                closePuzzle();
                break;
        }

        // 冷卻
        yield return new WaitForSeconds(ClickCoolTime);

        // 超過 2 次就 reset
        if (clickTime >= 2)
            clickTime = 0;

        clicking = null; // 標記冷卻結束
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            ontrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            ontrigger = false;
    }

    private void GUI(bool state)
    {
        for (int i = 0; i < GUIs.Length; i++)
            GUIs[i].SetActive(state);
    }

    private void showPuzzle()
    {
        puzAnimator.SetBool("showed", true);
        pm.PlayerState(false);
        GUI(false);
    }

    private void closePuzzle()
    {
        puzAnimator.SetBool("showed", false);
        pm.PlayerState(true);
        GUI(true);
    }
}
