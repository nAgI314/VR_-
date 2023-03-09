using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ShortVersionEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objs = default;
    private float waitTime = default;
    private float afterImageFadeTime = 3.0f;

    private void Start()
    {
        StartCoroutine(Effect(4));
    }

    private void Update()
    {

    }

    private IEnumerator Effect(int num)
    {
        // ‚·
        if (num == 1)
        {
            var beforeImage = objs[0].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[0].transform.Find("after").GetComponent<Image>();

            waitTime = 1.0f;
            beforeImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), waitTime);
            yield return new WaitForSeconds(waitTime);
            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(afterImageFadeTime);
            afterImage.DOFade(0f, waitTime);
        }

        // ‚½
        if (num == 2)
        {
            var beforeImage = objs[1].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[1].transform.Find("after").GetComponent<Image>();

            waitTime = 1.0f;
            beforeImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), waitTime);
            yield return new WaitForSeconds(waitTime);
            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(afterImageFadeTime);
            afterImage.DOFade(0f, waitTime);
        }

        // ‚Ð
        if (num == 3)
        {
            var beforeImage = objs[2].transform.Find("before").GetComponent<Image>();
            var afterImage = objs[2].transform.Find("after").GetComponent<Image>();

            waitTime = 1.0f;
            beforeImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);
            beforeImage.transform.DOScale(new Vector3(0, 0, 0), waitTime);
            yield return new WaitForSeconds(waitTime);
            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(afterImageFadeTime);
            afterImage.DOFade(0f, waitTime);
        }

        // ‚Ë
        if (num == 4)
        {
            var beforeImage_1 = objs[3].transform.Find("before1").GetComponent<Image>();
            var beforeImage_2 = objs[3].transform.Find("before2").GetComponent<Image>();
            var afterImage = objs[3].transform.Find("after").GetComponent<Image>();

            waitTime = 0.5f;
            beforeImage_1.DOFade(1f, waitTime);
            beforeImage_2.DOFade(1f, waitTime);
            yield return new WaitForSeconds(waitTime);

            waitTime = 1.5f;
            beforeImage_1.transform.DOLocalMoveX(650, waitTime);
            beforeImage_2.transform.DOLocalMoveX(650, waitTime);
            beforeImage_1.transform.DOScale(Vector3.zero, waitTime);
            beforeImage_2.transform.DOScale(Vector3.zero, waitTime);
            yield return new WaitForSeconds(waitTime);

            objs[3].transform.Find("Spheres Explode").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            waitTime = 1.0f;
            afterImage.transform.DOScale(new Vector3(1, 1, 1), waitTime);
            afterImage.DOFade(1f, waitTime);
            yield return new WaitForSeconds(afterImageFadeTime);
            afterImage.DOFade(0f, waitTime);
        }

    }
}
