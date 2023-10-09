using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubrica {

    //Classe per contenere stringhe equivalenti a numeri telefonici di dieci cifre
    public  class NumList {

        private List<string> list;

        //Questa funzione controlla la dimensione e il contenuto dei numeri
        Func<string, bool> isNum = s => s.Length == 10
         && s.Where(c => c >= '0' && c <= '9').Count() == 10;


        public NumList() {
            list = new List<string>();
        }

        public NumList(string s) : this() {
            AddNumber(s);
        }

        public NumList(List<string> s) : this() {
            AddNumber(s);
        }

        //aggiunge un numero solo e solo se è coerente e non presente
        public bool AddNumber(string s) {
            if (isNum(s) && !list.Contains(s)) {
                list.Add(s);
                return true;
            }
            return false;
        }

        public bool AddNumber(List<string> sl) {
            if(sl.Count == 0) return false;
            return sl.Select(s => AddNumber(s)).Aggregate((a, b) => a && b);
        }

        public bool RemoveNumber(string s) {
            return list.Remove(s);
        }

        public bool RemoveNumber(List<string> sl) {
            return sl.Select(s => RemoveNumber(s)).
            Aggregate((a, b) => a && b);
        }

        public bool FindNumber(string n) {
            return list.Contains(n);
        }

        public List<string> GetNumList() {
            List<string> l = new List<string>();
            list.ForEach(s => l.Add(s));
            return l;
        }

        public bool IsEmpy() {
            return list.Count == 0;
        }

        public string Lenght(int n) {
            return n < list.Count ? list[n] : "null";
        }

        public string GetAllNumbers() {
            return list.Count > 0 ? list.Aggregate((a, b) => a + "\n" + b) : "";
        }
    }
}
