using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class KarutaSystem : MonoBehaviour
{
   
        
        List<Texture> _textureList = new List<Texture>();
        List<KarutaHuda> _KarutaList = new List<KarutaHuda>();
        List<int> _numberList = new List<int>();

    List<string> _voiceList = new List<string>();
    //↑数値を取り出す用
    
   

    KarutaHuda _KarutaHudaPrehub = null;

    public static KarutaSystem instance = null;
    public KarutaSystem(KarutaHuda karutaHuda) {
        _KarutaHudaPrehub = karutaHuda;
    }
    // Start is called before the first frame update
    public void Initialize()
    {
        if (BotuPhotonScript.botuPhotonScript.isConnected)
        {
            BotuPhotonScript.botuPhotonScript.GetRoom();
            int seed = (BotuPhotonScript.botuPhotonScript.GetRoom().CustomProperties["seed"] is int value) ? value : 0;
            Random.InitState(seed);
        }
        instance = this;
        for (int i = 0; i < 44; i++)
        {
            _textureList.Add(Resources.Load<Texture>(string.Format("Texture/Karuta/{0}", i)));
            _numberList.Add(i);
            _voiceList.Add(string.Format("Sound/Karuta/{0}", i));
            KarutaHuda newObj = GameObject.Instantiate<KarutaHuda>(_KarutaHudaPrehub);
            newObj.SetHudaID(i);

            _KarutaList.Add(newObj);
        }
        List<Texture> _textureListCopy = new List<Texture>(_textureList);

        //↑は正解の札の参照用
        for (int i = _textureList.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            var temp = _KarutaList[i];
            _KarutaList[i] = _KarutaList[j];
            _KarutaList[j] = temp;

            var voiceTemp = _voiceList[i];
            _voiceList[i] = _voiceList[j];
            _voiceList[j] = voiceTemp;
        }
        //↑シャッフル


        float x = 2;
        float z = 0f;

        for (int maisuu = 0; maisuu <= 43; maisuu++)
        {
            _KarutaList[maisuu].transform.localPosition = new Vector3(x, 0f, z);
            _KarutaList[maisuu].name = maisuu.ToString();

            _KarutaList[maisuu].Setjin(maisuu);

            x += 0.1f;
            if (maisuu == 7)
            {
                x = 2f;
                z = 76f / 630f;
            }
            if (maisuu == 14)
            {
                x = 2;
                z = 152f / 630f;
            }
            else if (maisuu == 21)
            {
                x = 2;
                z = 248f / 630f;

            }
            else if (maisuu == 28)
            {
                x = 2;
                z = 324f / 630f;
            }
            else if (maisuu == 35)
            {
                x = 1.9f;
                z = 400f / 630f;
            }
            else
            {

            }

            if (22 <= maisuu)
            {
                _KarutaList[maisuu].transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
        for (int i = _textureList.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            var temp = _textureList[i];
            _textureList[i] = _textureList[j];
            _textureList[j] = temp;
        }

        for (int i = _numberList.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            var temp = _numberList[i];
            _numberList[i] = _numberList[j];
            _numberList[j] = temp;
        }

        for (int maisuu = 0; maisuu <= 43; maisuu++)
        {
            Texture Correct = _textureListCopy[_numberList[maisuu]];
            //Debug.Log(_numberList[maisuu]);
            Debug.Log(_KarutaList[_numberList[maisuu]]);
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
    public string GetSound(int hudaID)
    {
        return _voiceList[hudaID];
    }

    public List<KarutaHuda> GetKarutaList()
    {
        return _KarutaList;
    }
   
    public List<int> GetnumberList()
    {
        return _numberList;
        
    }

    }

        