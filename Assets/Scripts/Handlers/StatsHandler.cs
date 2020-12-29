using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour
{
    [SerializeField] private ServerConnectionConfiguration config;
    private ServerHandler _server;
    [SerializeField] private Text moneyText; 
    [SerializeField] private Text realsText; 
    [SerializeField] private Text scoreText; 

    private void Start()
    {
        _server = new ServerHandler(config);
        StartCoroutine(_server.GetStats(PlayerPrefs.GetString("accessToken"), Callback));
    }

    private void Callback(AccountStats obj)
    {
        moneyText.text = obj.MoneyBalance.ToString(CultureInfo.InvariantCulture);
        realsText.text = obj.RealBalance.ToString(CultureInfo.InvariantCulture);
        scoreText.text = obj.Score.ToString(CultureInfo.InvariantCulture);
    }
}
