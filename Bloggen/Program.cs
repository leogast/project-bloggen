using System;
using System.Collections.Generic;
using System.Threading;

namespace Bloggen
{
    class Program
    {

        /************************************ Bloggen ****************************************
        -Jag planerade uppgiften med aktivitetsdiagrammet och pseudokod. När jag började
        att skriva programmet ansåg jag att det var många fler detaljer som behövde inkluderas
        för att göra programmet mer användbart (snyggare) för både användaren och för de som
        läser min kod. När jag gjorde aktivitetsdiagram och pseudokod kändes det som jag var redo
        men när jag började med själva kodningen blev det mer och mer komplicerat.

        -Jag har skapat flera metoder för programmet som kan användas mer än en gång.

        -Det finns flera olika typer av felhantering som till exempel: TRYPARSE, DEFAULT, ELSE,
        IsNullOrWhiteSpace, ToUpper.

        -Jag har använt linjär och binär sökning. Dessutom tyckte jag att det var en bra ide att
        inkludera metoden BubbelSortering innan varje sökning för undvika att användaren
        behöver stoppa sökning och gå tillbaka till meny för att sortera först.

        -Det finns ett tredje element i blogginlägg som sparar datum och tid.

        -Jag har testat programmet med en riktig användare och jag gjorde några förbättringar
        efter det som:
                        -visualisering av alla blogginlägg innan redigering och radering för
                        att underlätta;
                        -ändring datumvisning till svenskt format;
                        -förtydligande av några meddelanden som presenteras till användaren.
                        -inkluderade en Thread.Sleep(3000) metod så att programmet stängas av
                        efter 3 sekunder.
        ***************************************************************************************/


        /*----------Initierar en lista som innehåller strängvektorer.
        Skapar listan här på grund av variabelns livslängd.*/
        static List<string[]> Bloggen = new List<string[]>();

