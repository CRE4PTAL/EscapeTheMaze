# EscapeTheMaze - 3D Puzzle Platformer 🏃‍♂️

Immersyjna gra 3D stworzona w silniku Unity, łącząca elementy zręcznościowe (platformer) z rozwiązywaniem zagadek logicznych. Zadaniem gracza jest wydostanie się z labiryntu pełnego śmiertelnych pułapek. Gra wyróżnia się dopracowaną oprawą wizualną dzięki wykorzystaniu wysokiej jakości tekstur i modeli 3D.

## 🛠 Technologie

* **Silnik:** Unity (3D)
* **Język:** C#
* **Grafika:** Modele i tekstury High-Fidelity (własna kompozycja assetów)
* **Fizyka:** Unity Physics & Character Controller

## 🌟 Główne funkcjonalności

### 🏃 System Ruchu i Wytrzymałości
Zaawansowany kontroler postaci oferuje płynne poruszanie się, skakanie i bieganie.
* **Pasek Staminy:** Bieganie (Shift) oraz skoki zużywają wytrzymałość.
* **Zmęczenie:** Gdy pasek staminy spadnie do zera, gracz traci możliwość sprintu do momentu regeneracji sił.

### 💀 Pułapki i Przeszkody
Labirynt najeżony jest różnorodnymi zagrożeniami:
* **Kolczasta Ściana:** Pułapka mechaniczna, która nie tylko zadaje obrażenia, ale również fizycznie odpycha gracza (Knockback).
* **Lava Zones:** Paski lawy na podłodze zadające obrażenia w czasie (Damage over Time) przy dłuższym kontakcie.
* **Kula w Korytarzu:** Przeszkoda dynamiczna poruszająca się w pętli (tam i z powrotem) – wymaga wyczucia czasu (timing), aby uniknąć uderzenia.
* **Strefa Parkour:** Sekcja zręcznościowa nad zbiornikiem lawy. Upadek oznacza natychmiastową śmierć (Insta-Kill).

### 🔐 Logika Gry i UI
* **System Kluczy:** Aby otworzyć drzwi, gracz musi odnaleźć i zebrać 2 ukryte klucze.
* **System Zdrowia:** Pasek HP reaguje na obrażenia z pułapek. Śmierć aktywuje ekran "Game Over".
* **Checkpointy:** Stan gry jest zapisywany w kluczowych momentach. Po śmierci gracz odradza się w ostatnim bezpiecznym punkcie lub może wrócić do Menu Głównego.

## 💡 Wyzwania i rozwiązania

Podczas tworzenia gry skupiłem się na responsywności i mechanikach "Game Feel".

* **Zarządzanie Staminą:** Zaimplementowałem logikę, która dynamicznie blokuje input sprintu, gdy wartość zmiennej `currentStamina` spadnie poniżej progu, oraz automatycznie regeneruje ją, gdy gracz odpoczywa.
* **Interakcja z Checkpointami:** Stworzyłem menedżera gry, który przechowuje pozycję ostatniego checkpointa i w momencie przeładowania sceny (respawnu) ustawia gracza w zapamiętanych współrzędnych.

## 🎮 Sterowanie

| Klawisz | Akcja |
| :--- | :--- |
| **W, A, S, D** | Poruszanie się |
| **Spacja** | Skok |
| **Shift (Przytrzymaj)** | Bieganie (wymaga staminy) |
| **Mysz** | Rozglądanie się |
| **Ctrl** | Kucanie (nie potrzebne, ale dodałem) |
| **R** | Odjęcie skrawka HP (dodane w ramach testowania checkpointów i Game Over Screen) |

## 💻 Uruchomienie projektu

1. Wejdź w zakładkę Releases (po prawej stronie ekranu)
2. Kliknij "Escape The Maze - Playable Build"
3. Pobierz ZIP'a i go rozpakuj
4. Uruchom Labirynt.exe
