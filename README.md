University Equipment Rental (C# console)
Aplikacja konsolowa w C#, która obsługuje uczelnianą wypożyczalnię sprzętu: rejestruje sprzęt i użytkowników, pozwala 
wypożyczać i zwracać sprzęt, oznaczać go jako niedostępny oraz generować raport podsumowujący.

Instrukcja uruchomienia
Sklonuj repozytorium i przejdź do katalogu projektu

    git clone https://github.com/s30786/APBD-Cw1-s30786.git

manualnie pobierając z adresu https://github.com/s30786/APBD-Cw1-s30786

Otwórz projekt w preferowanym IDE

Upewnij się, że masz zainstalowany .NET (wersja zgodna z TargetFramework w .csproj).

Uruchom aplikację

W konsoli wybieraj opcje z menu (1–12); scenariusz demonstracyjny uruchomisz z dedykowanej opcji menu.

Scenariusz demonstracyjny
Realizacja scenariusza demonstracyjnego odbywa się za pomocą komendy nr. 11 z menu konsolowego.

W jego ramach wykonywane są następujące scenariusze:
-Dodanie kilku egzemplarzy sprzętu różnych typów
-Dodanie kilku użytkowników różnych typów
-Poprawne wypożyczenie sprzętu
-Próba niepoprawnej operacji
-Zwrot w terminie
-Zwrot opóźniony z naliczeniem kary
-Wyświetlenie końcowego raportu o stanie systemu

Struktura i odpowiedzialności
Model domenowy (...Domain)
Equipment + EquipmentStatus
Abstrakcyjna baza dla sprzętu (Id generowane w kodzie, nazwa, status + metody zmiany statusu).

Laptop, Projector, Camera
Dziedziczą po Equipment. Każda klasa ma co najmniej dwa własne pola specyficzne (RAM/CPU, jasność/rozdzielczość, MP/zoom).
Dziedziczenie odzwierciedla domenę – wszystkie są „typami sprzętu”.

User + UserType
Jeden typ użytkownika z wyliczeniem UserType (Student/Employee). Dane wspólne: Id, imię, nazwisko, typ użytkownika.
Różne reguły (limity) wynikają z pola UserType, a nie z osobnych klas.

Rental
Pojedyncze wypożyczenie: użytkownik, sprzęt, data wypożyczenia, termin zwrotu, data faktycznego zwrotu i kara. 
Właściwości IsReturned, IsOverdue, WasReturnedOnTime opisują stan wypożyczenia na podstawie dat,
bez duplikowania logiki w innych miejscach.

Logika biznesowa
CatalogServices
Przechowuje użytkowników i sprzęt w pamięci. Dodaje nowe wpisy, zwraca listy, wyszukuje po Id i 
zmienia status sprzętu (dostępny/niedostępny). Odpowiada tylko za „katalog”, nie zna logiki wypożyczeń.

RentalServices
Odpowiada za proces wypożyczeń i zwrotów:

kontrola limitów (2 aktywne wypożyczenia dla studenta, 5 dla pracownika),

blokada wypożyczenia sprzętu niedostępnego lub już wypożyczonego,

wyznaczenie terminu zwrotu na podstawie liczby dni,

naliczanie kary za każdy dzień po terminie (PenaltyPerDay).

Reguły limitów i kar są zapisane w jednym miejscu, dzięki czemu ich zmiana nie wymaga modyfikowania wielu klas.

ReportServices
Generuje tekstowy raport podsumowujący (użytkownicy, sprzęt według statusu i typu, liczba wypożyczeń,
liczba przeterminowanych, suma kar) oraz formatuje listy sprzętu, użytkowników i wypożyczeń do wyświetlenia w konsoli.

Interfejs konsolowy
ConsoleInput
Jedno miejsce do pobierania i walidacji danych z konsoli (string/int/bool).
Dzięki temu walidacja wejścia nie jest wymieszana z logiką biznesową.

CatalogConsoleHandler
Obsługuje operacje katalogowe: dodawanie użytkowników i sprzętu, lista całego sprzętu, lista sprzętu dostępnego,
lista użytkowników, oznaczanie sprzętu jako niedostępnego.

RentalConsoleHandler
Obsługuje operacje wypożyczeń: wypożyczenie, zwrot z naliczeniem kary, aktywne wypożyczenia użytkownika,
lista przeterminowanych wypożyczeń, wyświetlenie raportu końcowego oraz scenariusz demonstracyjny.

ConsoleUI
Pętla menu (switch po wyborze użytkownika), która tylko deleguje wywołania do powyższych handlerów. 
Dzięki temu Program.cs nie trzyma logiki biznesowej.

Program
Składa serwisy i warstwę UI (CatalogServices, RentalServices, ReportServices, ConsoleInput, handlery, ConsoleUI),
a następnie wywołuje ui.Run().

Kohezja, sprzężenie i decyzje projektowe
Model domenowy (Domain) nie zna serwisów ani UI. Serwisy pracują wyłącznie na obiektach domenowych (User, Equipment,
Rental), a UI korzysta z publicznego API serwisów. To obniża coupling między warstwami.

Logika jest podzielona według odpowiedzialności:

CatalogServices – dane katalogowe (użytkownicy, sprzęt),

RentalServices – proces wypożyczeń, limity, kary,

ReportServices – raportowanie,

ConsoleInput + handlery + ConsoleUI – wyłącznie interakcja z użytkownikiem.

Każdą z tych klas można opisać jednym zdaniem, co pokazuje dążenie do wysokiej kohezji.

Reguły biznesowe, które mogą się zmieniać (limity dla typów użytkowników, sposób naliczania kary), są skupione w
RentalServices, a nie rozproszone po UI czy modelu domenowym – dzięki temu zmiana reguł nie wymaga edycji wielu plików.

Filtrowanie raportów
Sprzęt dostępny
CatalogServices.GetAvailableEquipment() zwraca tylko sprzęt o statusie Available, a opcja menu 
„Wyświetl dostępny sprzęt” pokazuje raport oparty na tej liście (ReportServices.FormatEquipmentList(...)).

Przeterminowane wypożyczenia
RentalServices.GetOverdueRentals() zwraca tylko aktywne wypożyczenia po terminie, a opcja menu 
„Wyświetl przeterminowane wypożyczenia” pokazuje ich listę (ReportServices.FormatRentalsList(...)).
Informacja o liczbie przeterminowanych pojawia się także w raporcie podsumowującym.
