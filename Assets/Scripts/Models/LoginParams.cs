/// <summary>
/// Параметры запроса входа
/// </summary>
public class LoginParams
{
    /// <summary>
    /// Id чата с аккаунтом
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    /// Пароль аккаунта
    /// </summary>
    public string Password { get; set; }

    public LoginParams(long chatId, string password)
    {
        ChatId = chatId;
        Password = password;
    }
}
