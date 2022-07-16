// using System.Collections;
// using System.Collections.Generic;
// using Pvr_UnitySDKAPI;
// using UnityEngine;

// public enum TouchPadButton{
//     Center,
//     Up,
//     Down,
//     Left,
//     Right
// }
// public class InteraccionRayo : MonoBehaviour
// {
//     //Controller(left hand / right hand)
//     public GameObject controller0;
//     public GameObject controller1;
//     [HideInInspector]
//     public GameObject currentController;
//     [HideInInspector]
//     public byte mainHand;

//     //Ray
//     private Ray ray;
//     private RaycastHit hit;
//     private Transform lastHit;
//     private Transform currentHit;
//     private GameObject rayLine;
//     private GameObject dot;
//     private GameObject start;
//     public float distance=3.0f;

//     [HideInInspector]
//     public bool interactuando;
//     private bool rotando;
//     private bool arrastrando;
//     private bool apuntando;

//     private bool isHasController = false;

//     private float disX, disY, disZ;
//     private float rotX, rotY, rotZ;
//     private Transform objeto;
//     [HideInInspector]
//     public string objetoNombre;
//     private Vector3 currentPos, lastPos;
//     private Vector3 currentAng, lastAng;
//     private int cont = 0;
//     private bool touchUp=true;
//     public bool entrenamiento;

//     private void Start()
//     {
//         ray = new Ray();
//         hit = new RaycastHit();

//         if (Pvr_UnitySDKManager.SDK.isHasController)
//         {
//             //Event subscription to evaluate controller state
//             Pvr_ControllerManager.PvrServiceStartSuccessEvent += ServiceStartSuccess;
//             Pvr_ControllerManager.SetControllerStateChangedEvent += ControllerStateListener;
//             isHasController = true;
// #if UNITY_EDITOR
//             ConfiguracionInicialEditor();
// #endif
//         }

//     }

//     private void OnDestroy()
//     {
//         if (isHasController)
//         {
//             //Desuscripción de eventos
//             Pvr_ControllerManager.PvrServiceStartSuccessEvent -= ServiceStartSuccess;
//             Pvr_ControllerManager.SetControllerStateChangedEvent -= ControllerStateListener;
//         }
//     }

//     // Update is called once per frame
//     private void Update()
//     {

//         //Determined whether the handle is connected
//         if (currentController!=null)
//         {
//             #region Raycast
//             //Rayo apunta con inclinación
//             if(!interactuando || arrastrando || entrenamiento)ray.direction = currentController.transform.forward - currentController.transform.up * 0.25f;
//             //Rayo apunta alineado con el forward del control
//             else ray.direction = currentController.transform.forward;

//             ray.origin = currentController.transform.Find("start").position;
//             //Rayo solo apunta a layer "Default" e ignora colliders Trigger
//             if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
//             {
//                 if (hit.transform.CompareTag("Interactable") && !interactuando)
//                 {
//                     currentHit = hit.transform;
//                     //Cambio color
//                     if (currentHit != null && lastHit != null && lastHit != currentHit)
//                     {
//                         if(lastHit.GetComponent<InteractableObject>())
//                             lastHit.GetComponent<InteractableObject>().CambiarColor(0);
//                     }
//                     else
//                     {
//                         if(currentHit.GetComponent<InteractableObject>())
//                             currentHit.GetComponent<InteractableObject>().CambiarColor(1);
//                     }


//                     dot.GetComponent<SpriteRenderer>().color = Color.red;

//                     apuntando=true;

//                     lastHit = hit.transform;
//                     dot.transform.position = hit.point;
//                 }
//                 else
//                 {
//                     apuntando = false;
//                     dot.GetComponent<SpriteRenderer>().color = Color.green;

//                     if(!arrastrando && !rotando)dot.transform.position = hit.point;
//                     if (lastHit != null)
//                     {
//                         //Cambio color
//                         if(lastHit.GetComponent<InteractableObject>())
//                             lastHit.GetComponent<InteractableObject>().CambiarColor(0);
//                         //Limpieza variables
//                         lastHit = null;
//                         currentHit = null;
//                     }

//                 }

//                 if (Pvr_ControllerManager.Instance.LengthAdaptiveRay)
//                 {
//                     //Dado que la distancia por defecto de dot es 3.3f
//                     //Su tamaño cambiará en proporción respecto a la distancia del objeto con el que colisiona
//                     float scale = 0.45f * dot.transform.localPosition.z / 3.3f;
//                     scale = Mathf.Clamp(scale, 0.178f, 0.5f);
//                     dot.transform.localScale = new Vector3(scale, scale, 1);
//                 }


