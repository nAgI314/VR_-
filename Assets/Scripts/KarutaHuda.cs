using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarutaHuda : MonoBehaviour
{

    [SerializeField] Material _material = null;
    Material _myMaterial = null;
    [SerializeField] MeshRenderer _cubeA = null;
    // Start is called before the first frame update

    [SerializeField] private int _hudaID = 0;
    private string soundCheck = null;
    public int hudaID  { get; set; }
    
    void Start()
    {

    }

    // Update is called once per frame
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
}