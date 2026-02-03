using UnityEngine;

public class mouseCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
