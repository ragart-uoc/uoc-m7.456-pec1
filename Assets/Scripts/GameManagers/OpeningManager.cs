using System.Collections;
using TMPro;
using UnityEngine;

namespace PEC1.GameManagers
{
    public class OpeningManager : MonoBehaviour
    {
        public TextMeshProUGUI openingText;
        private IEnumerator Start()
        {
            openingText.canvasRenderer.SetAlpha(0.0f);
            openingText.text = "Salvador Banderas presents";
            openingText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(2.5f);
            openingText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            openingText.text = "Based on The Secret of Monkey Island (TM) by LucasArts Entertainment Company";
            openingText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(2.5f);
            openingText.CrossFadeAlpha(0.0f, 1.5f, false);
        }
    }
}
