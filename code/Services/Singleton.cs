using ProjetFinDeSession.Models;
using System;
using System.Collections.Generic;

namespace ProjetFinDeSession.Services
{
    public sealed class Singleton
    {
        private static Singleton _instance = null;
        private static readonly object _lock = new object();

        public List<Utilisateur> Utilisateurs { get; private set; }

        private Singleton()
        {
            Console.WriteLine(" Singleton créé : une seule instance pour toute l’application MVC.");
            Utilisateurs = new List<Utilisateur>();
        }

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    Console.WriteLine(" instance est null  création en cours");
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Singleton();
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" Singleton déjà existant instance réutilisée.");
                }

                return _instance;
            }
        }

        public void LogMessage(string message)
        {
            Console.WriteLine(" LOG Singleton : " + message);
        }

     

        public bool EmailExiste(string email)
        {
            foreach (var u in Utilisateurs)
            {
                if (u.Email.ToLower() == email.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        public Utilisateur TrouverUtilisateur(string email, string mdp)
        {
            foreach (var u in Utilisateurs)
            {
                if (u.Email.ToLower() == email.ToLower()
                    && u.MotDePasse == mdp)
                {
                    return u;
                }
            }

            return null;
        }
    }
}