//             }
//             else
//             {
//                 apuntando = false;
//                 if (lastHit != null)
//                 {
//                     //Cambio color
//                     if(lastHit.GetComponent<InteractableObject>())
//                         lastHit.GetComponent<InteractableObject>().CambiarColor(0);
//                     //Limpieza variables
//                     lastHit = null;
//                     currentHit = null;
//                 }
//                 dot.GetComponent<SpriteRenderer>().color = Color.green;
//             }

//             // rayLine.GetComponent<LineRenderer>().SetPosition(0,currentController.transform.TransformPoint(0, 0, 0.072f));
//             // rayLine.GetComponent<LineRenderer>().SetPosition(1, dot.transform.position);
//             // rayLine.GetComponent<LineRenderer>().startWidth = 0.005f;
//             #endregion



//             #region Controles

//             #region Trigger

//             if (apuntando && Input.GetMouseButtonDown(0) || Controller.UPvr_GetKeyDown(mainHand, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
//                 TriggerDown();

//             if (Input.GetMouseButtonUp(0) || Controller.UPvr_GetKeyUp(mainHand, Pvr_UnitySDKAPI.Pvr_KeyCode.TRIGGER))
//                 TriggerUp();
//             #endregion

//             #region Touchpad

//             // if (Input.GetKeyDown(KeyCode.Space) || Controller.UPvr_GetKeyDown(mainHand, Pvr_UnitySDKAPI.Pvr_KeyCode.TOUCHPAD))
//             //     TouchpadDown(TouchPadButton.Center);

//             // if (interactuando && touchUp && Input.GetKeyDown(KeyCode.UpArrow) || Controller.UPvr_GetTouchPadClick(mainHand) == TouchPadClick.ClickUp)
//             //     TouchpadDown(TouchPadButton.Up);

//             // if (interactuando && touchUp && Input.GetKeyDown(KeyCode.DownArrow) || Controller.UPvr_GetTouchPadClick(mainHand) == TouchPadClick.ClickDown)
//             //     TouchpadDown(TouchPadButton.Down);

//             // if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Controller.UPvr_GetTouchPadClick(mainHand) == TouchPadClick.No)
//             //     touchUp = true;

//             #endregion


//             #endregion

//             if (interactuando)
//             {
//                 dot.GetComponent<SpriteRenderer>().color = Color.red;
//                 if(arrastrando) Arrastrar(objeto.GetComponent<ArrastrarObjeto>().movimientoControl);
//                 if(rotando) Rotar();
//             }

//         }
//     }

//     private void ConfiguracionInicialEditor()
//     {
//         currentController = controller1;
//         controller0.SetActive(false);
//         mainHand = 1;
//         dot = currentController.transform.Find("dot").gameObject;
//         dot.SetActive(true);
//         //currentController.transform.Find("ray_alpha").gameObject.SetActive(true);
//         start = currentController.transform.Find("start").gameObject;
//         rayLine = currentController.transform.Find("ray_LengthAdaptive").gameObject;
//         rayLine.SetActive(true);
//     }

//     #region Funciones controles

//     private void TriggerDown()
//     {
//         if (currentHit.transform.GetComponent<AgarrarObjeto>())
//         {
//             interactuando = true;
//             objeto = currentHit.transform;
//             objetoNombre = objeto.name;
//             Agarrar();
//         }
//         else if (currentHit.GetComponent<Animator>())
//         {
//             // if (currentHit.TryGetComponent<AnimacionManager>(out AnimacionManager aM))
//             //     aM.PlayAnimation();
//             // if (currentHit.TryGetComponent<HotspotAnimaciones>(out HotspotAnimaciones hA))
//             // {
//             //     hA.MostrarElementosUI(true);
//             // }
//         }
//         else if (currentHit.GetComponent<ArrastrarObjeto>())
//         {
//             interactuando = true;
//             arrastrando = true;
//             objeto = currentHit.transform;
//         }
//         else if (currentHit.GetComponent<RotarObjeto>())
//         {
//             interactuando = true;
//             rotando = true;
//             objeto = currentHit.transform;
//         }
//         else
//         {
//             if (currentHit.parent.TryGetComponent<CambioPosicion>(out CambioPosicion cp))
//             {
//                 cp.Ocultar((byte)currentHit.transform.GetSiblingIndex());
//                 // GetComponent<MovimientoCamara>().RestaurarPosicion();
//                 // GetComponent<MovimientoControles>().RestaurarPosicion();
//                 transform.parent.parent.position=new Vector3(currentHit.position.x,transform.position.y,currentHit.position.z);
//                 transform.parent.parent.eulerAngles=new Vector3(currentHit.eulerAngles.x,currentHit.eulerAngles.y,currentHit.eulerAngles.z);
//             }
//         }
//     }

