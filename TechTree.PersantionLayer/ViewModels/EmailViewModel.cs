namespace TechTree.PersantionLayer.ViewModels
{
    public class EmailViewModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        IList<IFormatProvider> attachements { get; set; } = null;
    }
}
