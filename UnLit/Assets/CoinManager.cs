using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int currentCoin = 0;
    public TextMeshProUGUI coinText;


    private void Awake()
    {
        instance = this;
    }
    public void AddCoin()
    {
        currentCoin++;
        coinText.text = currentCoin.ToString();
        coinText.transform.DOScale(new Vector2(1.5f, 1.5f), .1f).OnComplete(ScaleBack);
    }

    void ScaleBack()
    {
        coinText.transform.DOScale(new Vector2(1f, 1f), .1f);
    }
}
