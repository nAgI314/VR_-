using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateKarutas : MonoBehaviour
{
    public GameObject kalta;

    // Start is called before the first frame update
    void Start()
    {
        CreateKaruta();
    }

    void CreateKaruta()
    {
        Instantiate(kalta);
    }
}
