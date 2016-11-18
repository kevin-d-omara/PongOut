using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject background;

    void Awake()
    {
        Camera camera = gameObject.GetComponent<Camera>();
        FitCameraToSprite(ref camera, background);
    }

    // camera is adjusted to:
    //      - be centered on the sprite
    //      - tightly fit the limiting dimension of the sprite (width or height)
    private void FitCameraToSprite(ref Camera camera, GameObject obj)
    {
        CenterCameraOnSprite(ref camera, obj);

        Sprite sprite = obj.GetComponent<SpriteRenderer>().sprite;  
        Bounds bounds = sprite.bounds;
        Vector3 scale = obj.transform.lossyScale;   // account for scale factor

        float spriteUnitHeight  = bounds.size.y * scale.y;
        float spriteUnitWidth   = bounds.size.x * scale.x;
        float spriteAspectRatio = spriteUnitWidth / spriteUnitHeight;

        if (spriteAspectRatio <= camera.aspect) // spriteAspectRatio is taller
        {                                       // snap camera to sprite height
            camera.orthographicSize = spriteUnitHeight / 2f;
        }
        else                                    // spriteAspectRatio is wider
        {                                       // snap camera to sprite width
            camera.orthographicSize = (spriteUnitWidth / camera.aspect) / 2f;
        }
    }

    private void CenterCameraOnSprite(ref Camera camera, GameObject obj)
    {
        Vector3 center = new Vector3(obj.transform.position.x,
                                     obj.transform.position.y,
                                     camera.transform.position.z);

        camera.transform.position = center;
    }
}