using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Transform insultComebackParent;
    public GameObject insultComebackPrefab;

    public float dialogueDelay = 2f;
    
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
        StartCoroutine(StartBattle());

    }

    private IEnumerator StartBattle()
    {
        _isPlayerTurn = UnityEngine.Random.value > 0.5f;
        if (_isPlayerTurn)
        {
            storyText.text = "[PLAYER] I'll start!";
            yield return new WaitForSeconds(dialogueDelay);
            PlayerTurn();
        }
        else
        {
            storyText.text = "[ENEMY] It's my turn!";
            yield return new WaitForSeconds(dialogueDelay);
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
        button.onClick.AddListener(() => StartCoroutine(Insult(index)));
    }

    private void AddComebackListener(Button button, int insultIndex, int comebackIndex)
    {
        button.onClick.AddListener(() => StartCoroutine(Comeback(insultIndex, comebackIndex)));
    }
    
    private IEnumerator Insult(int index)
    {
        foreach (Transform child in insultComebackParent.transform)
        {
            Destroy(child.gameObject);
        }

        storyText.text = "[PLAYER]\n" + _insultComebacks[index].insult;
        yield return new WaitForSeconds(dialogueDelay);
        var enemyComebackIndex = UnityEngine.Random.Range(0, _insultComebacks.Length);
        var enemyComeback = _insultComebacks[enemyComebackIndex].comeback;
        storyText.text = "[ENEMY]\n" + enemyComeback;
        yield return new WaitForSeconds(dialogueDelay);
        if (enemyComebackIndex == index)
        {
            _playerLives--;
            storyText.text = "You lost a life!";
            yield return new WaitForSeconds(dialogueDelay);
            if (_playerLives == 0)
            {
                storyText.text = "You lost the game...";
                yield return new WaitForSeconds(dialogueDelay);
                Endgame(false);
            }
            EnemyTurn();
        }
        else
        {
            _enemyLives--;
            storyText.text = "Your opponent lost a life.";
            yield return new WaitForSeconds(dialogueDelay);
            if (_enemyLives == 0)
            {
                storyText.text = "You won the game!";
                yield return new WaitForSeconds(dialogueDelay);
                Endgame(true);
            }
            PlayerTurn();
        }
    }
    
    private IEnumerator Comeback(int insultIndex, int comebackIndex)
    {
        foreach (Transform child in insultComebackParent.transform)
        {
            Destroy(child.gameObject);
        }
        storyText.text = "[PLAYER]\n" + _insultComebacks[comebackIndex].comeback;
        yield return new WaitForSeconds(dialogueDelay);
        if (insultIndex == comebackIndex)
        {
            _enemyLives--;
            storyText.text = "Your opponent lost a life.";
            yield return new WaitForSeconds(dialogueDelay);
            if (_enemyLives == 0)
            {
                storyText.text = "Your won the game!";
                yield return new WaitForSeconds(dialogueDelay);
                Endgame(true);
            }
            else
            {
                PlayerTurn();    
            }
        }
        else
        {
            _playerLives--;
            storyText.text = "You lost a life.";
            yield return new WaitForSeconds(dialogueDelay);
            if (_playerLives == 0)
            {
                storyText.text = "You lost the game...";
                yield return new WaitForSeconds(dialogueDelay);
                Endgame(false);
            } else {
                EnemyTurn();
            }
        }
    }
    
    private void Endgame(bool isPlayerWinner)
    {
        SceneManager.LoadScene("GameOver");
    }
}
