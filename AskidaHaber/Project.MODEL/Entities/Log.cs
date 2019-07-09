namespace Project.MODEL.Entities
{
    public class Log : BaseEntity 
    {
        public string WhoIs { get; set; }

        public string ActionName { get; set; }

        public string ControllorName { get; set; }

        public string Information { get; set; }
      
        public Keyword Description { get; set; }

        public string IPAdress { get; set; }

        public string UrlAccessed { get; set; }

        public enum Keyword
        {
            Enter=1,Exit=2
        }
    }
}
