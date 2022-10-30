using UnityEngine;
using UnityEngine.UI;

namespace PEC1.GameManagers
{
    public class InputScrollManager : MonoBehaviour
    {
        public GameObject scrollBar;
        public Button buttonUp;
        public Button buttonDown;
        public ScrollRect scrollRect;

        private void Start()
        {
            buttonUp.onClick.AddListener(OnButtonUpClick);
            buttonDown.onClick.AddListener(OnButtonDownClick);            
            buttonUp.interactable = false;
            scrollRect.onValueChanged.AddListener(OnScrollRectValueChanged);
        }

        private void OnScrollRectValueChanged(Vector2 value)
        {
            switch (value.y)
            {
                case > 0.9999f:
                    buttonUp.interactable = false;
                    buttonDown.interactable = true;
                    break;
                case <= 0.0001f:
                    buttonUp.interactable = true;
                    buttonDown.interactable = false;
                    break;
                default:
                    buttonUp.interactable = true;
                    buttonDown.interactable = true;
                    break;
            }
        }

        private void OnButtonUpClick()
        {
            if (scrollRect.normalizedPosition.y < 1)
            {
                scrollRect.verticalNormalizedPosition += scrollRect.scrollSensitivity / scrollRect.content.sizeDelta.y;
            }
        }

        private void OnButtonDownClick()
        {
            if (scrollRect.normalizedPosition.y > 0)
            {
                scrollRect.verticalNormalizedPosition -= scrollRect.scrollSensitivity / scrollRect.content.sizeDelta.y;
            }
        }

        private void Update()
        {
            
            if (scrollBar.activeSelf && scrollRect.content.sizeDelta.y <= 0.0001f)
            {
                scrollBar.SetActive(false);
            }
            
            if (!scrollBar.activeSelf && scrollRect.content.sizeDelta.y > 0.0001f)
            {
                scrollBar.SetActive(true);
            }
        }

    }
}
