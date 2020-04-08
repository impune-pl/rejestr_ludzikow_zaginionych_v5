# Rejestr ludzikow zaginionych v5
Aplikacja służąca jako rejestr osób zaginionych, czyli CRUD z uprawnieniami.
Jest to piąte podejście do tworzenia tej aplikacji, stąd v5.

# Instalacja i uruchomienie

1. Sklonuj projekt
  Przejdź do folderu w którymchcesz umieścić projekt i wykonaj poniższe polecenie
    ```
      git clone https://github.com/impune-pl/rejestr_ludzikow_zaginionych_v5 .
    ```
2. Otwórz projekt w visual studio
3. Używając konsoli menedżera pakietów (Narzędzia > Menedżer pakietów NuGet > Konsola menedżera pakietów) uruchom poniższe polecenie:
    ```
      Update-Database
    ```
    Spowoduje to utworzenie bazy danych w twoim katalogu użytkownika ( `C:\Users\%USERNAME%\` )
  
4. Teraz możesz uruchomić projekt używając kombinacji `Ctrl + F5` lub `F5`

# Uwagi
Z powodów znanych tylko Microsoftowi, dodawanie obrazków z niektórych przeglądarek (Vivaldi, możliwe że również Opera i inne) powoduje crash serwera IIS.
Ze względu na brak logów i jakichkolwiek informacji pomocnych w diagnozowaniu problemu, zdecydowałem się go zignorować. Jeśli natrafisz na ten problem, spróbuj użyć Edge, testowałem na nim wielokrotnie, i za każdym razem działało.
