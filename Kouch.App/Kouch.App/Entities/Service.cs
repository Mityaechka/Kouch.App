namespace Kouch.App.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public bool IsOnline { get; set; }
        public ServiceType ServiceType { get; set; }
    }
    public class ServiceDescription
    {
        public string Name
        {
            get;
            set;
        }
        public string Description { get; set; }
        public Language Language
        {
            get;
            set;
        }
    }
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum ServiceType
    {
        Service = 1,
        Item = 2
    }
    public class ServiceTypeDisplay
    {
        public ServiceType ServiceType { get; set; }
        public string Name { get; set; }
    }
}
