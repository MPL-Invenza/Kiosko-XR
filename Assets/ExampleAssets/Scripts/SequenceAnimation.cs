using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SequenceAnimation : MonoBehaviour
{
    public UnityEvent OnButtonPress;
    public UnityEvent OnButtonRelease;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PressButton(ProvisionalSimpleInteractable xrSimple)
    {
        StartCoroutine("PressButtonCoroutine", xrSimple);
    }

    public void SetTimer(float timerValue)
    {
        timer = timerValue;
    }

    public IEnumerator PressButtonCoroutine(ProvisionalSimpleInteractable xrSimple)
    {
        //xrSimple.onSelectEntered.Invoke();
        xrSimple.onSelectEntered.Invoke();

        yield return new WaitForSecondsRealtime(timer);
        xrSimple.onSelectCanceled.Invoke();
    }
}
