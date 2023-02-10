using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Effects : MonoBehaviour
{
    [SerializeField]
    private GameObject obj17 = default;

    [SerializeField]
    private GameObject crane = default;

    [SerializeField]
    private Animator craneAnime = default;

    public IEnumerator ShowEffect(int id)
    {
        //「つ」のとき
        if (true) // id == 17
        {
            Debug.Log("aa 「つ」をとった！！");
            obj17.SetActive(true);
            obj17.transform.DORotate(new Vector3(0, 0, 360), 1f);
            var image = obj17.gameObject.transform.Find("札").GetComponent<Image>();
            image.DOFade(255f, 1f)
                .OnComplete(PlayFlyAnime);
            yield return new WaitForSeconds(4);
            FadeOut();
        }
    }

    private void PlayFlyAnime()
    {
        crane.SetActive(true);
        craneAnime.Play("Fly");
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void FadeOut()
    {
        var image = obj17.gameObject.transform.Find("札").GetComponent<Image>();
        image.DOFade(0f, 0.5f).OnComplete(() => obj17.SetActive(false)); ;
    }
}
