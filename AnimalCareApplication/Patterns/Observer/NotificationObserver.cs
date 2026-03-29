using System;
using AnimalCareApplication.Models;

namespace AnimalCareApplication.Patterns.Observer
{
    public class NotificationObserver : IObserver
    {
        private readonly AnimalCareDbContext _context;
        private readonly int _idUtilisateur;

        public NotificationObserver(AnimalCareDbContext context, int idUtilisateur)
        {
            _context = context;
            _idUtilisateur = idUtilisateur;
        }

        public void Update(string message)
        {
            var notification = new Notification
            {
                IdUtilisateur = _idUtilisateur,
                Message = message,
                EstLue = false,
                DateCreation = DateTime.Now
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
    }
}