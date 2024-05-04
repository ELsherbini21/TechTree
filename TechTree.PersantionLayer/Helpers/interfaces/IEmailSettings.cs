using TechTree.DAL.Models;

namespace TechTree.PersantionLayer.Helpers.interfaces
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);
    }
}
