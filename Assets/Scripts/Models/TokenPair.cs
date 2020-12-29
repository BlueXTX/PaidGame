public class TokenPair
{
    public long Id { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public TokenPair(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
