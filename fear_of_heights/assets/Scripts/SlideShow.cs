using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlideShow : MonoBehaviour
{
    public Image image;
    public Sprite[] allSprites;
    public float startGap, gapBetweenEachImage;

    void Start()
    {
        if (allSprites.Length > 0)
        {
            image.sprite = allSprites[0];
        }
        StartCoroutine(PlaySlideShow());
    }

    IEnumerator PlaySlideShow()
    {
        yield return new WaitForSeconds(startGap);

        foreach (var spriteHere in allSprites)
        {
            image.sprite = spriteHere;
            yield return new WaitForSeconds(gapBetweenEachImage);
        }
    }

    void Update()
    {

    }
    
}
