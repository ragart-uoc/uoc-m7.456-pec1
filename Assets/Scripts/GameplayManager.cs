using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Transform insultComebackParent;
    public GameObject insultComebackPrefab;
    
    private InsultComeback[] _insultComebacks;
    
    private bool _isPlayerTurn;
    
    private int _playerLives = 3;
    private int _enemyLives = 3;

    private void Start()
    {
        storyText.text = String.Empty;
        foreach (Transform child in insultComebackParent.transform)
        {
            Destroy(child.gameObject);
        }
        FillInsultComebackList();
        _isPlayerTurn = UnityEngine.Random.value > 0.5f;
        if (_isPlayerTurn)
        {
            StartCoroutine(DelayedStoryText("You are the first to speak.", 3f));
            PlayerTurn();
        }
        else
        {
            StartCoroutine(DelayedStoryText("Your opponent is the first to speak.", 3f));
            EnemyTurn();
        }

    }

    private void FillInsultComebackList()
    {
        var insultComebackList = Resources.Load<TextAsset>("InsultComeback");
        _insultComebacks = JsonUtility.FromJson<InsultComebackList>("{\"insultComebacks\":" + insultComebackList.text + "}").insultComebacks;
    }
    
    private void PlayerTurn()
    {
        storyText.text = "Choose an insult";
        var index = 0; 
        foreach (var insultComeback in _insultComebacks)
        {
            var insultComebackObject = Instantiate(insultComebackPrefab, insultComebackParent, true);
            insultComebackObject.GetComponent<RectTransform>().transform.localScale = Vector3.one;
            insultComebackObject.GetComponentInChildren<TextMeshProUGUI>().text = insultComeback.insult;
            AddInsultListener(insultComebackObject.GetComponent<Button>(), index);
            index++;
        }
    }
    
    private void EnemyTurn()
    {
        var enemyInsultIndex = UnityEngine.Random.Range(0, _insultComebacks.Length);
        var enemyInsult = _insultComebacks[enemyInsultIndex].insult;
        storyText.text = "[ENEMY]\n" + enemyInsult;
        var index = 0; 
        foreach (var insultComeback in _insultComebacks)
        {
            var insultComebackObject = Instantiate(insultComebackPrefab, insultComebackParent, true);
            insultComebackObject.GetComponent<RectTransform>().transform.localScale = Vector3.one;
            insultComebackObject.GetComponentInChildren<TextMeshProUGUI>().text = insultComeback.comeback;
            AddComebackListener(insultComebackObject.GetComponent<Button>(), enemyInsultIndex, index);
            index++;
        }
    }

    private void AddInsultListener(Button button, int index)
    {
        button.onClick.AddListener(() => Insult(index));
    }

    private void AddComebackListener(Button button, int insultIndex, int comebackIndex)
    {
        button.onClick.AddListener(() => Comeback(insultIndex, comebackIndex));
    }
    
    private void Insult(int index)
    {
        foreach (Transform child in insultComebackParent.transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(DelayedStoryText("[PLAYER]\n" + _insultComebacks[index].insult, 3f));
        var enemyComebackIndex = UnityEngine.Random.Range(0, _insultComebacks.Length);
        var enemyComeback = _insultComebacks[enemyComebackIndex].comeback;
        StartCoroutine(DelayedStoryText("[ENEMY]\n" + enemyComeback, 3f));
        if (enemyComebackIndex == index)
        {
            _playerLives--;
            StartCoroutine(DelayedStoryText("You lost a life", 3f));
            if (_playerLives == 0)
            {
                StartCoroutine(DelayedStoryText("You lost the game", 3f));
                Endgame(false);
            }
            EnemyTurn();
        }
        else
        {
            _enemyLives--;
            StartCoroutine(DelayedStoryText("Your opponent lost a life", 3f));
            if (_enemyLives == 0)
            {
                StartCoroutine(DelayedStoryText("You won the game", 3f));
                Endgame(true);
            }
            PlayerTurn();
        }
    }
    
    private void Comeback(int insultIndex, int comebackIndex)
    {
        foreach (Transform child in insultComebackParent.transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(DelayedStoryText("[PLAYER]\n" + _insultComebacks[comebackIndex].comeback, 3f));
        if (insultIndex == comebackIndex)
        {
            _enemyLives--;
            StartCoroutine(DelayedStoryText("Your opponent lost a life", 3f));
            if (_enemyLives == 0)
            {
                StartCoroutine(DelayedStoryText("You won the game", 3f));
                Endgame(true);
            }
            PlayerTurn();
        }
        else
        {
            _playerLives--;
            StartCoroutine(DelayedStoryText("You lost a life", 3f));
            if (_playerLives == 0)
            {
                StartCoroutine(DelayedStoryText("You lost the game", 3f));
                Endgame(false);
            }
            EnemyTurn();
        }
    }
    
    private void Endgame(bool isPlayerWinner)
    {
        
    }
    
    private IEnumerator DelayedStoryText(string text, float delay)
    {
        storyText.text = text;
        yield return new WaitForSeconds(delay);
    }
}
