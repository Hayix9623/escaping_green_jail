using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // 確保有這個命名空間來使用 InputActionReference

public class ending : MonoBehaviour
{
    // ✨ 建議使用 List<string>，更靈活，但在您的情況下 string[] 也可以
    public string[] talks;
    
    // 💡 建議您將 PlayerMovement 變數的類型加上 using 命名空間，或確保它位於全域命名空間中
    private PlayerMovement pm; 
    
    public TextMeshProUGUI texts;
    private int index = 0;
    
    // 💡 我們不再需要這個變數，因為我們會直接訂閱事件
    // private bool isActing; 
    
    public InputActionReference act;
    public Animator text;

    void Awake()
    {
        
        // 💡 訂閱事件：當輸入動作被執行 (按下) 時，調用 OnActPerformed 函式
        act.action.performed += OnActPerformed;

        // 初始化顯示第一段文字
        if (talks.Length > 0 && texts != null)
        {
             texts.text = talks[index];
        }
    }

    void OnEnable()
    {
        // 確保動作啟用
        if (act.action != null)
        {
            act.action.Enable();
        }
    }

    void OnDisable()
    {
        // 確保動作禁用
        if (act.action != null)
        {
            act.action.Disable();
        }
    }

    // 處理輸入動作執行的函式
    private void OnActPerformed(InputAction.CallbackContext context)
    {
        // 只有在這裡，在按鍵被按下時，才會執行以下代碼
        Debug.Log("Act Performed!");
        
        // 檢查是否還有下一段對話
        if (index < talks.Length - 1)
        {
            index++;
            // 播放 Animator 的 Trigger
            if (text != null)
            {
                text.SetTrigger("next");
            }
            // 立即更新文字
            if (texts != null)
            {
                texts.text = talks[index];
            }
        }
        else
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Debug.Log("Dialog ended.");
            // 例如： gameObject.SetActive(false);
        }
    }

    // 💡 不再需要 Update 函式來輪詢輸入狀態了
    // void Update()
    // {
    //    // ... 
    // }

    void OnDestroy()
    {
        // 💡 在銷毀時取消訂閱，防止記憶體洩漏或錯誤調用
        act.action.performed -= OnActPerformed;
    }
}