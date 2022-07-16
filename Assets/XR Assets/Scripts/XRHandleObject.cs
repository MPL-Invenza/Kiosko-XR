using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MovementType
{
    Translate,
    Rotate
}

public enum Axis
{
    X, Y, Z, mX, mY, mZ
}
public class XRHandleObject : MonoBehaviour
{
    public float movementSpeed;

    public MovementType movementType;
    [SerializeField]
    private Axis movementAxis;
    [SerializeField]
    private Axis comparisonAxis;
    public Axis sourceMovementAxis;
    [HideInInspector]
    public float movementValue = 0;

    [SerializeField]
    private bool enableLimits = true;

    public Vector2 limits;
    private Vector3 movementAxisVector;

    [HideInInspector]
    public bool move = false;

    private bool movementAxisInitialized;

    public Transform movingObject;
    [HideInInspector]
    private float comparisonValue;
    [HideInInspector]
    public bool upperLimit, lowerLimit;

    private Vector3 initialValue;
    private Vector3 currentValue;

    public bool resetMovementVariable { get; set; } = true;

    //Events
    public UnityEvent onUpperLimitReached;
    public UnityEvent onLowerLimitReached;
    private bool upperLimitEventFlag = false;
    private bool lowerLimitEventFlag = false;

    private void Start()
    {
        if (!movingObject)
        {
            movingObject = transform;
        }

        switch (movementType)
        {
            case MovementType.Translate:
                initialValue = movingObject.localPosition;
                break;
            case MovementType.Rotate:
                initialValue = movingObject.localEulerAngles;
                break;
        }
    }

    private void Update()
    {
        if (!movementAxisInitialized)
        {
            InitializeMovementAxis();
            movementAxisInitialized = true;
        }

        switch (movementType)
        {
            case MovementType.Translate:
                currentValue = movingObject.localPosition;
                break;
            case MovementType.Rotate:
                currentValue = movingObject.localEulerAngles;
                break;
        }

    }

    /// <summary>
    /// Initializes the movement axis vector
    /// </summary>
    private void InitializeMovementAxis()
    {
        switch (movementAxis)
        {
            case Axis.X:
                movementAxisVector = Vector3.right;
                break;
            case Axis.Y:
                movementAxisVector = Vector3.up;
                break;
            case Axis.Z:
                movementAxisVector = Vector3.forward;
                break;
            case Axis.mX:
                movementAxisVector = -Vector3.right;
                break;
            case Axis.mY:
                movementAxisVector = -Vector3.up;
                break;
            case Axis.mZ:
                movementAxisVector = -Vector3.forward;
                break;
        }
    }

    public void Handle(float value)
    {
        movementValue = value;

        switch (movementType)
        {
            case MovementType.Translate:
                Translate(movementValue);
                break;
            case MovementType.Rotate:
                Rotate(movementValue);
                break;
        }
    }

    public void Translate(float movementValue)
    {
        //Adds the movementValue to the selected axis variable (position) and compares it
        switch (comparisonAxis)
        {
            case Axis.X:
                comparisonValue = movingObject.localPosition.x + movementValue * movementSpeed;
                break;
            case Axis.Y:
                comparisonValue = movingObject.localPosition.y + movementValue * movementSpeed;
                break;
            case Axis.Z:
                comparisonValue = movingObject.localPosition.z + movementValue * movementSpeed;
                break;
            case Axis.mX:
                comparisonValue = movingObject.localPosition.x + movementValue * movementSpeed;
                break;
            case Axis.mY:
                comparisonValue = movingObject.localPosition.y + movementValue * movementSpeed;
                break;
            case Axis.mZ:
                comparisonValue = movingObject.localPosition.z + movementValue * movementSpeed;
                break;
        }

        if (enableLimits)
        {
            if (comparisonValue > limits.x && comparisonValue < limits.y)
                move = true;
            else
                move = false;

            if (comparisonValue >= limits.y)
            {
                upperLimit = true;
                SetMovementVariable(limits.y);

                //Call onUpperLimitReached event once
                if (!upperLimitEventFlag)
                {
                    onUpperLimitReached.Invoke();
                    upperLimitEventFlag = true;
                }
            }
            else
            {
                upperLimit = false;
                upperLimitEventFlag = false;
            }

            if (comparisonValue <= limits.x)
            {
                lowerLimit = true;
                SetMovementVariable(limits.x);

                //Call onLowerLimitReached event once
                if (!lowerLimitEventFlag)
                {
                    onLowerLimitReached.Invoke();
                    lowerLimitEventFlag = true;
                }

            }
            else
            {
                lowerLimit = false;
                lowerLimitEventFlag = false;
            }
        }
        else
            move = true;

        if (move)
            movingObject.Translate(movementValue * movementSpeed * movementAxisVector, Space.Self);

    }

