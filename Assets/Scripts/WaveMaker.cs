using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WaveMaker : MonoBehaviour
{
    public int bpm;
    public Button button;

    void Start()
    {
        //anim = m_Animator.getComponent<m_Animator>();
        float bps = bpm / 60.0f;
        InvokeRepeating("AnimateBeats", 0.0f, 1.0f/bps);
    }

    //Triggers an animation on each beat
    void AnimateBeats()
    {
        button.onClick.Invoke();
        BaseEventData data = new BaseEventData(EventSystem.current);
        ExecuteEvents.Execute(button.gameObject, data, ExecuteEvents.submitHandler);
    }
}
