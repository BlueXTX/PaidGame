using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "Server connection configuration",
    menuName = "Create new server connection configuration")]
public class ServerConnectionConfiguration : ScriptableObject
{
    [SerializeField] private string baseUri;
    [SerializeField] private string registerPath;
    [SerializeField] private string loginPath;
    [SerializeField] private string statsPath;
    [SerializeField] private string refreshTokenPath;
    public string RegisterUrl => baseUri + registerPath;
    public string LoginUrl => baseUri + loginPath;
    public string StatsUrl => baseUri + statsPath;

    public string RefreshTokenUrl => baseUri + refreshTokenPath;
}
