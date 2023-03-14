using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Billboard : MonoBehaviour
{
    private static readonly Vector3 AdditionalEuler = new Vector3(0f, 180f, 0f);
    private Transform cam = default;

    private void Start()
    {
        cam = Camera.main.gameObject.transform;
    }

    private void Update()
    {
        cam = Camera.main.gameObject.transform;
        transform.LookAt(cam);

        // îΩì]ñhé~
        transform.rotation *= Quaternion.Euler(AdditionalEuler);
    }
}