        static void Main(string[] args)
        {
            //-Initierar en strängvektor som ska innehålla tre element titel, meddelande och datum.
            string[] blogginlägg = new string[3];

            //-Initierar en loop som ska styra när programmet avslutas.
            bool isRunning = true;

            //-Skapar WHILE-satsen för att styra loopen.
            while (isRunning)
            {
                //-Rensar konsolen.
                Console.Clear();

                //-Skriver ut menyn för användaren.
                Console.WriteLine("\n\t****** Välkommen till Bloggen! ******\n\t" +
                    "\n\tVad vill du göra?\n\n\t" +
                    "[1]Skriv nytt inlägg i Bloggen\n\t" +
                    "[2]Skriv ut alla blogginlägg\n\t" +
                    "[3]Redigera blogginlägg\n\t" +
                    "[4]Radera blogginlägg\n\t" +
                    "[5]Sortera Bloggen\n\t" +
                    "[6]Sök blogginlägg (linjär sökning)\n\t" +
                    "[7]Sök blogginlägg (binär sökning)\n\t" +
                    "[8]Avsluta programmet");

                //-Tar emot användarens input för meny.
                Console.Write("\n\tVälj [menyval]: ");

                //-Felhantering: konverterar string till int för att läsa in [menyval].
                Int32.TryParse(Console.ReadLine(), out int menyval);

                //-Deklarerar variabel som ska användas i SWITCH-satsen.
                string temp;
                int svar;

                //-Skapar SWITCH-satsen för [menyval]. 
                switch (menyval)
                {

                    //-----Skapar FALL 1 för menyval [1]Skriv nytt inlägg i Bloggen.
                    case 1:

                        //-Använder "NyttInlägg" och "AddTillBloggen" metoder för att skriva nytt blogginlägg.
                        blogginlägg = NyttInlägg();
                        AddTillBloggen(blogginlägg);

                        //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
                        BloggenUtskrift();

                        //-Presenterar meddelande till användaren.
                        Console.WriteLine("\n\t > > > > > Blogginlägget har sparats < < < < < ");

                        //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                        MenyAvslut();

                        //-Avslutar FALL 1.
                        break;


                    //-----Skapar FALL 2 för menyval [2]Skriv ut alla blogginlägg.
                    case 2:

                        //-Använder "BubbelSortering" metoden för att sortera Bloggen för en snyggare presentation.
                        BubbelSortering();

                        //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
                        BloggenUtskrift();

                        //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                        MenyAvslut();

                        //-Avslutar FALL 2.
                        break;


                    //-----Skapar FALL 3 för menyval [3]Redigera blogginlägg.
                    case 3:

                        /*-Skapar IF-satsen för att kontrollera
                        om det finns några blogginlägg.*/
                        if (Bloggen.Count > 0)
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
                            BloggenUtskrift();

                            //-Tilldelar värde till variabel som ska ta användarens input med metoden "SkrivHär".
                            temp = SkrivHär("titel du vill redigera");

                            //-Använder "BubbelSortering" metoden för att sortera Bloggen för en snyggare presentation.
                            BubbelSortering();

                            //-Tilldelar värde till variabel för att söka hela Bloggen med metoden "BinärSökning".
                            svar = BinärSökning(temp);

                            /*-Skapar IF-satsen för att kontrollera
                            om blogginlägg finns i Bloggen.*/
                            if (svar != -1)
                            {
                                //-Använder "RedigeraBlogg" metoden för att redigera blogginlägg.
                                RedigeraBlogginlägg(svar);

                                //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
                                BloggenUtskrift();
                            }

                            //-Skapar ELSE-satsen för felhantering.
                            else
                            {
                                //-Rensar konsolen.
                                Console.Clear();

                                //-Presenterar meddelande till användaren.
                                Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!! Blogginlägget finns inte !!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                                //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                                MenyAvslut();
                            }
                        }

                        //-Skapar ELSE-satsen för felhantering.
                        else
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Presenterar meddelande till användaren.
                            Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!! Bloggen är tom !!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();
                        }

                        //-Avslutar FALL 3.
                        break;


                    //-----Skapar FALL 4 för menyval [4]Radera blogginlägg.
                    case 4:

                        /*-Skapar IF-satsen för att kontrollera
                        om det finns några blogginlägg.*/
                        if (Bloggen.Count > 0)
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Använder "BubbelSortering" metoden för att sortera Bloggen för en snyggare presentation.
                            BubbelSortering();

                            //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
                            BloggenUtskrift();

                            //-Tilldelar värde till variabel som ska ta användarens input med metoden "SkrivHär".
                            temp = SkrivHär("titel du vill radera");

                            //-Tilldelar värde till variabel för att söka hela Bloggen med metoden "BinärSökning".
                            svar = BinärSökning(temp);

                            /*-Skapar IF-satsen för att kontrollera
                            om blogginlägg finns i Bloggen.*/
                            if (svar != -1)
                            {
                                //-Använder "BloggenUtskrift" metoden för att radera blogginlägg.
                                RaderaBlogginlägg(svar);

                                //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
                                BloggenUtskrift();
                            }

                            //-Skapar ELSE-satsen för felhantering.
                            else
                            {
                                //-Rensar konsolen.
                                Console.Clear();

                                //-Presenterar meddelande till användaren.
                                Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!! Blogginlägget finns inte !!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                                //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                                MenyAvslut();
                            }
                        }

                        //-Skapar ELSE-satsen för felhantering.
                        else
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Presenterar meddelande till användaren.
                            Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!! Bloggen är tom !!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();
                        }

                        //-Avslutar FALL 4.
                        break;


                    //-----Skapar FALL 5 för menyval [5]Sortera Bloggen.
                    case 5:

