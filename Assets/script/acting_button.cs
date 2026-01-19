using Unity.VisualScripting;
using UnityEngine;

public class acting_button : MonoBehaviour
{
    [SerializeField] private  GameObject acting_icon;
    [SerializeField] private GameObject acting;
    private Animator am;
    public bool pressed;
    private PlayerMovement pm;
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("origin_player").GetComponent<PlayerMovement>();
        am = GetComponent<Animator>();
        if (pm.isPC)
        {
            acting.SetActive(false);
        }
    }
    void Update()
    {
        if (pm.trigged)
        {
           am.SetBool("trigger",true);

        }
        else
        {
           am.SetBool("trigger",false);
        }
    }
    public void buttonPressed()
    {
        pressed = true;
    }
    public void buttonreleased()
    {
        pressed = false;
    }
    public void setButton(bool state){
        acting.SetActive(state);
    }

}
