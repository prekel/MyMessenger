namespace MyMessenger.Core.Responses
{
	public enum ResponseCode
	{
		/// <summary>
		/// Всё хорошо
		/// </summary>
		Ok, 
		/// <summary>
		/// Неправильное имя пользователя
		/// </summary>
		WrongNickname,
		/// <summary>
		/// Неправильный пароль
		/// </summary>
		WrongPassword,
		/// <summary>
		/// Доступ закрыт (например при попытке получить сообщения не своего диалога)
		/// </summary>
		AccessDenied, 
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
		InternalServerError,
		/// <summary>
		/// Пользователь с таким именем не существует
		/// </summary>
		NicknameNotFound,
		/// <summary>
		/// Диалог с таким идентификатором не существует
		/// </summary>
		DialogNotFound
	}
}