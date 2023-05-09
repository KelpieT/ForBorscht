using DG.Tweening;
using UnityEngine;

public class Window : MonoBehaviour
{
    private const float ANIM_DURATION = 0.5f;
    private Tweener tweener;

    public void Show(bool show)
    {
        if (show == true)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        tweener?.Kill();
        if (gameObject.activeInHierarchy == false)
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
        }
        tweener = transform.DOScale(Vector3.one, ANIM_DURATION);
    }

    private void Hide()
    {
        tweener?.Kill();
        tweener = transform.DOScale(Vector3.zero, ANIM_DURATION);
        tweener.onComplete += () => gameObject.SetActive(false);
    }
}
