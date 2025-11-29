using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class scenceController : MonoBehaviour
{
    public int completePuzzle_num = 0;
    public PlayableDirector drama;
    public void playDrama()
    {
        if (completePuzzle_num == 3)
        {
            drama.Play();
        }
    }
}
