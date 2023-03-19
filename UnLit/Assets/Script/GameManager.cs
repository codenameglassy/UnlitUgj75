using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform playerTransform, startPos, coinFinalPos;
    public PlayerMovement player;

    public CanvasGroup fadeCanvas;
    public RectTransform dialoguePanel;
    public TextMeshProUGUI chatText;
    public Vector2 showPos, hidePos;

    public int keyCount; 
    private void Awake()
    {
        instance = this;

        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManagerCS.instance.Play("theme");
        fadeCanvas.DOFade(0, 3f);
    }

    void Init()
    {
        fadeCanvas.alpha = 1;
        StartCoroutine(Tutorial());
    }

    public void ResetPlayerPos()
    {
        StartCoroutine(Enum_Reset_Player());
    }

    IEnumerator Enum_Reset_Player()
    {
        yield return new WaitForSeconds(.5f);
        playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y + 10f, playerTransform.position.z), 2f);

        yield return new WaitForSeconds(2f);
        AudioManagerCS.instance.Play("dive");
        playerTransform.DOMove(startPos.position, 3f);
    }

    IEnumerator Tutorial()
    {

        yield return new WaitForSeconds(.5f);
        chatText.text = "WASD to move, Space bar to Roll/Attack";
        dialoguePanel.DOAnchorPos(showPos, 1f);
        yield return new WaitForSeconds(5f);
        dialoguePanel.DOAnchorPos(hidePos, 1f);

    }

    public void AddKey()
    {
        keyCount++;

        if(keyCount >= 6)
        {
            return;
        }

    }
 
}
