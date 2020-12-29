using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;

public class ServerHandler
{
    private readonly ServerConnectionConfiguration _config;

    public ServerHandler(ServerConnectionConfiguration config)
    {
        _config = config;
    }

    public IEnumerator Register(RegisterParams regParams, Action<bool> callback)
    {
        var data = regParams;
        var options = new RequestHelper
        {
            Uri = _config.RegisterUrl,
            CertificateHandler = new DebugCertificateHandler(),
            BodyString = JsonConvert.SerializeObject(data)
        };
        RestClient.Post(options).Then(helper =>
        {
            callback.Invoke(helper.StatusCode == (long) HttpStatusCode.OK);
        });
        yield break;
    }

    public IEnumerator Login(LoginParams logParams, Action<TokenPair> callback)
    {
        var data = logParams;
        var options = new RequestHelper
        {
            Uri = _config.LoginUrl,
            CertificateHandler = new DebugCertificateHandler(),
            BodyString = JsonConvert.SerializeObject(data)
        };
        RestClient.Post(options).Then(helper =>
        {
            callback.Invoke(JsonConvert.DeserializeObject<TokenPair>(helper.Text));
        });
        yield break;
    }

    public IEnumerator RefreshToken(string refreshToken, Action<TokenPair> callback)
    {
        var options = new RequestHelper
        {
            Uri = _config.RefreshTokenUrl,
            CertificateHandler = new DebugCertificateHandler(),
            Params = new Dictionary<string, string> {{"refreshToken", refreshToken}}
        };
        RestClient.Get(options).Then(helper =>
        {
            callback.Invoke(JsonConvert.DeserializeObject<TokenPair>(helper.Text));
        });
        yield break;
    }

    public IEnumerator GetStats(string authToken, Action<AccountStats> callback)
    {
        var options = new RequestHelper
        {
            Uri = _config.StatsUrl,
            CertificateHandler = new DebugCertificateHandler(),
            Headers = new Dictionary<string, string>
            {
                {"Authorization", $"Bearer {authToken}"},
            }
        };
        RestClient.Get(options).Then(helper =>
        {
            callback.Invoke(JsonConvert.DeserializeObject<AccountStats>(helper.Text));
        });
        yield break;
    }
}
