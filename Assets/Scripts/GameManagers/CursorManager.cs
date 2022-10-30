using UnityEngine;

namespace PEC1.GameManagers
{
    public class CursorManager : MonoBehaviour
    {
        private Texture2D _cursorTexture;

        void Start()
        {
            _cursorTexture = Resources.Load<Texture2D>("Sprites/cursor");
            var cursorOffset = new Vector2(_cursorTexture.width / 2f, _cursorTexture.height / 2f);
            Cursor.SetCursor(_cursorTexture, cursorOffset, CursorMode.Auto);
        }

    }
}
