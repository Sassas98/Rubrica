using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Rubrica {

    //classe di gestione
    public class PhoneBookHandler {

        private PhoneBookData db;
        private ConsoleUI ui;
        private ActionDecoder decoder;

        public PhoneBookHandler() {
            db = new PhoneBookData();
            ui = new ConsoleUI();
            decoder = new ActionDecoder();
        }

        //fa partire il messaggio iniziale e di volta in volta prende le risposte e le elabora
        public void Start() {
            string s;
            for(var d = decoder.Decode(ui.GetMessage(0, "")); d.Item1 != Action.EXIT; d = decoder.Decode(s)){
                s = d.Item1 switch {
                    Action.HELP => ui.GetMessage(6, ""),
                    Action.NEW => ui.GetMessage(NewNumber(d.Item2), ""),
                    Action.SEARCH => ui.GetMessage(5, SearchNumbers(d.Item2)),
                    Action.DEL => ui.GetMessage(DeleteNumber(d.Item2), ""),
                    Action.ALL => ui.GetMessage(4, AllNumbers()),
                    Action.NONE => ui.GetMessage(1, ""),
                    _ => ""
                };
            }
        }

        //applica l'azione new
        private int NewNumber(List<string> items) {
            var d = FromListToDouple(items);
            return db.NewNumber(d.Item1, d.Item2) ? 3 : 2;
        }

        //applica l'azione search
        private string SearchNumbers(List<string> items) {
            string s = db.Search(items.Count == 1 ? items[0] : FromListToString(items));
            return s.Equals("") ? "Non è stato trovato nulla." : s;
        }

        //applica l'azione all
        private string AllNumbers() {
            string s = db.GetAllNumbers();
            return s.Equals("") ? "Non è stato trovato nulla." : s;
        }

        //applica l'azione delete
        private int DeleteNumber(List<string> items) {
            var d = FromListToString(items);
            return db.Remove(d) ? 3 : 2;
        }

        //converte una lista di stringhe in una coppia di stringhe: il corpo e la stringa finale
        private (string, string) FromListToDouple(List<string> items) {
            string num = items[items.Count - 1];
            items.Remove(num);
            return (FromListToString(items), num);
        }

        //converte una lista di stringhe in una stringa
        private string FromListToString(List<string> items) {
            return items.Aggregate((a, b) => a + " " + b);
        }

    }

}


