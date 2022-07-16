using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CardDollyCartManager : MonoBehaviour
{
    public CinemachineSmoothPath path;
    private int scrollDirection;
    private int scrollDirectionAux = 0;
    public bool eventInvoked { get; set; }
    private byte previousChildCount = 0;
    private bool listUpdated;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDollyCartsPosition();
        previousChildCount = ((byte)transform.childCount);
    }

    private void Update()
    {
        if (transform.childCount != previousChildCount)
        {
            UpdateDollyCartsPosition();
            previousChildCount = ((byte)transform.childCount);
        }
    }

    public void UpdateDollyCartsPosition()
    {
        for (byte i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CinemachineDollyCart>().m_Path == null)
            {
                transform.GetChild(i).GetComponent<CinemachineDollyCart>().m_Path = path;
            }
            if ((i + 1) <= path.m_Waypoints.Length - 1)
            {
                transform.GetChild(i).GetComponent<CinemachineDollyCart>().m_Position = i + 1;
            }
            else
            {
                transform.GetChild(i).GetComponent<CinemachineDollyCart>().m_Position = path.m_Waypoints.Length - 1;
            }

        }
    }

    public void GetSliderValue(Vector2 values)
    {
        if (eventInvoked)// scrollDirection != scrollDirectionAux)
        {
            if (values.x == 0)
                scrollDirection = 1;
            else scrollDirection = -1;

            print(values.x);
            if (scrollDirection == -1)
            {
                for (int i = (transform.childCount - 1); i >= 0; i--)
                {
                    CinemachineDollyCart child = transform.GetChild(i).GetComponent<CinemachineDollyCart>();

                    //Si este es el ultimo hijo de la jerarquía y se encuentra en la última posición visible
                    if (i == (transform.childCount - 1) && child.m_Position == path.m_Waypoints.Length - 2)
                    {
                        //Salga del ciclo for
                        break;
                    }
                    else
                    {
                        //Posicion mayor al punto visible
                        if (child.m_Position > path.m_Waypoints.Length - 2) //Punto visble
                        {
                            if (transform.GetChild(i - 1).GetComponent<CinemachineDollyCart>().m_Position == path.m_Waypoints.Length - 2) //Punto visble
                            {
                                child.m_Position -= 1;
                            }
                        }
                        else
                        {
                            child.m_Position -= 1;
                        }
                    }

                }
            }
            else if (scrollDirection == 1)
            {
                for (byte i = 0; i < transform.childCount; i++)
                {
                    CinemachineDollyCart child = transform.GetChild(i).GetComponent<CinemachineDollyCart>();

                    if (i == 0 && child.m_Position == 1)
                    {
                        break;
                    }
                    else
                    {
                        //Posicion mayor al punto visible
                        if (child.m_Position < 1) //Punto visble
                        {
                            if (transform.GetChild(i + 1).GetComponent<CinemachineDollyCart>().m_Position == 1) //Punto visble
                            {
                                child.m_Position += 1;
                            }
                        }
                        else
                        {
                            child.m_Position += 1;
                        }
                    }

                }
            }
            eventInvoked = false;
        }

    }

    public void GetMovementDirection(int direction)
    {

        if (direction == -1)
        {
            for (int i = (transform.childCount - 1); i >= 0; i--)
            {
                CinemachineDollyCart child = transform.GetChild(i).GetComponent<CinemachineDollyCart>();

                //Si este es el ultimo hijo de la jerarquía y se encuentra en la última posición visible
                if (i == (transform.childCount - 1) && child.m_Position <= path.m_Waypoints.Length - 2)
                {
                    //Salga del ciclo for
                    break;
                }
                else
                {
                    //Posicion mayor al punto visible
                    if (child.m_Position > path.m_Waypoints.Length - 2) //Punto visble
                    {
                        if (transform.GetChild(i - 1).GetComponent<CinemachineDollyCart>().m_Position == path.m_Waypoints.Length - 2) //Punto visble
                        {
                            child.m_Position -= 1;
                        }
                    }
                    else
                    {
                        child.m_Position -= 1;
                    }
                }

            }
        }
        else if (direction == 1)
        {
            for (byte i = 0; i < transform.childCount; i++)
            {
                CinemachineDollyCart child = transform.GetChild(i).GetComponent<CinemachineDollyCart>();

                if (i == 0 && child.m_Position == 1)
                {
                    break;
                }
                else
                {
                    //Posicion mayor al punto visible
                    if (child.m_Position < 1) //Punto visble
                    {
                        if (transform.GetChild(i + 1).GetComponent<CinemachineDollyCart>().m_Position == 1) //Punto visble
                        {
                            child.m_Position += 1;
                        }
                    }
                    else
                    {
                        child.m_Position += 1;
                    }
                }

            }
        }

    }
}
