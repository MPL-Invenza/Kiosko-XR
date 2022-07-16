using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeValues : MonoBehaviour
{

    public static float Normalize(Vector2 inputRange, float inputValue, Vector2 outputRange)
    {
        float outputValue=0;
        float middleValue = (inputRange.x + inputRange.y)/2;
        outputValue=((inputRange.x - inputValue) / (middleValue-inputRange.x))+1;
        outputValue = outputValue * -1;
        if (outputValue>1)
            outputValue = 1;
        else if(outputValue<-1)
                outputValue = -1;

        return Mathf.Clamp(outputValue, outputRange.x, outputRange.y);
    }
}
