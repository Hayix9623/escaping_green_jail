using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class puzzle : MonoBehaviour
{
    private PlayerMovement pm;
    private bool ontrigger;
    private int clickTime;
    private scenceController sc;
    [SerializeField] private GameObject[] GUIs;
    [SerializeField] private GameObject PuzzleGUI;
    private Animator puzAnimator;
    public Animator InputAnimator;
    [SerializeField] private float ClickCoolTime = 1f;
    private UnityEngine.UI.Image target;
    private InputScript input;
    public Sprite puzzle_sprite;
    public int myindex;
    public bool completeStat;
    private Coroutine clicking;
    [Header("驗證設定")]
    [Tooltip("正確的答案/目標字串")]
    public string correctValue = "UNITY"; 
    [Header("顏色設定")]
    public Color correctColor = Color.green;   // 正確時的顏色 (綠色)
    public Color incorrectColor = Color.red;   // 錯誤時的顏色 (紅色)
    public Color defaultColor = Color.white;   // 預設顏色 (黑色)

    void Awake()
    {
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<scenceController>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        puzAnimator = PuzzleGUI.GetComponent<Animator>();
        target = PuzzleGUI.GetComponent<UnityEngine.UI.Image>();
        input = GameObject.FindGameObjectWithTag("Input").GetComponent<InputScript>();
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
            input.puzzleIndex = myindex;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            ontrigger = false;

    }

    public void GUI(bool state)
    {
        for (int i = 0; i < GUIs.Length; i++)
            GUIs[i].SetActive(state);
    }

    public void showPuzzle()
    {
        input.ClearInput();
        setImageTo();
        loadInput();
        puzAnimator.SetBool("showed", true);
        InputAnimator.SetBool("appear",true);
        pm.PlayerState(false);
        GUI(false);
    }

    public void closePuzzle()
    {
        pm.PlayerState(true);
        puzAnimator.SetBool("showed", false);
        InputAnimator.SetBool("appear",false);
        input.ResetTextColor();
        input.ClearInput();
        if (sc.completePuzzle_num != 3) GUI(true);
    }
    private void setImageTo()
    {
        if (target != null && puzzle_sprite != null)
        {
            target.sprite = puzzle_sprite;
        }
    }
    private void loadInput()
    {
        input.correctValue = correctValue;
        input.correctColor = correctColor;
        input.incorrectColor = incorrectColor;
        input.defaultColor = defaultColor;
    }
}
