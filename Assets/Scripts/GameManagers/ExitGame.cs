using UnityEngine;

namespace PEC1.GameManagers
{
    public class ExitGame : MonoBehaviour
    {
        public void Exit()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
