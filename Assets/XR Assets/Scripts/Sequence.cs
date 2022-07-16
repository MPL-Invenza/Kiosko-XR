using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sequence : MonoBehaviour
{
    public Transform dummyColliders;
    public bool[] steps;
    public UnityEvent onCorrectStep;
    public UnityEvent onIncorrectStep;
    public UnityEvent OnStepUndid;
    public bool[] stepEventInvoked;

    // Start is called before the first frame update
    private void Start()
    {
        stepEventInvoked = new bool[steps.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectDummyCollider()
    {
        onIncorrectStep.Invoke();
    }

    /// <summary>
    /// Change the status of the step to true and deactivate the dummy collider of the next step
    /// </summary>
    /// <param name="step"></param>
    public void CompleteStep(int step)
    {
        if (this.gameObject.activeSelf)
        {
            steps[step] = true;
            if (!stepEventInvoked[step])
            {
                onCorrectStep.Invoke();
                stepEventInvoked[step] = true;
            }
            if (step < steps.Length - 1)
            {
                ManageDummyCollider(step + 1, false);
            }
        }
    }

    /// <summary>
    /// Change the status of the step to true and activate the dummy collider of the next steps
    /// </summary>
    /// <param name="step"></param>
    public void UndoStep(int step)
    {
        if (this.gameObject.activeSelf)
        {
            steps[step] = false;
            if (stepEventInvoked[step])
            {
                OnStepUndid.Invoke();
                stepEventInvoked[step] = false;
            }
            if (step < steps.Length - 1)
            {
                ManageDummyCollider(step + 1, true);
            }
        }
        //steps[step] = false;
        ////for (int i = step + 1; i < steps.Length; i++)
        ////{
        ////    ManageDummyCollider(i, true);
        ////}
        //if (step < steps.Length - 1)
        //{
        //    ManageDummyCollider(step + 1, true);
        //}
    }

    /// <summary>
    /// Alternate the step status, used for example with push buttons
    /// </summary>
    /// <param name="step"></param>
    public void ChangeStepStatus(int step)
    {
        steps[step] = !steps[step];
        if (!steps[step])
        {
            UndoStep(step);
        }
        else
        {
            CompleteStep(step);
        }

        //if (step < steps.Length - 1)
        //{
        //    ManageDummyCollider(step + 1, true);
        //}
    }


    /// <summary>
    /// Checks the dummy collider that was selected
    /// </summary>
    /// <param name="step"></param>
    public void CheckStep(int step)
    {
        if (step > 0)
        {
            if (steps[step - 1])
            {
                if (!steps[step])
                {
                    steps[step] = true;
                    dummyColliders.GetChild(step).gameObject.SetActive(false);
                    onCorrectStep.Invoke();
                }
            }
            else
            {
                onIncorrectStep.Invoke();
            }
        }
        else
        {
            if (!steps[0])
            {
                steps[step] = true;
                onCorrectStep.Invoke();
            }
        }
    }

    /// <summary>
    /// Activates or deactivates the selected dummy collider
    /// </summary>
    /// <param name="dummy"></param>
    /// <param name="activate"></param>
    public void ManageDummyCollider(int dummy, bool activate)
    {
        dummyColliders.GetChild(dummy).gameObject.SetActive(activate);

        //dummyColliders.GetChild(dummy).GetComponent<MeshRenderer>().enabled = activate;
        //dummyColliders.GetChild(dummy).GetComponent<BoxCollider>().enabled = activate;
    }
}
