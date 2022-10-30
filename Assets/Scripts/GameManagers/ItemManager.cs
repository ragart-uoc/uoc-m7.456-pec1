using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PEC1.Entities;
using UnityEngine.EventSystems;

namespace PEC1.GameManagers
{
    public class ItemManager : MonoBehaviour
    {
        public GameObject statusBar;
        public TextMeshProUGUI statusText;
        public Transform itemParent;
        public GameObject itemPrefab;

        private Item[] _items;

        private void Start()
        {
            statusText.text = String.Empty;
            DestroyItemsOnScreen();
            _items = GetItemList();
            FillItems();
        }
        
        private Item[] GetItemList()
        {
            var itemList = Resources.Load<TextAsset>("Items");
            return JsonUtility.FromJson<ItemList>("{\"items\":" + itemList.text + "}").items;
        }
        
        private void DestroyItemsOnScreen()
        {
            foreach (Transform child in itemParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void FillItems()
        {
            var index = 0;
            foreach (var item in _items)
            {
                var itemObject = Instantiate(itemPrefab, itemParent, true);
                itemObject.GetComponent<RectTransform>().transform.localScale = Vector3.one;
                statusText.text = item.name;
                var image = Resources.Load<Sprite>("Sprites/" + item.image);
                itemObject.GetComponentInChildren<Image>().sprite = image;
                AddItemListener(itemObject, index);
                index++;
            }
        }
        
        private void AddItemListener(GameObject item, int index)
        {
            var eventTrigger = item.AddComponent<EventTrigger>();
            
            var pointerEnter = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            pointerEnter.callback.AddListener((eventData) => { OnItemPointerEnter(eventData, index); });
            eventTrigger.triggers.Add(pointerEnter);

            var pointerExit = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            pointerExit.callback.AddListener(OnItemPointerExit);
            eventTrigger.triggers.Add(pointerExit);
        }
        
        private void OnItemPointerEnter(BaseEventData eventData, int index)
        {
            statusText.text = _items[index].name;
            statusBar.SetActive(true);
        }
        
        private void OnItemPointerExit(BaseEventData eventData)
        {
            statusText.text = String.Empty;
            statusBar.SetActive(false);
        }

    }
}