//     private void TriggerUp()
//     {
//         interactuando = false;
//         if (objeto!=null)
//         {
//             if (objeto.TryGetComponent<AgarrarObjeto>(out AgarrarObjeto aO))
//             {
//                 if (!aO.bloqueo)
//                 {
//                     objeto.GetComponent<AgarrarObjeto>().Soltar();
//                     objeto.gameObject.layer = 0;
//                 }
//             }
//             else if (objeto.TryGetComponent<ArrastrarObjeto>(out ArrastrarObjeto arO))
//             {

//             }
//         }

//         //Arrastrar
//         arrastrando = false;
//         lastPos = Vector3.zero;
//         objeto = null;
//         rotando = false;
//         lastAng = Vector3.zero;
//     }

//     private void TouchpadDown(TouchPadButton touchpad)
//     {
//         if (objeto.transform.TryGetComponent<AgarrarObjeto>(out AgarrarObjeto agarrarObj))
//         {
//             switch (touchpad)
//             {
//                 case TouchPadButton.Up:
//                     agarrarObj.MovimientoProfundidad(1);
//                     break;
//                 case TouchPadButton.Down:
//                     agarrarObj.MovimientoProfundidad(-1);
//                     break;
//                 case TouchPadButton.Center:
//                     //rotando=!rotando;
//                     break;
//             }
//             touchUp = false;
//         }
//     }

//     #endregion

// #region Funciones Acciones

//     private void Agarrar()
//     {
//         objeto = currentHit.transform;
//         objeto.GetComponent<AgarrarObjeto>().Agarrar(currentController.transform);

//         // if (objeto.TryGetComponent<ActivacionCable>(out ActivacionCable ac))
//         //     ac.ActivarCable();

//         lastHit.GetComponent<InteractableObject>().CambiarColor(0);
//         objeto.gameObject.layer = LayerMask.GetMask("Water");
//     }

//     private void Arrastrar(bool movimientoControl)
//     {
//         if (!movimientoControl)
//             currentPos = dot.transform.position;
//         else
//             currentPos = currentController.transform.localPosition;


//         if (lastPos != Vector3.zero)
//         {
//             disX = currentPos.x - lastPos.x;
//             disY = currentPos.y - lastPos.y;
//             disZ = currentPos.z - lastPos.z;
//         }

//         if (!movimientoControl)
//             lastPos = dot.transform.position;
//         else
//             lastPos = currentController.transform.localPosition;

//         if (objeto != null)
//         {
//             foreach (ArrastrarObjeto arrastrarScript in objeto.GetComponents<ArrastrarObjeto>())
//             {
//                 if (arrastrarScript.enabled)
//                 {
//                     arrastrarScript.MovimientoControl(disX, disY, disZ);
//                 }
//             }
//         }
//     }

//     private void Rotar()
//     {
//         currentAng = currentController.transform.localEulerAngles;

//         if (lastAng != Vector3.zero)
//         {
//             rotX = currentAng.x - lastAng.x;
//             rotY = currentAng.y - lastAng.y;
//             rotZ = currentAng.z - lastAng.z;
//         }
//         lastAng = currentController.transform.localEulerAngles;

//         if (objeto != null)
//             objeto.GetComponent<RotarObjeto>().GiroControl(rotX, rotY, rotZ);
//     }

//     #endregion

//     #region Eventos

//     private void ServiceStartSuccess()
//     {
//         if (Controller.UPvr_GetControllerState(0) == ControllerState.Connected ||
//             Controller.UPvr_GetControllerState(1) == ControllerState.Connected)
//         {
//             //HeadSetController.SetActive(false);
//         }
//         else
//         {
//             //HeadSetController.SetActive(true);
//         }
//         if (Controller.UPvr_GetMainHandNess() == 0)
//         {
//             currentController = controller0;
//             mainHand = 0;
//         }
//         else if (Controller.UPvr_GetMainHandNess() == 1)
//         {
//             currentController = controller1;
//             mainHand = 1;
//         }
//         dot = currentController.transform.Find("dot").gameObject;
//     }

//     private void ControllerStateListener(string data)
//     {

//         if (Controller.UPvr_GetControllerState(0) == ControllerState.Connected ||
//             Controller.UPvr_GetControllerState(1) == ControllerState.Connected)
//         {
//             //HeadSetController.SetActive(false);
//         }
//         else
//         {
//            //HeadSetController.SetActive(true);
//         }

//         if (Controller.UPvr_GetMainHandNess() == 0)
//         {
//             currentController = controller0;
//             mainHand = 0;
//         }
//         if (Controller.UPvr_GetMainHandNess() == 1)
//         {
//             currentController = controller1;
//             mainHand = 1;
//         }
//     }

//     #endregion

// }

