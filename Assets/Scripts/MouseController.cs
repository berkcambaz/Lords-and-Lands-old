using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    public float zoomMin;
    public float zoomMax;
    public float scrollSpeed;
    private float scroll;

    private Vector3 dragOrigin;

    private void Start()
    {
        scroll = Game.Instance.cam.orthographicSize;
    }

    private void Update()
    {
        // If mouse is over UI, cancel any tile related UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            UIManager.HideTileHighlight();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 offset = Game.Instance.cam.ScreenToWorldPoint(dragOrigin) - Game.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
            Game.Instance.cam.transform.Translate(offset);
            dragOrigin = Input.mousePosition;
        }

        Vector2Int tilePos = Vector2Int.FloorToInt(Game.Instance.cam.ScreenToWorldPoint(Input.mousePosition));
        UIManager.ShowTileHighlight(tilePos);

        scroll = Mathf.Clamp(scroll - (Input.mouseScrollDelta.y * scrollSpeed), zoomMin, zoomMax);
        Game.Instance.cam.orthographicSize = Mathf.Lerp(Game.Instance.cam.orthographicSize, scroll, Time.deltaTime * 5f);
    }
}
