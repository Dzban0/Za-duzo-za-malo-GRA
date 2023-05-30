using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Za_dużo__za_mało___GRA
{
    class Widok0
    {
        public const char ZNAK_ZAKONCZENIA_GRY = 'X';

        private Kontroler0 kontroler;

        public Widok0(Kontroler0 kontroler) => this.kontroler = kontroler;

        public void CzyscEkran() => Console.Clear();

        public void KomunikatPowitalny() => Console.WriteLine("Wylosowałem liczbę z zakresu ");

        public int WczytajPropozycje()
        {
            int wynik = 0;
            bool sukces = false;
            while (!sukces)
            {
                Console.Write("Podaj swoją propozycję (lub " + Kontroler0.ZNAK_ZAKONCZENIA_GRY + " aby przerwać): ");
                try
                {
                    string value = Console.ReadLine().TrimStart().ToUpper();
                    if (value.Length > 0 && value[0].Equals(ZNAK_ZAKONCZENIA_GRY))
                        throw new KoniecGryException();

                    //UWAGA: ponizej może zostać zgłoszony wyjątek 
                    wynik = Int32.Parse(value);
                    sukces = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Podana przez Ciebie wartość nie przypomina liczby! Spróbuj raz jeszcze.");
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Przesadziłeś. Podana przez Ciebie wartość jest zła! Spróbuj raz jeszcze.");
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("Nieznany błąd! Spróbuj raz jeszcze.");
                    continue;
                }
            }
            return wynik;
        }

        public void OpisGry()
        {
            Console.WriteLine("Gra w \"Za dużo za mało\"." + Environment.NewLine
                + "Twoimm zadaniem jest odgadnąć liczbę, którą wylosował komputer." + Environment.NewLine + "Na twoje propozycje komputer odpowiada: za dużo, za mało albo trafiłeś");
        }

        public bool ChceszKontynuowac(string prompt)
        {
            Console.Write(prompt);
            char odp = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return (odp == 't' || odp == 'T');
        }

        public void HistoriaGry()
        {
            if (kontroler.ListaRuchow.Count == 0)
            {
                Console.WriteLine("--- pusto ---");
                return;
            }

            Console.WriteLine("Nr    Propozycja     Odpowiedź     Czas    Status");
            Console.WriteLine("=================================================");
            int i = 1;
            foreach (var ruch in kontroler.ListaRuchow)
            {
                Console.WriteLine($"{i}     {ruch.Liczba}      {ruch.Wynik}  {ruch.Czas.Second}   {ruch.StatusGry}");
                i++;
            }
        }

        public void KomunikatZaDuzo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Za dużo!");
            Console.ResetColor();
        }

        public void KomunikatZaMalo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Za mało!");
            Console.ResetColor();
        }

        public void KomunikatTrafiono()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Trafiono!");
            Console.ResetColor();
        }
    }
}
