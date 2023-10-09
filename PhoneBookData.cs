using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Principal;

namespace Rubrica {
    //Registro di tutti i contatti
    public class PhoneBookData {

        //su questo dizionario vengono salvati tutti i contatti
        private Dictionary<string, NumList> db;

        //se è presente un file carica quello, altrimenti la rubrica è vuota
        public PhoneBookData() {
            db = new Dictionary<string, NumList>();
            if (File.Exists(@".\PhoneBookData.json")) {
                string s = File.ReadAllText(@".\PhoneBookData.json");
                var a = JsonSerializer.Deserialize<string []>(s).ToList().Select(x => JsonSerializer.Deserialize<string[]>(x).ToList()).ToList();
                a.ToList().ForEach(contact => { for(int i = 1; i < contact.Count; i++) NewNumber(contact[0], contact[i]); });
            }
        }

        //aggiunge un numero solo se non già presente da nessuna parte
        //se non era già presente inizializza una numlist
        public bool NewNumber(string name, string number) {
            if (db.Values.Where(n => n.FindNumber(number)).Count() > 0) return false;
            if (!db.ContainsKey(name))
                db.Add(name, new NumList());
            bool b = db[name].AddNumber(number);
            Save();
            return b;
        }

        //controlla se una stringa è presente nei nomi o nei numeri dei contatti
        public string Search(string n) {
            var l = db.Where(v => v.Key.ToUpper().Contains(n.ToUpper()) ||
            v.Value.GetNumList().Where(s => s.Contains(n)).Count() > 0)
            .Select(v => v.Key + "\n" + v.Value.GetAllNumbers());
            return l.Count() > 0 ? l.Aggregate((a, b) => a + "\n\n" + b) : "";
        }

        //restituisce l'intera rubrica
        public string GetAllNumbers() {
            return Search("");
        }

        //rimuove un numero o un contatto, se presente
        public bool Remove(string s) {
            return RemoveContact(s) || RemoveNumber(s);
        }

        //rimuove un contatto
        public bool RemoveContact(string n) {
            bool b = db.Remove(n);
            Save();
            return b;
        }

        //rimuove un numero
        public bool RemoveNumber(string num) {
            var l = db.Where(n => n.Value.FindNumber(num)).ToList();
            if(l.Count() == 0) return false;
            l[0].Value.RemoveNumber(num);
            if (l[0].Value.IsEmpy())
                RemoveContact(l[0].Key);
            Save();
            return true;
        }

        //salva in json il dizionario
        private void Save() {
            List<string> l = new List<string>();
            foreach (var item in db) {
                var a = item.Value.GetNumList();
                a.Insert(0, item.Key);
                l.Add(JsonSerializer.Serialize(a.ToArray()));
            }
            string s = JsonSerializer.Serialize(l.ToArray());
            File.WriteAllText(@".\PhoneBookData.json", s);
        }

    }
}
