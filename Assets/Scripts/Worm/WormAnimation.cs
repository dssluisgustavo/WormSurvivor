using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WormAnimation : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        DOTween.Sequence()
            .Append(spriteRenderer.transform.DOScaleY(0.9f, .25f))
            .Append(spriteRenderer.transform.DOScaleY(1f, .25f))
            .SetLoops(-1);

        var side = Random.Range(-1, 2);
        if (side == 0) side = 1;
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                side *= -1;
                spriteRenderer.transform.localScale = new Vector3(side, 1, 1);
            }).AppendInterval(5f).SetLoops(-1);
    }
}
