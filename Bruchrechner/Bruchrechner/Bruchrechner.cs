using System;

namespace Bruchrechner
{
    // Klasse um Brüche zu repräsentieren
    class Bruch
    {
        // Eigenschaften für Zähler und Nenner des Bruchs
        private int zaehler;
        private int nenner;

        // Konstruktor um einen Bruch mit Zähler und Nenner zu erstellen
        public Bruch(int zaehler, int nenner)
        {
            this.zaehler = zaehler;
            this.nenner = nenner;
            Kuerzen(); // Kürze den Bruch beim Erstellen
        }

        // Methode um einen Bruch auf den kleinstmöglichen gemeinsamen Nenner zu kürzen
        public void Kuerzen()
        {
            int ggt = GGT(Math.Abs(zaehler), Math.Abs(nenner)); // Größter gemeinsamer Teiler berechnen
            zaehler /= ggt;
            nenner /= ggt;
        }

        // Methode um den größten gemeinsamen Teiler zu berechnen
        private int GGT(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Überladung des Additionsoperators für Brüche
        public static Bruch operator +(Bruch bruch1, Bruch bruch2)
        {
            int neuerNenner = bruch1.nenner * bruch2.nenner;
            int neuerZaehler = bruch1.zaehler * bruch2.nenner + bruch2.zaehler * bruch1.nenner;
            return new Bruch(neuerZaehler, neuerNenner);
        }

        // Überladung des Subtraktionsoperators für Brüche
        public static Bruch operator -(Bruch bruch1, Bruch bruch2)
        {
            int neuerNenner = bruch1.nenner * bruch2.nenner;
            int neuerZaehler = bruch1.zaehler * bruch2.nenner - bruch2.zaehler * bruch1.nenner;
            return new Bruch(neuerZaehler, neuerNenner);
        }

        // Überladung des Multiplikationsoperators für Brüche
        public static Bruch operator *(Bruch bruch1, Bruch bruch2)
        {
            int neuerNenner = bruch1.nenner * bruch2.nenner;
            int neuerZaehler = bruch1.zaehler * bruch2.zaehler;
            return new Bruch(neuerZaehler, neuerNenner);
        }

        // Überladung des Divisionsoperators für Brüche
        public static Bruch operator /(Bruch bruch1, Bruch bruch2)
        {
            int neuerNenner = bruch1.nenner * bruch2.zaehler;
            int neuerZaehler = bruch1.zaehler * bruch2.nenner;
            return new Bruch(neuerZaehler, neuerNenner);
        }

        // Methode zur Berechnung der Wurzel eines Bruchs
        public double WurzelZiehen()
        {
            return Math.Sqrt((double)zaehler / nenner);
        }

        // Methode zur Berechnung der Potenz eines Bruchs
        public Bruch Potenzieren(int exponent)
        {
            int neuerZaehler = (int)Math.Pow(zaehler, exponent);
            int neuerNenner = (int)Math.Pow(nenner, exponent);
            return new Bruch(neuerZaehler, neuerNenner);
        }

        // Überschreiben der ToString Methode für die Darstellung des Bruchs
        public override string ToString()
        {
            return $"{zaehler}/{nenner}";
        }

        // Get und Set für den Zähler und Nenner
        public int Zaehler
        {
            get { return zaehler; }
            set
            {
                zaehler = value;
                Kuerzen();
            }
        }

        public int Nenner
        {
            get { return nenner; }
            set
            {
                if (value == 0)
                    throw new ArgumentException("Nenner darf nicht 0 sein.");
                nenner = value;
                Kuerzen();
            }
        }
    }

    class Start
    {
        // Methode zum Parsen von Benutzereingabe in Bruchobjekte
        static Bruch ParseBruch(string input)
        {
            string[] parts = input.Split('/');
            int zaehler = int.Parse(parts[0]);
            int nenner = int.Parse(parts[1]);
            return new Bruch(zaehler, nenner);
        }

        // Methode zur Durchführung der Rechenoperation basierend auf dem Operator
        static Bruch Berechne(Bruch operand1, string operation, Bruch operand2)
        {
            switch (operation)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand1 / operand2;
                case "^":
            int exponent = int.Parse(operand2.ToString()); // Parsen des Exponenten
            return new Bruch((int)Math.Pow(operand1.Zaehler, exponent), (int)Math.Pow(operand1.Nenner, exponent));
                default:
                    throw new ArgumentException("Ungültige Operation.");

            }
        }

        // Variable zum Speichern des letzten Ergebnisses 
        static Bruch LetztesErgebnis = null;

        // Hauptmethode des Programms
        static void Main(string[] args)
        {
            // Endlosschleife für die Benutzereingaben
            while (true)
            {
                // Benutzer zur Eingabe einer Rechenoperation oder zum Beenden des Programms auffordern
                Console.WriteLine("Geben Sie eine Rechnung ein (z.B. .../... + .../..., sqrt .../... für Wurzel oder .../... ^ n für Potenz) oder 'exit' zum Beenden: ");
                string input = Console.ReadLine();

                // Programm beenden, wenn Benutzer 'exit' eingibt
                if (input.ToLower() == "exit")
                    break;

                try
                {
                    Console.WriteLine($"Eingabe vor dem Aufteilen: {input}");


                    // Index des Potenzierungsoperators finden
                    int powerIndex = input.IndexOf('^');
                    if (powerIndex != -1)
                    {
                        // Teile der Eingabe definieren
                        string numeratorPart = input.Substring(0, powerIndex - 1).Trim();
                        string exponentPart = input.Substring(powerIndex + 1).Trim();

                        // Bruch und Exponent erstellen
                        Bruch operand_1 = ParseBruch(numeratorPart);
                        int exponent = int.Parse(exponentPart);

                        // Berechnung der Potenz
                        Bruch result1 = operand_1.Potenzieren(exponent);
                        Console.WriteLine($"Ergebnis der Potenzierung: {result1}");
                        continue;
                    }

                    // Andernfalls wie gewohnt fortfahren
                    // Eingabe in Teile zerlegen und Bruchobjekte erstellen
                    string[] parts = input.Split(' ');
                    if (parts.Length == 1 && parts[0].ToLower() == "exit") // Überprüfung für exit-Befehl
                        break;

                    Bruch operand1;
                    string operation;
                    Bruch operand2;

                    if (parts[0].ToLower() == "sqrt") // Wurzelziehen
                    {
                        operand1 = ParseBruch(parts[1]);
                        double wurzel = operand1.WurzelZiehen();
                        Console.WriteLine($"Ergebnis der Wurzelziehung: {wurzel}");
                        continue;
                    }
                    else // Standard-Rechenoperationen
                    {
                        operand1 = ParseBruch(parts[0]);
                        operation = parts[1];
                        operand2 = ParseBruch(parts[2]);
                    }

                    // Berechnung des Ergebnisses basierend auf der Operation
                    Bruch result = Berechne(operand1, operation, operand2);
                    Console.WriteLine("Ergebnis: " + result);

                    // Letztes Ergebnis speichern
                    LetztesErgebnis = result;
                }
                catch (Exception ex)
                {
                    // Fehlermeldung anzeigen, wenn eine Ausnahme auftritt
                    Console.WriteLine("Fehler: " + ex.Message);
                }
            }
        }


    }
}
