;------------------------------------------------------------------------------
;   Определяем некоторые константы
;------------------------------------------------------------------------------

; Имя приложения
#define   Name        "MemoNote"
; Версия приложения
#define   Version    "0.0.1"
; Фирма-разработчик
#define   Publisher  "Vold"
; Сафт фирмы разработчика
#define   URL        "http://www.google.com"
; Имя исполняемого модуля
#define   ExeName    "MemoNote.exe"

;****************************************************************
[Setup]

; Уникальный идентификатор приложения, 
;сгенерированный через Tools -> Generate GUID
AppId={{76B68066-F6E9-4B19-91EA-96ED9C0982C9}

; Прочая информация, отображаемая при установке
AppName={#Name}
AppVersion={#Version}
AppPublisher={#Publisher}
AppPublisherURL={#URL}
AppSupportURL={#URL}
AppUpdatesURL={#URL}

; Путь установки по-умолчанию
DefaultDirName={pf}\{#Name}
; Имя группы в меню "Пуск"
DefaultGroupName={#Name}

; Каталог, куда будет записан собранный setup и имя исполняемого файла
OutputDir=E:\work\test-setup
OutputBaseFileName=test-setup

; Файл иконки
SetupIconFile=E:\work\Mirami\Mirami\icon.ico

; Параметры сжатия
Compression=lzma
SolidCompression=yes

;------------------------------------------------------------------------------
;   Устанавливаем языки для процесса установки
;------------------------------------------------------------------------------
[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "License_ENG.txt"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"; LicenseFile: "License_RUS.txt"

;------------------------------------------------------------------------------
;   Опционально - некоторые задачи, которые надо выполнить при установке
;------------------------------------------------------------------------------
[Tasks]
; Создание иконки на рабочем столе
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]

; Исполняемый файл
Source: "D:\Projects\STP\MemoNote\MemoNote\bin\Debug\MemoNote.exe"; DestDir: "{app}"; Flags: ignoreversion

; Прилагающиеся ресурсы
Source: "D:\Projects\STP\MemoNote\MemoNote\bin\Debug\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

;------------------------------------------------------------------------------
;   Указываем установщику, где он должен взять иконки
;------------------------------------------------------------------------------ 
[Icons]

Name: "{group}\{#Name}"; Filename: "{app}\{#ExeName}"

Name: "{commondesktop}\{#Name}"; Filename: "{app}\{#ExeName}"; Tasks: desktopicon