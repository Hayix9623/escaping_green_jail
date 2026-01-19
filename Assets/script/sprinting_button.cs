using UnityEngine;

public class sprinting_button : MonoBehaviour
{
    [SerializeField] private  GameObject sprinting_icon;
    public bool pressed;
    public void buttonPressed()
    {
        pressed = true;
    }
    public void buttonreleased()
    {
        pressed = false;
    }
}
