using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarutaHuda : MonoBehaviour
{
    [SerializeField] private Material _material = null;
    [SerializeField] private Material _myMaterial = null;
    [SerializeField] private MeshRenderer _cubeA = null;
    [SerializeField] private int _hudaID = 0;
    [SerializeField] private bool _jin;
    private string _soundCheck = null;

    public int hudaID 
    { 
        get { return _hudaID; } 
    }

<<<<<<< HEAD
    [SerializeField] bool jin;
    private string soundCheck = null;
    //public int hudaID{get;set;} 
    public int HudaID => _hudaID;
=======
>>>>>>> develop
    public bool Jin

    {
        get { return _jin; }
    }
    
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetHudaID(int hudaID)
    {
        
        _hudaID = hudaID;
        SetTexture(KarutaSystem.instance.GetTexture(_hudaID));
        SetSound(KarutaSystem.instance.GetSound(_hudaID)); 
    }

    void SetTexture(Texture texture)
    {
        if (_myMaterial == null)
        {
            _myMaterial = GameObject.Instantiate<Material>(_material);
            _cubeA.material = _myMaterial;
        }

        _myMaterial.mainTexture = texture;
    }
    void SetSound(string soundPath)
    {
        _soundCheck = soundPath;
 
    }

    public AudioClip Getsound()
    {
        return Resources.Load<AudioClip>(_soundCheck);
    }
<<<<<<< HEAD
    public void Setjin(int maisuu,int hudaAmountjin)
=======

    public void Setjin(int maisuu)
>>>>>>> develop
    {
        if (maisuu >= hudaAmountjin/2)
        {
            _jin = true;
        }
        else
        {
            _jin = false;
        }
    }
}