
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarutaSystem : MonoBehaviour
{
    List<KarutaHuda> _KarutaList = new List<KarutaHuda>();

    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        float z = 0f;
        
        for (int maisuu = 0; maisuu <= 43; maisuu = maisuu + 1)
        {
            KarutaHuda newObj = GameObject.Instantiate<KarutaHuda>(_KarutaHudaPrehub);
            newObj.transform.localPosition = new Vector3(x, 0f, z);
            _KarutaList.Add(newObj);
            x += 1;
            if (maisuu == 7)
            {
                x = 0;
                z = 76f / 63f;
            }
            if (maisuu == 14)
            {
                x = 0;
                z += 76f / 63f;
            }
            else if (maisuu == 21)
            {
                x = 0;
                z = 152f / 63f;
            }
            else if (maisuu == 28)
            {
                x = 0;
                z = 248f / 63f;
            }
            else if (maisuu == 35)
            {
                x = -1;
                z = 324f / 63f;
            }
            else
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
        