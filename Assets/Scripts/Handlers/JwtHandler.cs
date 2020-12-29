using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JWT;
using JWT.Serializers;
using UnityEngine;

public class JwtHandler : MonoBehaviour
{
    public static bool TokenStillValid(string token)
    {
        var decoder = new JwtDecoder(new JsonNetSerializer(), new JwtBase64UrlEncoder());
        var payload =
            decoder.DecodeToObject<Dictionary<string, string>>(token);
        string expStr = payload.SingleOrDefault(x => x.Key == "exp").Value;
        var date = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expStr))
            .ToLocalTime();
        return date > DateTimeOffset.Now.ToLocalTime();
    }

    public static void SaveTokens(TokenPair pair)
    {
        PlayerPrefs.SetString("accessToken", pair.AccessToken);
        PlayerPrefs.SetString("refreshToken", pair.RefreshToken);
        PlayerPrefs.Save();
    }

    public static TokenPair LoadTokens()
    {
        return new TokenPair(
            PlayerPrefs.GetString("accessToken", string.Empty),
            PlayerPrefs.GetString("refreshToken", string.Empty));
    }

    public static bool HaveAuth()
    {
        var tokens = LoadTokens();
        return tokens.AccessToken.Length >= 4 &&
               TokenStillValid(tokens.AccessToken);
    }

    public static bool HaveRefreshToken()
    {
        var tokens = LoadTokens();
        {
            return tokens.RefreshToken.Length > 4;
        }
    }
}
