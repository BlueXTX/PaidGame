using System;
using JWT.Builder;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthHandler : MonoBehaviour
{
    [SerializeField] private ServerConnectionConfiguration config;
    [SerializeField] private InputField chatIdField;
    [SerializeField] private InputField passwordField;
    private ServerHandler _server;
    [SerializeField] private string sceneAfterAuth;

    private void Start()
    {
        _server = new ServerHandler(config);
        if (JwtHandler.HaveAuth())
        {
            LoadNextScene();
        }
        else
        {
            if (JwtHandler.HaveRefreshToken())
            {
                StartCoroutine(_server.RefreshToken(JwtHandler.LoadTokens().RefreshToken,
                    pair => { JwtHandler.SaveTokens(pair); }));
            }
        }
    }

    public void CallLogin()
    {
        StartCoroutine(
            _server.Login(new LoginParams(Convert.ToInt64(chatIdField.text),
                passwordField.text), pair =>
            {
                if (pair.AccessToken.Length <= 0)
                {
                    PopupWindow.Instance.ShowMessage("Неверные данные для входа");
                }
                else
                {
                    PlayerPrefs.SetString("accessToken", pair.AccessToken);
                    PlayerPrefs.SetString("refreshToken", pair.RefreshToken);
                    LoadNextScene();
                }
            })
        );
    }

    private void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(sceneAfterAuth);
    }


    private void RefreshToken()
    {
        var tokens = JwtHandler.LoadTokens();
        if (tokens.RefreshToken.Length > 4)
        {
            StartCoroutine(_server.RefreshToken(tokens.RefreshToken,
                pair => { JwtHandler.SaveTokens(pair); }));
        }
    }
}
