using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public Transform meteor, metorObj;
    public Transform meteorPos;

    public RectTransform dialougePanel;
    public TextMeshProUGUI chatText;

    public Vector2 showPos, hidePos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tween());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Tween()
    {
        //dialougePanel.DOAnchorPos(hidePos, 2f);

        //

        chatText.text = "A peaceful day, winds are breezing..";
        dialougePanel.DOAnchorPos(showPos, 2f);
        yield return new WaitForSeconds(5f);
        dialougePanel.DOAnchorPos(hidePos, 1f);
        chatText.text = "...";
        yield return new WaitForSeconds(1f);


        chatText.text = "Untill... ";
        dialougePanel.DOAnchorPos(showPos, 2f);
        yield return new WaitForSeconds(4f);
        dialougePanel.DOAnchorPos(hidePos, 1f);
        chatText.text = "...";
        yield return new WaitForSeconds(1f);

        // 
        chatText.text = "A Meteor crashed...";
        dialougePanel.DOAnchorPos(showPos, 1f);
        yield return new WaitForSeconds(3f);
        dialougePanel.DOAnchorPos(hidePos, 1f);
        chatText.text = "...";
       
        yield return new WaitForSeconds(2f);
        meteor.DOMove(meteorPos.position, 5f).SetEase(Ease.Linear);
        metorObj.DOLocalRotate(new Vector3(180, 180, 180), 3f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(5f);

        chatText.text = "What troubles will it bring... ";
        dialougePanel.DOAnchorPos(showPos, 2f);
        yield return new WaitForSeconds(5f);
        dialougePanel.DOAnchorPos(hidePos, 1f);
        chatText.text = "...";
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Game");
    }
}
