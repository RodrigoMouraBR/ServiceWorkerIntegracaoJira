using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeFor.Domain.ValuesObjects.Response
{
    public class QuadroProjetoJiraResponse
    {
        public QuadroProjetoJiraResponse()
        {
            Values = new List<Value>();
        }
        public int MaxResults { get; set; }
        public int StartAt { get; set; }
        public int Total { get; set; }
        public bool IsLast { get; set; }
        public List<Value> Values { get; set; }
    }

    public class Location
    {
        public int ProjectId { get; set; }
        public string DisplayName { get; set; }
        public string ProjectName { get; set; }
        public string ProjectKey { get; set; }
        public string ProjectTypeKey { get; set; }
        public string AvatarURI { get; set; }
        public string Name { get; set; }
    }

    public class Value
    {
        public Value()
        {
            Location = new Location();
        }
        public int Id { get; set; }
        public string Self { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Location Location { get; set; }
    }
}
