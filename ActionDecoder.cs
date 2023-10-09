using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubrica {

    //trasforma una stringa in una dupla azione-lista di argomenti
    public class ActionDecoder {

        //parametri minimi per far funzionare i comandi
        private readonly byte[] parameters = { 1, 3, 2, 2, 1, 1 };

        //valori con cui confrontare
        private readonly List<string []> register = new List<string[]> {
                new string[] { "help", "?" },
                new string[] { "new" },
                new string[] { "search", "find" },
                new string[] { "delete", "cancel", "destroy" },
                new string[] { "all" },
                new string[] { "exit", "end", "quit" }
        };

        //trasformo la stringa in una lista, ottengo il risultato e eccetto con l'azione NONE rimuovo la prima parola
        public (Action, List<string>) Decode(string s) {
            List<string> l = s.Split(" ").ToList();
            Action a = Decode(l);
            if (a != Action.NONE)
                l.RemoveAt(0);
            return (a, l);
        }

        //confronta la parola con tutte quelle da confrontare, una categoria alla volta
        private Action Decode(List<string> s) {
            for (int i = 0; i < 6; i++)
                if (s.Count >= parameters[i] && Decode(s[0].ToLower().Replace("-", ""), i))
                    return (Action)i;
            return Action.NONE;
        }

        //controlla di volta in volta se una parola inizia come un comando riconosciuto
        private bool Decode(string s, int i) {
            foreach (string r in register[i]) {
                if (r.StartsWith(s))
                    return true;
            }
            return false;
        }
    }
}
