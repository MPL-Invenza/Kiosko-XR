using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator objectAnimator;


    public void animationStarter(string argument)
    {
        objectAnimator.SetBool(argument,true);
    }
}