    public void Rotate(float movementValue)
    {
        // print($"{movementAxis}: {movementValue}");
        //Adds the movementValue to the selected axis variable (rotation) and compares it
        switch (comparisonAxis)
        {
            case Axis.X:
                comparisonValue = movingObject.localEulerAngles.x + movementValue * movementSpeed;
                break;
            case Axis.Y:
                comparisonValue = movingObject.localEulerAngles.y + movementValue * movementSpeed;
                break;
            case Axis.Z:
                comparisonValue = movingObject.localEulerAngles.z + movementValue * movementSpeed;
                break;
            case Axis.mX:
                comparisonValue = movingObject.localEulerAngles.x + movementValue * movementSpeed;
                break;
            case Axis.mY:
                comparisonValue = movingObject.localEulerAngles.y + movementValue * movementSpeed;
                break;
            case Axis.mZ:
                comparisonValue = movingObject.localEulerAngles.z + movementValue * movementSpeed;
                break;
        }
        if (comparisonValue < 0)
            comparisonValue = 360.0f + comparisonValue;

        if (enableLimits)
        {
            if (comparisonValue > limits.x && comparisonValue < limits.y)
                move = true;
            else
                move = false;


            if (comparisonValue >= limits.y)
            {
                upperLimit = true;
                SetMovementVariable(limits.y);

                //Call onUpperLimitReached event once
                if (!upperLimitEventFlag)
                {
                    onUpperLimitReached.Invoke();
                    upperLimitEventFlag = true;
                }
            }
            else
            {
                upperLimit = false;
                upperLimitEventFlag = false;
            }


            if (comparisonValue <= limits.x)
            {
                lowerLimit = true;
                SetMovementVariable(limits.x);

                //Call onLowerLimitReached event once
                if (!lowerLimitEventFlag)
                {
                    onLowerLimitReached.Invoke();
                    lowerLimitEventFlag = true;
                }

            }
            else
            {
                lowerLimit = false;
                lowerLimitEventFlag = false;
            }

        }
        else
            move = true;

        if (move)
            movingObject.Rotate(movementAxisVector, movementValue * movementSpeed);
    }

    public void ResetMultipleScripts()
    {
        foreach (XRHandleObject item in GetComponents<XRHandleObject>())
        {
            item.Reset();
        }
    }


    public void Reset()
    {

        if (resetMovementVariable)
        {
            switch (movementType)
            {
                case MovementType.Translate:
                    movingObject.localPosition = initialValue;
                    break;
                case MovementType.Rotate:
                    movingObject.localEulerAngles = initialValue;
                    break;
            }
        }

        //Reset events flags
        lowerLimitEventFlag = true;
        upperLimitEventFlag = true;
    }


    public void SetMovementVariable(float value)
    {
        switch (movementType)
        {
            case MovementType.Translate:
                {
                    switch (movementAxis)
                    {
                        case Axis.X:
                            movingObject.localPosition = new Vector3(value, currentValue.y, currentValue.z);
                            break;
                        case Axis.Y:
                            movingObject.localPosition = new Vector3(currentValue.x, value, currentValue.z);
                            break;
                        case Axis.Z:
                            movingObject.localPosition = new Vector3(currentValue.x, currentValue.y, value);
                            break;
                        case Axis.mX:
                            movingObject.localPosition = new Vector3(value, currentValue.y, currentValue.z);
                            break;
                        case Axis.mY:
                            movingObject.localPosition = new Vector3(currentValue.y, value, currentValue.z);
                            break;
                        case Axis.mZ:
                            movingObject.localPosition = new Vector3(currentValue.x, currentValue.y, value);
                            break;
                    }
                }
                break;
            case MovementType.Rotate:
                {
                    switch (movementAxis)
                    {
                        case Axis.X:
                            movingObject.localEulerAngles = new Vector3(value, currentValue.y, currentValue.z);
                            break;
                        case Axis.Y:
                            movingObject.localEulerAngles = new Vector3(currentValue.x, value, currentValue.z);
                            break;
                        case Axis.Z:
                            movingObject.localEulerAngles = new Vector3(currentValue.x, currentValue.y, value);
                            break;
                        case Axis.mX:
                            movingObject.localEulerAngles = new Vector3(value, currentValue.y, currentValue.z);
                            break;
                        case Axis.mY:
                            movingObject.localEulerAngles = new Vector3(currentValue.y, value, currentValue.z);
                            break;
                        case Axis.mZ:
                            movingObject.localEulerAngles = new Vector3(currentValue.x, currentValue.y, value);
                            break;
                    }
                }
                break;
        }

    }

    public float GetComparisonValue()
    {
        return comparisonValue;
    }

}

