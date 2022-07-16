using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField]
    private LiquidVolumeAnimator Nivel;
    float n = 0;
    // Start is called before the first frame update
    void Start()
    {
        Nivel = GetComponent<LiquidVolumeAnimator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        n = Nivel.level;
        Debug.Log(n);
    }
}
