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
    
    private string soundCheck = null;

    public int hudaID 
    { 
        get { return _hudaID; } 
    }

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
        soundCheck = soundPath;
 
    }

    public AudioClip Getsound()
    {
        return Resources.Load<AudioClip>(soundCheck);
    }

    public void Setjin(int maisuu)
    {
        if (maisuu > 22)
        {
            _jin = true;
        }
        else
        {
            _jin = false;
        }
    }
}