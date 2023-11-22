Пример проекта для тестирования логина в Steam через проксю.

Перед тем как тестировать, в проект нужно добавить 3 файла.
Лежать они должны в одной папке с этим файлом - README.txt.

- 1 -

Файл с данными от Steam аккаунта: логин и пароль.

Название:
logon_data.json

Структура:
{
    "Username": "username",
    "Password": "password"
}

- 2 -

Файл с данными от прокси: IP адрес, порт, логин и пароль.

Название:
proxy_data.json

Структура:
{
    "Address": "address",
    "Port": "port",
    "Username": "username",
    "Password": "password"
}

- 3 -

MaFile - файл с данными от Steam аккаунта для эмуляции мобильного 2FA.

Название:
steam_guard_account.maFile
