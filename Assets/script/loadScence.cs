using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    // 拖拽 FaderImage 上的 Animator 组件到这里
    [SerializeField] private Animator faderAnimator;
    
    // 存储目标场景名称，供动画事件使用
    private string targetSceneName;
    
    // 外部设置的、通过 OnTriggerEnter2D 切换的目标场景名称
    public string LoadSceneName;
    public bool istrigged = false;

    // ----------------------------------------------------
    // I. 外部调用：启动场景切换 (淡出)
    // ----------------------------------------------------
    public void GoToScene(string sceneName)
    {
        if (faderAnimator == null)
        {
            Debug.LogError("Fader Animator 未设置，无法进行场景切换！");
            SceneManager.LoadScene(sceneName); // 失败时直接加载
            return;
        }

        // 1. 存储目标场景名称
        targetSceneName = sceneName;

        // 2. 触发 Animator 开始播放 FadeOut 动画 (从透明到不透明)
        // 你的代码原本使用的 Trigger 是 "StartFade"，我们保留它
        faderAnimator.SetTrigger("StartFade"); 
    }
    void Update()
    {
        if (istrigged)
        {
            GoToScene(LoadSceneName);
        }
    }

    // ----------------------------------------------------
    // II. 动画事件触发：在 FadeOut 动画结束时被调用
    // ----------------------------------------------------
    // **注意：这个方法名必须与你在 Animator 中设置的动画事件函数名完全一致**
    public void OnFadeOutComplete()
    {
        // 场景现在完全被 Fader 遮挡（Alpha=1）
       
        // 3. 开始加载新场景
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // 使用同步加载 LoadScene
            SceneManager.LoadScene(targetSceneName);
        }
    }
    public void ReceiveAnimationEvent() 
    {
        Debug.Log("成功从动画事件接收到消息并执行！");
        OnFadeOutComplete();
    }
}