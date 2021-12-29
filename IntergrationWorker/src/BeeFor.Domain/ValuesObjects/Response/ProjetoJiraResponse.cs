namespace BeeFor.Domain.ValuesObjects.Response
{
    public class ProjetoJiraResponse
    {
        public ProjetoJiraResponse()
        {
            Lead = new Lead();
        }
        public string Expand { get; set; }
        public string Self { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public Lead Lead { get; set; }
    }

    public class Lead
    {
        public string AccountId { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
    }
}
