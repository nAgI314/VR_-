using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ImageEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objs = default;
    private float waitTime = 1.0f;
    private float afterImageFadeTime = 2.0f;

    private void Start()
    {
        StartCoroutine(Effect(1));
    }

    private void Update()
    {
        
    }

    private void InitWaitTime()
    {
        waitTime = 1.0f;
    }

    private IEnumerator Effect(int num)
    {
        yield return new WaitForSeconds(3f);

        // え（wait：7秒）
        if (num == 1)
        {
            var beforeImage = objs[0].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[0].transform.Find("after").GetComponent<Image>();
            var darumaObj = objs[0].transform.Find("Daruma").gameObject;

            Debug.Log("aaaa in1");

            beforeImage.DOFade(1f, waitTime);
            waitTime = 3f;
            yield return new WaitForSeconds(waitTime);
            Debug.Log("aaaa in2");
            waitTime = 2f;
            beforeImage.DOFade(0f, waitTime);
            darumaObj.transform.DOScale(new Vector3(800, 800, 800), 2f);
            darumaObj.transform.DOMoveZ(200, waitTime);
            afterImage.DOFade(1f, waitTime);
            Debug.Log("aaaa in3");
            yield return new WaitForSeconds(waitTime);
            afterImageFadeTime = 2f;
            afterImage.DOFade(0f, afterImageFadeTime);
            Debug.Log("aaaa in4");
            yield return new WaitForSeconds(afterImageFadeTime);
        }

        // す（wait：6秒）
        if (num == 2)
        {
            var beforeImage = objs[1].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[1].transform.Find("after").GetComponent<Image>();

            beforeImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), waitTime);
            yield return new WaitForSeconds(waitTime);
            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            waitTime = 2;
            yield return new WaitForSeconds(waitTime);

            afterImage.DOFade(0f, afterImageFadeTime);
            yield return new WaitForSeconds(afterImageFadeTime);
            
        }

        // た（wait：6秒）
        if (num == 3)
        {
            var beforeImage = objs[2].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[2].transform.Find("after").GetComponent<Image>();

            beforeImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), waitTime);
            yield return new WaitForSeconds(waitTime);
            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            waitTime = 2f;
            yield return new WaitForSeconds(waitTime);

            afterImage.DOFade(0f, afterImageFadeTime);
            yield return new WaitForSeconds(afterImageFadeTime);
        }

        // つ（wait：6秒）
        if (num == 4)
        {
            var beforeImage = objs[3].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[3].transform.Find("after").GetComponent<Image>();

            beforeImage.DOFade(1f, waitTime);
            waitTime = 2.0f;
            yield return new WaitForSeconds(waitTime);
            beforeImage.DOFade(0f, waitTime);
            afterImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);
            
            afterImage.DOFade(0f, afterImageFadeTime);
            yield return new WaitForSeconds(afterImageFadeTime);
        }

        // ね（wait：6.5秒）
        if (num == 5)
        {
            var beforeImage_1 = objs[4].transform.Find("before1").GetComponent<Image>();
            var beforeImage_2 = objs[4].transform.Find("before2").GetComponent<Image>();
            var afterImage = objs[4].transform.Find("after").GetComponent<Image>();

            beforeImage_1.DOFade(1f, waitTime);
            beforeImage_2.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);

            waitTime = 2f;
            beforeImage_1.transform.DOLocalMoveX(650, waitTime);
            beforeImage_2.transform.DOLocalMoveX(650, waitTime);
            beforeImage_1.transform.DOScale(Vector3.zero, waitTime);
            beforeImage_2.transform.DOScale(Vector3.zero, waitTime);
            yield return new WaitForSeconds(waitTime);

            waitTime = 0.5f;
            objs[3].transform.Find("Spheres Explode").gameObject.SetActive(true);
            yield return new WaitForSeconds(waitTime);

            waitTime = 1.0f;
            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);

            afterImage.DOFade(0f, waitTime);
            yield return new WaitForSeconds(afterImageFadeTime);
            
        }

        // ひ（wait：6秒）
        if (num == 6)
        {
            var beforeImage = objs[5].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[5].transform.Find("after").GetComponent<Image>();

            beforeImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), waitTime);
            yield return new WaitForSeconds(waitTime);

            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            waitTime = 2f;
            yield return new WaitForSeconds(waitTime);

            afterImage.DOFade(0f, afterImageFadeTime);
            yield return new WaitForSeconds(afterImageFadeTime);
        }

        InitWaitTime();
    }
}
