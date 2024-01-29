using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiomexAufgabe
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /*Variablen*/
        List<string> Seperators = new List<string>() { "(", ")" };
        List<string> RechenOperators = new List<string>() { "+", "-", "*", "/" };
        List<string> InBuiltFunktionen = new List<string>() { "Str", "Val" };
        /*Variablen*/

        //Versuch den Text zu lösen
        private void buttonLosung_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            Aufgabe Aufgabe = AufgabeZurLsite(textBoxFormelInput.Text);
            if (Aufgabe.hasErrors)
            {
                richTextBox1.Text += (Aufgabe.errorMessage);//Der Text hat Fehler.
            }
            else
            {
                //Der Text ist gelöst
                richTextBox1.Text = ("Die Lösung für \n" + textBoxFormelInput.Text + " ist :\n" + LoseDieAufgabe(Aufgabe.Liste)[0]);
            }
        }

        #region MainLosung
        //Der Main für die Lösung
        private List<string> LoseDieAufgabe(List<string> Liste)
        {
            Liste = EntferneLehreStellen(Liste);
            Liste = FindeVariableWerte(Liste);
            while (Liste.Count() > 1)//Bis die Liste eine Eingabe hat Brutforce einüben
            {
                Liste = FindeLosbareFormeln(Liste);//Alle möglichen Funktionen werden gelöst
                Liste = EntferneLehreStellen(Liste);

                //Der Reihe nach werden open-closed Klammern gelöst
                Liste = FindeLosbareKlammerInhalte(Liste);
                Liste = EntferneLehreStellen(Liste);


                //Falls keine Klammern oder Funktionen mehr da sind werden Grundrechnungen gemacht
                int FormelnZahl = 0;
                int KlammernIndex = Liste.IndexOf("(");
                for (int i = 0; i < Liste.Count; i++)
                {
                    if (InBuiltFunktionen.Contains(Liste[i]))
                    {
                        FormelnZahl++;
                    }
                }

                if (FormelnZahl == 0 && KlammernIndex == -1)
                {
                    Liste = MacheGrundRechnungen(Liste);
                }
            }


            return Liste;
        }
        private List<string> FindeLosbareFormeln(List<string> liste)
        {
            // Es wird in der Liste von hinten angefangen
            // Fall eine Stelle ein Funktionname ist und von einem wert folgt wird sie gelöst
            // Der Funktionname wird mit der Lösung ersetzt
            for (int i = liste.Count() - 1; i >= 0; i--)
            {
                string item = liste[i];
                if (InBuiltFunktionen.Contains(item) && liste[i + 1] != null && liste[i + 1] != "(")
                {
                    liste[i] = LoseFormel(liste[i], liste[i + 1]);
                    liste[i + 1] = " ";
                }
            }
            liste = EntferneLehreStellen(liste);
            return liste;
        }
        private List<string> FindeLosbareKlammerInhalte(List<string> liste)
        {
            List<int> OpenParanthesis = new List<int>();

            for (int i = 0; i < liste.Count; i++)
            {
                string item = liste[i];

                if (item == "(")
                    OpenParanthesis.Add(i);
                if (item == ")" && OpenParanthesis.Count() > 0)
                {
                    //Eine Klammer ist geschlossen
                    List<string> KlammerInhalt = new List<string>();
                    for (int j = OpenParanthesis.Last() + 1; j < i; j++)
                    {
                        KlammerInhalt.Add(liste[j]);
                        liste[j] = " ";
                    }
                    liste[OpenParanthesis.Last()] = " ";
                    liste[i] = " ";

                    OpenParanthesis.RemoveAt(OpenParanthesis.Count - 1);

                    //Der KlammerInhalt wird gelöst und replaced mit dem result
                    liste[i] = LoseDieAufgabe(KlammerInhalt)[0];
                }
            }

            return liste;
        }       
        private List<string> MacheGrundRechnungen(List<string> liste)
        {          
            if (RechenOperators.Contains(liste.First()))
                liste[0] = " ";
            if (RechenOperators.Contains(liste.Last()))
                liste[liste.Count - 1] = " ";
            liste = EntferneLehreStellen(liste);
            //Punkt- vor Strichrechnung


            //1. Alle Punktrechnungen
            for (int i = 1; i < liste.Count - 1; i++)
            {
                double val1 = 0;
                double val2 = 0;
                bool val1IsNum = Double.TryParse(liste[i - 1], out val1);
                bool val2IsNum = Double.TryParse(liste[i + 1], out val2);
                string result = "";
                if (liste[i] == "*" || liste[i] == "/")
                {
                    switch (liste[i])
                    {
                        case "*":
                            if (val1IsNum && val2IsNum)
                            {
                                //Einfache Multiplikation 
                                result = GrundRechnungen.Multiplikation(val1, val2).ToString();
                            }
                            if (!val1IsNum && val2IsNum)
                            {
                                //Multiplikation von Str und Double 
                                result = GrundRechnungen.MultiplikationString(liste[i - 1], val2).ToString();
                            }
                            if (val1IsNum && !val2IsNum)
                            {
                                //Multiplikation von Str und Double 
                                result = GrundRechnungen.MultiplikationString(liste[i + 1], val1).ToString();
                            }
                            break;
                        case "/":
                            if (val1IsNum && val2IsNum)
                            {
                                if (val2IsNum && val2 == 0)
                                {
                                    //Versuch durch 0 zu teilen-> Fehler
                                    //Erster wert wird als lösung angenommen
                                    result = val1.ToString();
                                    MessageBox.Show("Logik error ("+val1.ToString()+"/"+val2.ToString()+")");                                   
                                    break;
                                }

                                result = GrundRechnungen.Division(val1, val2).ToString();
                            }
                            if (!val1IsNum || !val2IsNum)
                            {
                                //Versuch String durch Zahl zu teilen-> Fehler
                                //Beide werte werden  als lösung addiert
                                MessageBox.Show("String Divison Error, wird ignoriert");
                                result = GrundRechnungen.AdditionString(liste[i - 1], liste[i + 1]);
                            }
                            break;
                    }
                    liste[i + 1] = result.ToString();
                    liste[i - 1] = "";
                    liste[i] = "";
                }

            }
            liste = EntferneLehreStellen(liste);

            //Alle Strichrechnungen
            for (int i = 1; i < liste.Count - 1; i++)
            {
                double val1 = 0;
                double val2 = 0;
                bool val1IsNum = Double.TryParse(liste[i - 1], out val1);
                bool val2IsNum = Double.TryParse(liste[i + 1], out val2);
                string result = "";

                if (liste[i] == "+" || liste[i] == "-")
                {
                    switch (liste[i])
                    {
                        case "+":
                            if (val1IsNum && val2IsNum)
                            {
                                result = GrundRechnungen.Addition(val1, val2).ToString();
                            }
                            if (!val1IsNum || !val2IsNum)
                            {
                                result = GrundRechnungen.AdditionString(liste[i - 1], liste[i + 1]).ToString();
                            }
                            break;
                        case "-":
                            if (val1IsNum && val2IsNum)
                            {
                                result = GrundRechnungen.Subtraktion(val1, val2).ToString();
                            }
                            if (!val1IsNum || !val2IsNum)
                            {
                                MessageBox.Show("String Subtraktion Logik Error wird ignoriert");
                                result = GrundRechnungen.AdditionString(liste[i - 1], liste[i + 1]).ToString();
                            }
                            break;
                    }
                    liste[i + 1] = result.ToString();
                    liste[i - 1] = "";
                    liste[i] = "";
                }
            }
            liste = EntferneLehreStellen(liste);

            return liste;
        }
        #endregion MainLosung

        #region HelperFunktionen

        //Lehre Eingaben in der Liste werden entfernt und die Liste Wird kürzer
        private List<string> EntferneLehreStellen(List<string> liste)
        {
            List<string> Liste = new List<string>();
            foreach (string item in liste)
            {
                if (item.Trim() != "")
                    Liste.Add(item.Trim());
            }
            return Liste;
        }

        /*
         1. Stücke in der Liste die kein Text, keine Zahl oder eine Funktion sind werden gefunden
         2. Jede variable wird mit Hilfe des Inputforms mit String oder Zahlen ersetzt
         */
        private List<string> FindeVariableWerte(List<string> liste)
        {
            for (int i = 0; i < liste.Count; i++)
            {
                string item = liste[i];
                double number;

                bool isString = false;
                if (item.First() == '\"' && item.Last() == '\"')
                    isString = true;


                if (!Seperators.Contains(item) && !RechenOperators.Contains(item) &&
                    !InBuiltFunktionen.Contains(item) && !double.TryParse(item, out number) && !isString)
                {
                    InputForm form2 = new InputForm();
                    form2.WertName = item;
                    form2.ShowDialog();
                    liste[i] = form2.Wert;
                }

            }
            return liste;
        }


        //Der Text wird zur Liste verwandelt und logik kontrollen gemacht
        private Aufgabe AufgabeZurLsite(string musterFormel)
        {
            List<string> FormelInStucken = new List<string>();
            Aufgabe aufgabe = new Aufgabe(FormelInStucken);

            if (String.IsNullOrEmpty(musterFormel.Trim()))
            {
                aufgabe.hasErrors = true;
                aufgabe.errorMessage = "Formel hat keinen Inhalt!";
            }



            string stuck = "";
            bool inLine = false;
            bool paranthesisError = false;
            int paranthesisCount = 0;

            foreach (char c in musterFormel)
            {
                if (c.ToString() == "\"")
                    inLine = !inLine;

                if (c.ToString() == "(")
                    paranthesisCount++;
                if (c.ToString() == ")")
                {
                    paranthesisCount--;
                    if (paranthesisCount < 0)
                        paranthesisError = true;
                }
                if (c.ToString() == " " && !inLine)
                {
                    continue;
                }
                if (Seperators.Contains(c.ToString()) || RechenOperators.Contains(c.ToString()))
                {
                    FormelInStucken.Add(stuck);
                    FormelInStucken.Add(c.ToString());
                    stuck = "";
                }
                else
                    stuck += c.ToString();

            }
            FormelInStucken.Add(stuck);

            if (inLine)
            {
                aufgabe.hasErrors = true;
                aufgabe.errorMessage = "Formel hat Quote Error";
            }

            if (paranthesisCount != 0)
            {
                aufgabe.hasErrors = true;
                aufgabe.errorMessage = "Formel hat Klammernzahl Error";
            }

            if (paranthesisError)
            {
                aufgabe.hasErrors = true;
                aufgabe.errorMessage = "Formel hat Klammernlogik Error";
            }
            return aufgabe;
        }

        //Die Lösung der Inbuilt Funktionen
        private string LoseFormel(string v1, string v2)
        {
            string result = "";
            switch (v1)
            {
                case "Str":
                    result = "\"" + v2 + "\"";
                    break;
                case "Val":
                    result = Convert.ToDouble(v2.Replace("\"", "")).ToString();
                    break;
            }
            return result;
        }

        #endregion HelperFunktionen
    }
}
