using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class GetEgg : MonoBehaviour
{
    private MainUI mainUI;

    private void Start()
    {
        mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();
    }

    public void ClickEggs()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(mainUI.goldIcon.position);
        transform.parent.DOMove(pos, 0.8f).OnComplete(() =>
        {
            mainUI.money += 100;
            Destroy(gameObject);
        });
    }
}
