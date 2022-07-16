using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StepCounter : MonoBehaviour
{

    public TMP_Text counterText;
    //public TMP_Text messageText;
    public byte totalAmount;
    [HideInInspector]
    public byte currentAmount;
    public UnityEvent onTotalAmountReached;

    private void Start()
    {
        Invoke("ShowGUI", 2.5f);
    }

    //private void ShowGUI()
    //{
    //    counterText.enabled = true;
    //    messageText.enabled = true;
    //    counterText.text = $"{currentAmount}/{totalAmount}";
    //}

    public void UpdateText(bool correct)
    {
        if (correct)
        {
            currentAmount++;
            counterText.text = $"{currentAmount}/{totalAmount}";
            //messageText.text = "Bien hecho!";
        }
        else
        {
            currentAmount--;
            counterText.text = $"{currentAmount}/{totalAmount}";
            //messageText.text = "Incorrecto, por favor ubícalo en el lugar indicado";
        }

        if (currentAmount == totalAmount)
        {
            //messageText.text = "Actividad Finalizada";
            onTotalAmountReached.Invoke();
        }
    }
}
