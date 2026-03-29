using System;

namespace AnimalCareApplication.Models
{
    public class Notification
    {
        public int IdNotification { get; set; }

        public int IdUtilisateur { get; set; }
        
        public string Message { get; set; } = string.Empty;

        public bool EstLue { get; set; } = false;

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
    }
}