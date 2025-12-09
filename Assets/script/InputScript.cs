using UnityEngine;
using TMPro;
using System.Collections; // 必須引用 TextMeshPro 的命名空間

public class InputScript : MonoBehaviour
{
    // 在 Inspector 中將 Input Field (TMP) 拖曳到這個欄位
    public TMP_InputField inputField;
    public string correctValue;
    public Color correctColor;
    public Color incorrectColor;
    public Color defaultColor;
    public Animator puzAnimator;
    public Animator InputAnimator;
    public int puzzleIndex;
    private Coroutine closing;
    private Coroutine  cleaning;
    private scenceController sc;
    public GameObject[] puzzles;
    private puzzle puzScript;
    void Start()
    {
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<scenceController>();
        puzScript = GameObject.FindGameObjectWithTag("puzzle").GetComponent<puzzle>();
        if (inputField != null)
        {
            // 將文字顏色設定回預設值
            SetInputTextColor(defaultColor); 

            // 監聽 onEndEdit 事件：當使用者按下 Enter 或失去焦點時觸發
            inputField.onEndEdit.AddListener(ValidateInput);
        }
    }

    // 當輸入完成時調用的驗證函數
    void ValidateInput(string inputText)
    {
        // 為了確保比較精確，通常會先移除兩側的空格 (Trim)
        string inputToCompare = inputText.Trim().ToUpper(); // 轉成大寫方便不區分大小寫比較

        if (inputToCompare == correctValue.ToUpper())
        {
            // 驗證成功
            SetInputTextColor(correctColor);
            OnCorrectInput(inputText);
        }
        else
        {
            // 驗證失敗
            SetInputTextColor(incorrectColor);
            Invoke("ResetTextColor", 1.0f); 
            OnWrongInput();
        }
    }

    // 實際設定文字顏色的函數
    public void SetInputTextColor(Color targetColor)
    {
        // Input Field 的文字物件是它的 TextComponent 屬性
        if (inputField.textComponent is TextMeshProUGUI tmpText)
        {
            tmpText.color = targetColor;
        }
        // // 如果您使用的是傳統 UI (Legacy UI)，則使用：
        // else if (inputField.textComponent is Text legacyText)
        // {
        //     legacyText.color = targetColor;
        // }
    }

    // 重設顏色為預設值
    public void ResetTextColor()
    {
        // 只有在輸入欄位未被選中時才重設顏色，避免影響使用者輸入
        if (!inputField.isFocused)
        {
            SetInputTextColor(defaultColor);
        }
    }
    
    // 驗證成功後執行回傳值的具體函數
    void OnCorrectInput(string finalValue)
    {
        if (closing != null)
        {
            StopCoroutine(closeGUI());
        }
        puzzles[puzzleIndex].SetActive(false);
        sc.completePuzzle_num += 1;
        closing = StartCoroutine(closeGUI());
        sc.playDrama();
    }
    void OnWrongInput()
    {
       if (cleaning != null)
        {
            StopCoroutine(cleanTexts());
        } 
        cleaning = StartCoroutine(cleanTexts());
    }
    IEnumerator closeGUI()
    {
        yield return new WaitForSeconds(1f);
        puzScript.closePuzzle();
    }
    IEnumerator cleanTexts()
    {
        yield return new WaitForSeconds(1f);
        ClearInput();
    }
    public void ClearInput()
{
    // 將 .text 屬性設為空字串，即可清空輸入框的內容
    inputField.text = "";
    
    // 可選：將遊標設置回輸入框，以便用戶可以立即開始新的輸入
    inputField.ActivateInputField(); 
}
}