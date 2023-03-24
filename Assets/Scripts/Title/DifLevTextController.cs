using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifLevTextController : MonoBehaviour
{
    public static DifLevTextController difLevTextController ;

    public void Awake()
    {
        if(difLevTextController == null)
        {
            difLevTextController = this;
        }
    } 

    public void TextMove(float difLevSign)
    {
        Transform signText = this.transform;
        Vector3 signPosi =signText.position;
        signPosi.y = difLevSign;
        signText.position = signPosi;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
