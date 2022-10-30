using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PEC1.Entities;
using PEC1.BattleStates;

namespace PEC1.GameManagers
{
    public class CombatManager : StateMachine
    {
        public TextMeshProUGUI playerText;
        public TextMeshProUGUI enemyText;
        public Transform insultComebackParent;
        public GameObject insultComebackPrefab;
        public GameObject playerInstance;
        public GameObject enemyInstance;

        public float dialogueDelay = 2f;

        [HideInInspector] public InsultComeback[] insultComebacks;

        public Character Player = new Character(3);
        public Character Enemy = new Character(3);
        
        [HideInInspector] public Animator mPlayerAnimator;
        [HideInInspector] public Animator mEnemyAnimator;

        private void Start()
        {
            mPlayerAnimator = playerInstance.GetComponent<Animator>();
            mEnemyAnimator = enemyInstance.GetComponent<Animator>();
            playerText.text = String.Empty;
            enemyText.text = String.Empty;
            DestroyInsultComebacksOnScreen();
            insultComebacks = GetInsultComebackList();
            SetState(new Begin(this));
        }

        private InsultComeback[] GetInsultComebackList()
        {
            var insultComebackList = Resources.Load<TextAsset>("InsultComeback");
            return JsonUtility.FromJson<InsultComebackList>("{\"insultComebacks\":" + insultComebackList.text + "}")
                .insultComebacks;
        }

        public void DestroyInsultComebacksOnScreen()
        {
            foreach (Transform child in insultComebackParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void FillInsults()
        {
            var index = 0;
            foreach (var insultComeback in insultComebacks)
            {
                var insultComebackObject = Instantiate(insultComebackPrefab, insultComebackParent, true);
                insultComebackObject.GetComponent<RectTransform>().transform.localScale = Vector3.one;
                insultComebackObject.GetComponentInChildren<TextMeshProUGUI>().text = insultComeback.insult;
                AddInsultListener(insultComebackObject.GetComponent<Button>(), index);
                index++;
            }
        }

        private void AddInsultListener(Button button, int index)
        {
            button.onClick.AddListener(() => StartCoroutine(State.Insult(index)));
        }

        public void FillComebacks(int enemyInsultIndex)
        {
            var index = 0;
            foreach (var insultComeback in insultComebacks)
            {
                var insultComebackObject = Instantiate(insultComebackPrefab, insultComebackParent, true);
                insultComebackObject.GetComponent<RectTransform>().transform.localScale = Vector3.one;
                insultComebackObject.GetComponentInChildren<TextMeshProUGUI>().text = insultComeback.comeback;
                AddComebackListener(insultComebackObject.GetComponent<Button>(), enemyInsultIndex, index);
                index++;
            }
        }

        private void AddComebackListener(Button button, int insultIndex, int comebackIndex)
        {
            button.onClick.AddListener(() => StartCoroutine(State.Comeback(insultIndex, comebackIndex)));
        }
    }
}
