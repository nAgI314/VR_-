using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class KarutaSystem : MonoBehaviour
{
   
        
        List<Texture> _textureList = new List<Texture>();
        List<KarutaHuda> _KarutaList = new List<KarutaHuda>();
   
    [SerializeField] KarutaHuda _KarutaHudaPrehub = null;
        public static KarutaSystem instance = null;
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            for (int i = 0; i < 44; i++)
            { _textureList.Add(Resources.Load<Texture>(string.Format("Texture/Karuta/{0}", i))); }
        for (int i = _textureList.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            var temp = _textureList[i]; 
            _textureList[i] = _textureList[j]; 
            _textureList[j] = temp; 
        }


        
        int x = 0;
           float z = 0f;

            for (int maisuu = 0; maisuu <= 43; maisuu++)
            {
                KarutaHuda newObj = GameObject.Instantiate<KarutaHuda>(_KarutaHudaPrehub);
                newObj.transform.localPosition = new Vector3(x, 0f, z);
                
                newObj.SetHudaID(maisuu);
           
            



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
                    z = 152f / 63f;
                }
                else if (maisuu == 21)
                {
                    x = 0;
                    z = 248f / 63f;
                
                }
                else if (maisuu == 28)
                {
                    x = 0;
                    z = 324f / 63f;
                }
                else if (maisuu == 35)
                {
                    x = -1;
                    z = 400f / 63f;
                }
                else
                {

                }

                if (22 <= maisuu)
                {
                    newObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

        }
        for (int i = _textureList.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            var temp = _textureList[i];
            _textureList[i] = _textureList[j];
            _textureList[j] = temp;
        }
        var waitTask = Task.Delay(5000);
        waitTask.Wait();

        int Correct;
        for (int maisuu = 0; maisuu <= 43; maisuu++)
        {
          
            System.Console.WriteLine(_textureList[maisuu]);
            



            var waitTask2 = Task.Delay(10000);
            waitTask2.Wait();
        }



    }

        // Update is called once per frame
        void Update()
        {
        }
        public Texture GetTexture(int hudaID)
        {
            return _textureList[hudaID];

        }

    }

        