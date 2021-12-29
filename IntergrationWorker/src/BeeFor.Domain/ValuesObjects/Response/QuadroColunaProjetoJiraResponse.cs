using System.Collections.Generic;

namespace BeeFor.Domain.ValuesObjects.Response
{
    public class QuadroColunaProjetoJiraResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Self { get; set; }
        public Locations Location { get; set; }
        public Filter Filter { get; set; }
        public ColumnConfig ColumnConfig { get; set; }
        public Ranking Ranking { get; set; }
    }

    public class Locations
    {
        public string Type { get; set; }
        public string Key { get; set; }
        public string Id { get; set; }
        public string Self { get; set; }
        public string Name { get; set; }
    }

    public class Filter
    {
        public string Id { get; set; }
        public string Self { get; set; }
    }

    public class Statuss
    {
        public string Id { get; set; }
        public string Self { get; set; }
    }

    public class Column
    {
        public string Name { get; set; }
        public List<Statu> Statuses { get; set; }
    }

    public class ColumnConfig
    {
        public List<Column> Columns { get; set; }
        public string ConstraintType { get; set; }
    }

    public class Ranking
    {
        public int RankCustomFieldId { get; set; }
    }
}

