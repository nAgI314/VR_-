using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShortVersionEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objs = default;

    [SerializeField]
    private Animator effectsAnime = default;

    [SerializeField]
    private Animation[] animations = default;

    private GameObject craneObj = default;
    private GameObject darumaObj = default;

    private void Start()
    {
        // debug
        // ShowEffect(1);
        craneObj = objs[0].transform.Find("Crane").gameObject;
        darumaObj = objs[1].transform.Find("Daruma").gameObject;
    }

    public void ShowEffect(int num)
    {

        // つ
        if (num == 0)
        {
            effectsAnime.Play("Effect_つ");

            craneObj.transform.DOLocalMove(new Vector3(-125, 158, -270), 1.66f);
        }

        // え
        if (num == 1)
        {
            effectsAnime.Play("Effect_え");

            darumaObj.transform.DOLocalMove(new Vector3(3f, -345f, -74), 0.8f)
                .OnComplete(() => darumaObj.transform.DOPunchScale(Vector3.one * 150f, 1f, 5, 1.5f));
        }

        // す
        if (num == 2)
        {
            effectsAnime.Play("Effect_す");
        }

        // た
        if (num == 3)
        {
            effectsAnime.Play("Effect_た");
        }

        // ね
        if (num == 4)
        {
            effectsAnime.Play("Effect_ね");
        }

        // ひ
        if (num == 5)
        {
            effectsAnime.Play("Effect_ひ");
        }

    }
}

