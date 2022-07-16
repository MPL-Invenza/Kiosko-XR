using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XRHandleEventManager : MonoBehaviour
{
    public XRHandleObject[] xRScripts;


    public void ActivateScript(int scriptNumber)
    {
        xRScripts[scriptNumber].enabled = true;
    }

    public void DeactivateScript(int scriptNumber)
    {
        xRScripts[scriptNumber].enabled = false;
    }

    public void DeactivateOnLowerEvent(int scriptNumber)
    {
        for (byte i = 0; i < xRScripts[scriptNumber].onLowerLimitReached.GetPersistentEventCount(); i++)
        {
            xRScripts[scriptNumber].onLowerLimitReached.SetPersistentListenerState(i, UnityEventCallState.Off);
        }
    }

    public void DeactivateOnUpperEvent(int scriptNumber)
    {
        for (byte i = 0; i < xRScripts[scriptNumber].onUpperLimitReached.GetPersistentEventCount(); i++)
        {
            xRScripts[scriptNumber].onLowerLimitReached.SetPersistentListenerState(i, UnityEventCallState.Off);
        }
    }

    public void ActivateOnLowerEvent(int scriptNumber)
    {
        for (byte i = 0; i < xRScripts[scriptNumber].onLowerLimitReached.GetPersistentEventCount(); i++)
        {
            xRScripts[scriptNumber].onLowerLimitReached.SetPersistentListenerState(i, UnityEventCallState.RuntimeOnly);
        }
    }

    public void ActivateOnUpperEvent(int scriptNumber)
    {
        for (byte i = 0; i < xRScripts[scriptNumber].onUpperLimitReached.GetPersistentEventCount(); i++)
        {
            xRScripts[scriptNumber].onLowerLimitReached.SetPersistentListenerState(i, UnityEventCallState.RuntimeOnly);
        }
    }
}