                        /*-Skapar IF-satsen för att kontrollera
                        om det finns några blogginlägg.*/
                        if (Bloggen.Count > 0)
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Använder "BubbelSortering" metoden för att sortera Bloggen.
                            BubbelSortering();

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();
                        }

                        //-Skapar ELSE-satsen för felhantering.
                        else
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Presenterar meddelande till användaren.
                            Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!! Bloggen är tom !!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();
                        }

                        //-Avslutar FALL 5.
                        break;


                    //-----Skapar FALL 6 för menyval [6]Sök blogginlägg (linjär sökning).
                    case 6:

                        /*-Skapar IF-satsen för att kontrollera
                        om det finns några blogginlägg.*/
                        if (Bloggen.Count > 0)
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Tilldelar värde till variabel som ska ta användarens input med metoden "SkrivHär".
                            temp = SkrivHär("titel du söker efter");

                            //-Tilldelar värde till variabel för att söka igenom hela Bloggen med metoden "LinjärSökning".
                            svar = LinjärSökning(temp);

                            /*-Skapar IF-satsen för att kontrollera
                            om blogginlägg finns i Bloggen.*/
                            if (svar != -1)
                            {
                                //-Presenterar meddelande till användaren.
                                Console.WriteLine("\n\t > > > > > > Blogginlägget hittades < < < < < < ");

                                //-Använder "SökningUtskrift" metoden för att skriva ut sökresultat.
                                SökningUtskrift(svar);
                            }

                            //-Skapar ELSE-satsen för felhantering.
                            else
                            {
                                //-Rensar konsolen.
                                Console.Clear();

                                //-Presenterar meddelande till användaren om blogginlägg inte finns.
                                Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!! Blogginlägget finns inte !!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            }

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();

                        }

                        //-Skapar ELSE-satsen för felhantering.
                        else
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Presenterar meddelande till användaren.
                            Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!! Bloggen är tom !!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();
                        }

                        //-Avslutar FALL 6.
                        break;


                    //-----Skapar FALL 7 för menyval [7]Sök blogginlägg (binär sökning).
                    case 7:

                        /*-Skapar IF-satsen för att kontrollera
                        om det finns några blogginlägg.*/
                        if (Bloggen.Count > 0)
                        {

                            //-Rensar konsolen.
                            Console.Clear();

                            //-Tilldelar värde till variabel som ska ta användarens input med metoden "SkrivHär".
                            temp = SkrivHär("titel du vill söka efter");

                            //-Tilldelar värde till variabel för att söka igenom hela Bloggen med metoden "BinärSökning".
                            svar = BinärSökning(temp);

                            /*-Skapar IF-satsen för att kontrollera
                            om blogginlägg finns i Bloggen.*/
                            if (svar != -1)
                            {
                                //-Presenterar meddelande till användaren.
                                Console.WriteLine("\n\t > > > > > > Blogginlägget hittades < < < < < < ");

                                //-Använder "SökningUtskrift" metoden för att skriva ut sökresultat.
                                SökningUtskrift(svar);
                            }

                            else
                            {
                                //-Rensar konsolen.
                                Console.Clear();

                                //-Presenterar meddelande till användaren.
                                Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!! Blogginlägget finns inte !!!!!!!!!!");
                                Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            }

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();
                        }

                        //-Skapar ELSE-satsen för felhantering.
                        else
                        {
                            //-Rensar konsolen.
                            Console.Clear();

                            //-Presenterar meddelande till användaren.
                            Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!! Bloggen är tom !!!!!!!!!!!!!!!");
                            Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                            MenyAvslut();
                        }

                        //-Avslutar FALL 7.
                        break;


                    //-----Skapar FALL 8 för menyval [8]Avsluta programmet.
                    case 8:

                        //-Avslutar loopen genom att tilldela värde "false" till "isRunning".
                        isRunning = false;

                        //-Rensar konsolen.
                        Console.Clear();

                        //-Presenterar meddelande till användaren.
                        Console.WriteLine("\n\t* * * * * Tack för den här gången! * * * * *");

                        //-Väntar 3 sekunder innan programmet avslutas.
                        Thread.Sleep(3000);

                        //-Rensar konsolen.
                        Console.Clear();

                        //-Avslutar FALL 8.
                        break;


                    //-----Felhantering: skapar DEFAULT-sats ifall användaren skriver ett ogiltigt [menyval].
                    default:

                        //-Rensar konsolen.
                        Console.Clear();

                        //-Presenterar meddelande till användaren.
                        Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine("\t! Du måste välja en siffra av menyvalen[1-8] !");
                        Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                        //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                        MenyAvslut();

                        //-Avslutar DEFAULT.
                        break;

                }
            }
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>     METODER     <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


        //--------------Skapar en metod som matar in nya blogginlägg.
        static string[] NyttInlägg()
        {
            //-Rensar konsolen.
            Console.Clear();

            //-Initierar en strängvektor som innehåller tre element.
            string[] inlägg = new string[3];

            //-Användar "SkrivHär" metoden för att skapa första position i vektorn (titel).
            inlägg[0] = SkrivHär("titel");

            //-Användar "SkrivHär" metoden för att skapa andra position i vektorn (Blögginlägg).
            inlägg[1] = SkrivHär("text");

            //-Användar "DateTime" metoden för att skapa tredje position i vektorn (datum).
            inlägg[2] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            /*-Felhantering: skapar IF-satsen för att kontrollera
            om användaren har skrivit "titel" och "text".*/
            if (!string.IsNullOrWhiteSpace(inlägg[0]) && !string.IsNullOrWhiteSpace(inlägg[1]))

                //-Returnera variabel "inlägg" för att spara blogginlägg i Bloggen.
                return inlägg;

            //-Skapar ELSE-satsen för felhantering.
            else
            {
                //-Rensar konsolen.
                Console.Clear();

                //-Presenterar meddelande till användaren.
                Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.WriteLine("\t!!!!!!!!!! Blogginlägget finns inte !!!!!!!!!!");
                Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
                MenyAvslut();

                //-Returnera metoden "NyttInlägg" så användaren kan försöka igen.
                return NyttInlägg();
            }
        }


        //----------Skapar en metod som adderar blogginlägg i Bloggen.
        static void AddTillBloggen(string[] inlägg)
        {
            //-Adderar blogginlägg till Bloggen.
            Bloggen.Add(inlägg);
        }


        //------------Skapar en metod för att spara vad användaren har skrivit in.
        static string SkrivHär(string textHär)
        {
            //-Be användaren skriva inlägg.
            Console.Write("\n\tSkriv " + textHär + " här: ");

            //-Sparar vad användaren har skrivit.
            string text = Console.ReadLine();

            //-Returnera variabel "text" med metoden "ToUpper" för felhantering.
            return (text);
        }


        //----------Skapar en metod som skriver ut alla blogginlägg.
        static void BloggenUtskrift()
        {
            //-Rensar konsolen.
            Console.Clear();

            //-Initierar en strängvektor som innehålla tre element.
            string[] inlägg = new string[3];

            /*-Skapar IF-satsen för att kontrollera
            om det finns några blogginlägg.*/
            if (Bloggen.Count > 0)
            {
                //-Presenterar meddelande till användaren.
                Console.WriteLine("\n\t* * * * * Lista med alla blogginlägg * * * * *");

                //-Skapar en FOR-loop som ska skanna hela Bloggen.
                for (int i = 0; i < Bloggen.Count; i++)
                {
                    //-Tilldelar värde till "inlägg" så många gånger som det finns blogginlägg.
                    inlägg = Bloggen[i];

                    //-Presenterar meddelande till användaren och skriver ut alla blogginlägg.
                    Console.WriteLine("\n\t--------------- Blogginlägg # " + (i + 1) +
                                      " \n\tTitel: " + inlägg[0] +
                                      " \n\tText: " + inlägg[1] +
                                      " \n\tDatum: " + inlägg[2]);
                }
            }

            //-Skapat ELSE-satsen för felhantering.
            else
            {
                //-Presenterar meddelande till användaren.
                Console.WriteLine("\n\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Console.WriteLine("\t!!!!!!!!!!!!!!! Bloggen är tom !!!!!!!!!!!!!!!");
                Console.WriteLine("\t!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }


        //----------Skapar en metod som undvikar upprepande kod.
        static void MenyAvslut()
        {
            //-Presenterar meddelande till användaren.
            Console.WriteLine("\n\t!!!!!!!! Tryck ENTER för att fortsätta !!!!!!!!");

            //-Väntar på användaren.
            Console.ReadLine();
        }

        //----------Skapar en metod som redigerar blogginlägg.
        static void RedigeraBlogginlägg(int i)
        {
            //-Rensar konsolen
            Console.Clear();

            //-Initierar en strängvektor som innehålla tre element.
            string[] inlägg = new string[3];

            //-Användar "SkrivHär" metoden för att redigera första position i vektorn (titel).
            inlägg[0] = SkrivHär("en ny titel");

            //-Användar "SkrivHär" metoden för att redigera andra position i vektorn (text).
            inlägg[1] = SkrivHär("en ny text");

            //-Användar "DateTime" metoden för att redigera tredje position i vektorn (datum).
            inlägg[2] = DateTime.Now.ToString("yyyy-MM-dd HH:MM");

            //-Skicka det redigerade blogginlägget till listan.
            Bloggen[i] = inlägg;

            //-Rensar konsolen
            Console.Clear();

            //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
            BloggenUtskrift();

            //-Presenterar meddelande till användaren.
            Console.WriteLine("\n\t > > > > > Blogginlägget redigerades < < < < < ");

            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
            MenyAvslut();
        }


        //----------Skapar en metod som raderar blogginlägg.
        static void RaderaBlogginlägg(int i)
        {
            //-Rensar konsolen
            Console.Clear();

            //-Använder "RemoveAt" metoden för att radera blogginlägg.
            Bloggen.RemoveAt(i);

            //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
            BloggenUtskrift();

            //-Presenterar meddelande till användaren.
            Console.WriteLine("\n\t> > > > > > Blogginlägget raderades < < < < < <");

            //-Använder "MenyAvslut" metoden för standardiserad menyavslutning.
            MenyAvslut();
        }


        //----------Skapar en metod som sorterar alla blogginlägg.
        static void BubbelSortering()
        {

            //-Rensar konsolen
            Console.Clear();

            //-Initierar "max" variabel som ska användas i FOR-loopen.
            int max = Bloggen.Count - 1;

            //-Skapar en FOR-loop som ska köras så många gånger det finns blogginlägg i listan.
            for (int i = 0; i < max; i++)
            {
                //-Skapar en FOR-loop som ska köras så många gånger det finns blogginlägg i listan - i.
                for (int index = 0; index < max - i; index++)
                {
                    /*-Initierar två strängvariabler som ska jämföra två intilliggande titlar med varandra.
                    Tilldelar ett värde till en tillfällig variabel "int" som med
                    metoden "CompareTo" jämför om ena titeln är tidigare (alfabetiskt) än den andra.*/
                    string[] titel1 = Bloggen[index];
                    string[] titel2 = Bloggen[index + 1];
                    int tmp = titel1[0].CompareTo(titel2[0]);

                    /*-Om "tmp" är större än 0 så ska blogginlägg med nuvarande index byta plats med
                    blogginlägg med näst kommande index.*/
                    if (tmp > 0)
                    {
                        //-Initierar strängvariabel som ska användas för platsbyte.
                        String[] temp = Bloggen[index];
                        Bloggen[index] = Bloggen[index + 1];
                        Bloggen[index + 1] = temp;
                    }
                }
            }

            //-Använder "BloggenUtskrift" metoden för att skriva ut alla blogginlägg.
            BloggenUtskrift();

            //-Presenterar meddelande till användaren.
            Console.WriteLine("\n\t > > > > > > Bloggen har sorterats < < < < < < ");
        }


        //---------Skapar en metod som söker blogginlägg med linjär sökning.
        static int LinjärSökning(string text)
        {
            //-Rensar konsolen.
            Console.Clear();

            /*-Skapar en for-loop som ska gå inom hela Bloggens "Length" för att söka innehållet.
            Initieringsvärde: 0, Villkor: hela Bloggens längd, Ökningsvärde: ökar värdet med 1 för varje varv.*/
            for (int i = 0; i < Bloggen.Count; i++)
            {
                /*-Jämför inmatat sökord med variabel "i" för att kontrollera om blogginlägg finns i Bloggen.
                Felhantering:använder "ToUpper" metoden som gör att alla bokstäver blir stora oavsett om
                användaren skriver med små eller stora bokstäver.*/
                string[] inlägg = Bloggen[i];
                if (inlägg[0].ToUpper() == text.ToUpper())

                    //-Returnera sökresultat med variabel "i".
                    return i;
            }

            //Avslutar loopen.
            return -1;
        }


        //---------Skapar en metod som söker blogginlägg med binär sökning.
        static int BinärSökning(string text)
        {
            //-Använder "BubbelSortering" metoden för att sortera Bloggen för att kunna göra en binär sökning
            BubbelSortering();

            //-Rensar konsolen.
            Console.Clear();

            //-Initierar variabel "första" och "sista" som är första och sista blogginlägg på listan.
            int första = 0;
            int sista = Bloggen.Count - 1;

            //-Skapat WHILE-satsen för att styra loopen som körs så länge "första" är mindre än "sista".
            while (första <= sista)
            {
                //-Initierar variabel "mellan" som är medelvärde mellan "första" och "sista".
                int mellan = (första + sista) / 2;

                //-Läggar variabel "mellan" i listan.
                string[] bokstav = Bloggen[mellan];

                //-Använder "CompareTo" metoden för att plocka ut enstaka bokstav från strängar för jämförelser.
                //int temp = text.CompareTo(bokstav[0]);
                int temp = text.ToUpper().CompareTo(bokstav[0].ToUpper());

                //-Om variable "temp" är större än noll ska "första" vara lika med "mellan" + 1.
                if (temp > 0)
                    första = mellan + 1;

                //-Om variable "temp" är mindre än noll ska "sista" vara lika med "mellan" - 1.
                else if (temp < 0)
                    sista = mellan - 1;

                //-Loopen avslutar när "temp" blir lika med noll.
                else
                {
                    //-Returnera sökresultat som finns på element "mellan".
                    return mellan;
                }
            }

            //Avslutar loopen.
            return -1;
        }


        //----------Skapar en metod som skriver ut ett blogginlägg.
        static void SökningUtskrift(int i)

        {
            //-Lägger elementet i listan.
            string[] inlägg = Bloggen[i];

            //-Presenterar meddelande till användaren och skriver ut blogginlägg.
            Console.WriteLine("\n\t-------------- Blogginlägg # " + (i + 1) +
                              " \n\tTitel: " + inlägg[0] +
                              " \n\tText: " + inlägg[1] +
                              " \n\tDatum: " + inlägg[2]);
        }
    }
}