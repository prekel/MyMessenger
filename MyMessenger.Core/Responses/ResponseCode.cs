namespace MyMessenger.Core.Responses
{
	public enum ResponseCode
	{
		/// <summary>
		/// Всё хорошо
		/// </summary>
		Ok, 
		/// <summary>
		/// Доступ закрыт (например при попытке получить сообщения не своего диалога)
		/// </summary>
		AccessDenied, 
		/// <summary>
		/// Неправильное имя пользователя
		/// </summary>
		WrongNickname,
		/// <summary>
		/// Неправильный пароль
		/// </summary>
		WrongPassword,
		/// <summary>
		/// Имя пользователя уже существует
		/// </summary>
		NicknameAlreadyExists,
		/// <summary>
		/// Несуществующий токен
		/// </summary>
		InvalidToken, 
		/// <summary>
		/// Неверный запрос
		/// </summary>
		InvalidRequest, 
		/// <summary>
		/// Неизвестная ошибка
		/// </summary>
		UnknownError, 
		/// <summary>
		/// Внутренняя ошибка сервера
		/// </summary>
		InternalServerError
	}
}