using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private GameObject mainScene;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 miniOffset;

    public void OnDrag()
    {
        if (mainScene.transform.localScale.x < 1)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mainScene.transform.position = pos + miniOffset;
        }
        else
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mainScene.transform.position = pos + offset;
        }
    }
}
