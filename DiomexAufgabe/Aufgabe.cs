using System.Collections.Generic;

namespace DiomexAufgabe
{
    public  class Aufgabe
    {
        public List<string> Liste { get; set; }
        public string errorMessage { get; set; }
        public bool hasErrors  { get; set; }
        public Aufgabe(List<string> _Liste)
        {
            Liste = _Liste;
        }
    }
}