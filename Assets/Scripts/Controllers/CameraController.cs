using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float edgeBuffer = 0f;

    private Camera myCamera;
    private GameObject viewedObject;

    void Awake()
    {
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void SetViewedObject(GameObject spriteObj)
    {
        viewedObject = spriteObj;
        FitCameraToSprite(ref myCamera, viewedObject, edgeBuffer);
    } 

    // Adjusts camera to:
    //      - be centered on the sprite
    //      - tightly fit the limiting dimension of the sprite (width or height)
    //      - have 'buffer' units of background around the tightest dimension
    private void FitCameraToSprite(ref Camera camera, GameObject obj, float buffer = 0f)
    {
        CenterCameraOnSprite(ref camera, obj);

        Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;
        Bounds bounds = sprite.bounds;
        Vector3 scale = obj.transform.lossyScale;   // account for scale factor

        float spriteUnitHeight = bounds.size.y * scale.y;
        float spriteUnitWidth = bounds.size.x * scale.x;
        float spriteAspectRatio = spriteUnitWidth / spriteUnitHeight;

        if (spriteAspectRatio <= camera.aspect) // spriteAspectRatio is taller
        {                                       // snap camera to sprite height
            camera.orthographicSize = spriteUnitHeight / 2f;
        }
        else                                    // spriteAspectRatio is wider
        {                                       // snap camera to sprite width
            camera.orthographicSize = (spriteUnitWidth / camera.aspect) / 2f;
        }
        camera.orthographicSize += buffer;
    }

    private void CenterCameraOnSprite(ref Camera camera, GameObject obj)
    {
        Vector3 center = new Vector3(obj.transform.position.x,
                                     obj.transform.position.y,
                                     camera.transform.position.z);

        camera.transform.position = center;
    }
}