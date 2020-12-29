using Newtonsoft.Json;

/// <summary>
/// Параметры запроса регистрации
/// </summary>
public class RegisterParams
{
    /// <summary>
    /// Id чата с аккаунтом 
    /// </summary>
    [JsonProperty("chatId")]
    public long ChatId { get; set; }

    /// <summary>
    /// Пароль аккаунта
    /// </summary>
    [JsonProperty("password")]
    public string Password { get; set; }

    public RegisterParams(long chatId, string password)
    {
        ChatId = chatId;
        Password = password;
    }
}
