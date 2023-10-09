using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubrica {
    //classe per output e input su console
    public class ConsoleUI {
        private readonly string[] db = {"Benvenut* nella rubrica. Come posso aiutarti?",
      "Comando non riconosciuto.", "Operazione non eseguita.", "Operazione eseguita.",
      "Rubrica:", "Contatti trovati:", "help: mostra questo messaggio\nnew: registra "+
      "nuovo numero es. new Mario Rossi 1234567890\nsearch: trova un contatto tramite "+
      "il nome o il numero\ndelete: cancella un numero nella rubrica\nall: mostra "+
      "tutti i numeri salvati\nexit: chiude il programma"};

        public string GetMessage(int n, string s) {
            s = s.Equals("") ? db[n] : db[n] + "\n" + s;
            Console.WriteLine("\n"+s+"\n");
            string c;
            do c = Console.ReadLine();
            while (c.Equals(null));
            return c;
        }
    }
}
