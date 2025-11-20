using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class playerBar : MonoBehaviour
{
    public Slider slider;
    public float maxVaule = 100f;
    private float currentVaule;
    public float duration = 1f;
    private Coroutine currentCoroutine;
    void Start()
    {
        slider.value = maxVaule;
    }
    public void SliderTovaule(float vaule)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentVaule = slider.value;
        currentCoroutine = StartCoroutine(setToVaule(vaule));
    }
    private IEnumerator setToVaule(float targetValue)
    {
        float startValue = slider.value;
        float totalDistance = Mathf.Abs(targetValue - startValue);
        float elapsed = 0f;
        float speed = totalDistance / duration;


        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            slider.value = Mathf.MoveTowards(slider.value, targetValue, speed * Time.deltaTime);
            yield return null;
        }


        slider.value = targetValue; // 確保最後值精準
    }
}
