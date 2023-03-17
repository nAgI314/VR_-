using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShortVersionEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objs = default;

    private void Start()
    {
        // debug
        // StartCoroutine(ShowEffect(1));
    }

    public IEnumerator ShowEffect(int num)
    {
        objs[num].SetActive(true);
        var beforeImage = objs[num].transform.Find("before").GetComponent<Image>();
        var afterImage = objs[num].transform.Find("after").GetComponent<Image>();

        // つ（wait：5秒）
        if (num == 0)
        {
            var craneObj = objs[num].transform.Find("Crane").gameObject;
            craneObj.transform.DOLocalMove(new Vector3(-125, 158, -270), 1.66f);

            beforeImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(2f);
            beforeImage.DOFade(0f, 2f);
            afterImage.DOFade(1f, 2f);
             yield return new WaitForSeconds(2f);

            craneObj.SetActive(false);

            afterImage.DOFade(0f, 1f);
             yield return new WaitForSeconds(1f);
        }

        // え（wait：6.5秒）
        if (num == 1)
        {
            var darumaObj = objs[num].transform.Find("Daruma").gameObject;
            darumaObj.SetActive(true);

            darumaObj.transform.DOLocalMove(new Vector3(3f, -345f, -74), 1.2f)
                .OnComplete(() => darumaObj.transform.DOPunchScale(Vector3.one * 200f, 1f, 5, 1f));

            beforeImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(3f);

            darumaObj.transform.DOScale(new Vector3(800, 800, 800), 1f);
            darumaObj.transform.DOLocalMoveZ(300, 1f);
             yield return new WaitForSeconds(0.5f);

            beforeImage.DOFade(0f, 1f);
            afterImage.DOFade(1f, 1f);
            darumaObj.SetActive(false);
             yield return new WaitForSeconds(2f);
            
            afterImage.DOFade(0f, 1f);
             yield return new WaitForSeconds(1f);
        }

        // す（wait：5秒）
        if (num == 2)
        {
            beforeImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(1f);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), 1f);
             yield return new WaitForSeconds(1f);
            afterImage.transform.DOScale(new Vector3(1, 1, 1), 1f);
            afterImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(2f);

            afterImage.DOFade(0f, 1f);
             yield return new WaitForSeconds(1f);

        }

        // た（wait：5秒）
        if (num == 3)
        {
            beforeImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(1f);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), 1f);
             yield return new WaitForSeconds(1f);
            afterImage.transform.DOScale(new Vector3(1, 1, 1), 1f);
            afterImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(2f);

            afterImage.DOFade(0f, 1f);
             yield return new WaitForSeconds(1f);
        }

        // ね（wait：5.5秒）
        if (num == 4)
        {
            var beforeImage2 = objs[num].transform.Find("before2").GetComponent<Image>();

            beforeImage.DOFade(1f, 0.5f);
            beforeImage2.DOFade(1f, 0.5f);
             yield return new WaitForSeconds(1.5f);

            beforeImage.transform.DOLocalMoveX(650, 1.5f);
            beforeImage2.transform.DOLocalMoveX(650, 1.5f);
            beforeImage.transform.DOScale(Vector3.zero, 1.5f);
            beforeImage2.transform.DOScale(Vector3.zero, 1.5f);
             yield return new WaitForSeconds(1f);

            afterImage.transform.DOScale(new Vector3(1, 1, 1), 1f);
            afterImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(2f);

            afterImage.DOFade(0f, 1f);
             yield return new WaitForSeconds(1f);

        }

        // ひ（wait：5秒）
        if (num == 5)
        {
            beforeImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(1f);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), 1f);
             yield return new WaitForSeconds(1f);

            afterImage.transform.DOScale(new Vector3(1, 1, 1), 1f);
            afterImage.DOFade(1f, 1f);
             yield return new WaitForSeconds(2f);

            afterImage.DOFade(0f, 1f);
             yield return new WaitForSeconds(1f);
        }

        objs[num].SetActive(false);
    }
}

