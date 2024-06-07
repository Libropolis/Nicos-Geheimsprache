using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nicos_Geheimsprache
{
    using System;

    class Program
    {


        static void Main()
        {
            int wahl = 0;
            Console.WriteLine("Möchtest du verschlüsseln<1> oder entschlüsseln<2>?");
            wahl = int.Parse(Console.ReadLine());
            if (wahl == 1)
            {
                Console.Write("Bitte geben Sie einen Satz ein: ");
                string eingabe = Console.ReadLine();

                string verschluesselteEingabe = Verschluesseln(eingabe);
                Console.WriteLine($"Verschlüsselter Satz: {verschluesselteEingabe}");
            }
            else if (wahl == 2)
            {
                Console.Write("Bitte geben Sie einen Satz ein: ");
                string eingabe = Console.ReadLine();

                string entschluesselteEingabe = Entschluesseln(eingabe);
                Console.WriteLine($"Entschlüsselter Satz: {entschluesselteEingabe}");
            }

        }

        static string Verschluesseln(string eingabe)
        {
            char[] vokale = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            string[] woerter = eingabe.Split(' ');

            for (int i = 0; i < woerter.Length; i++)
            {
                // Vertausche die ersten beiden Buchstaben
                string wort = woerter[i];
                wort = wort.Substring(1, 1) + wort.Substring(0, 1) + wort.Substring(2);

                string verschluesseltesWort = "";

                char letzterBuchstabe = '\0';

                foreach (char buchstabe in wort)
                {
                    if (Array.Exists(vokale, v => v == buchstabe))
                    {
                        // Füge ein B und den wiederholten Vokal ein, aber entferne den wiederholten Vokal
                        if (buchstabe != letzterBuchstabe)
                        {
                            verschluesseltesWort += $"{char.ToLower(buchstabe)}b{char.ToLower(buchstabe)}";
                            letzterBuchstabe = buchstabe;
                        }
                    }
                    else
                    {
                        verschluesseltesWort += buchstabe;
                        letzterBuchstabe = '\0'; // Zurücksetzen für Konsonanten
                    }
                }

                woerter[i] = verschluesseltesWort;
            }

            return string.Join(" ", woerter);
        }

        static string Entschluesseln(string verschluesselteEingabe)
        {
            char[] vokale = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            string[] woerter = verschluesselteEingabe.Split(' ');

            for (int i = 0; i < woerter.Length; i++)
            {
                string wort = woerter[i];
                string entschluesseltesWort = "";
                bool vokalGefunden = false;

                for (int j = 0; j < wort.Length; j++)
                {
                    char aktuellerBuchstabe = wort[j];

                    if (Array.Exists(vokale, v => char.ToLower(v) == char.ToLower(aktuellerBuchstabe)))
                    {
                        if (!vokalGefunden)
                        {
                            entschluesseltesWort += aktuellerBuchstabe;
                            vokalGefunden = true;
                        }
                    }
                    else if (aktuellerBuchstabe == 'b' && vokalGefunden)
                    {
                        // Überspringe das 'B', da es Teil des Musters ist
                    }
                    else
                    {
                        entschluesseltesWort += aktuellerBuchstabe;
                        vokalGefunden = false;
                    }
                }

                // Vertausche die Anfangsbuchstaben
                if (entschluesseltesWort.Length >= 3)
                {
                    entschluesseltesWort = entschluesseltesWort.Substring(1, 1) + entschluesseltesWort.Substring(0, 1) + entschluesseltesWort.Substring(2);
                }

                woerter[i] = entschluesseltesWort;
            }

            // Verbinde die Wörter zu einem Satz
            return string.Join(" ", woerter);
        }

    }
}