using UnityEngine;
using UnityEngine.SceneManagement;

namespace PEC1.GameManagers
{
    public class NewGame : MonoBehaviour
    {
        public void StartNewGame()
        {
            SceneManager.LoadScene("Combat");
        }
    }
}
