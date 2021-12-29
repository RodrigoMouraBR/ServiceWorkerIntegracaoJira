using System.Collections.Generic;

namespace BeeFor.Domain.ValuesObjects.Response
{
    public class BoardEpicNoneIssueResponse
    {
        public string Expand { get; set; }
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
        public List<Issue> Issues { get; set; }
    }   
    public class Issuetype
    {
        public string Self { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public bool Subtask { get; set; }
        public int AvatarId { get; set; }
        public string EntityId { get; set; }
        public int HierarchyLevel { get; set; }
    }

    public class AvatarUrls
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Project
    {
        public string Self { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string ProjectTypeKey { get; set; }
        public bool Simplified { get; set; }
        public AvatarUrls AvatarUrls { get; set; }
    }

    public class Resolution
    {
        public string Self { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }

    public class Issuerestrictions
    {
    }

    public class Issuerestriction
    {
        public Issuerestrictions Issuerestrictions { get; set; }
        public bool ShouldDisplay { get; set; }
    }

    public class Watches
    {
        public string Self { get; set; }
        public int WatchCount { get; set; }
        public bool IsWatching { get; set; }
    }

    public class Priority
    {
        public string Self { get; set; }
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
    }

    public class NonEditableReason
    {
        public string Reason { get; set; }
        public string Message { get; set; }
    }

    public class Customfield10018
    {
        public bool HasEpicLinkFieldDependency { get; set; }
        public bool ShowField { get; set; }
        public NonEditableReason NonEditableReason { get; set; }
    }

    public class Assignee
    {
        public string Self { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        public AvatarUrls AvatarUrls { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string TimeZone { get; set; }
        public string AccountType { get; set; }
    }

    public class StatusCategory
    {
        public string Self { get; set; }
        public int Id { get; set; }
        public string Key { get; set; }
        public string ColorName { get; set; }
        public string Name { get; set; }
    }

    public class Statu
    {
        public string Self { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public StatusCategory StatusCategory { get; set; }
    }

    public class Timetracking
    {
    }

    public class Creator
    {
        public string Self { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        public AvatarUrls AvatarUrls { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string TimeZone { get; set; }
        public string AccountType { get; set; }
    }

    public class Reporter
    {
        public string Self { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        public AvatarUrls AvatarUrls { get; set; }
        public string DisplayName { get; set; }
        public bool Active { get; set; }
        public string TimeZone { get; set; }
        public string AccountType { get; set; }
    }

    public class Aggregateprogress
    {
        public int Progress { get; set; }
        public int Total { get; set; }
    }

    public class Progress
    {
        public int progresss { get; set; }
        public int Total { get; set; }
    }

    public class Comment
    {
        public List<object> Comments { get; set; }
        public string Self { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
        public int StartAt { get; set; }
    }

    public class Votes
    {
        public string Self { get; set; }
        public int votes { get; set; }
        public bool HasVoted { get; set; }
    }

    public class Worklog
    {
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
        public List<object> Worklogs { get; set; }
    }

    public class Fields
    {

        public Issuetype Issuetype { get; set; }
        public object Timespent { get; set; }
        public object Sprint { get; set; }
        public Project Project { get; set; }
        public List<object> FixVersions { get; set; }
        public object Aggregatetimespent { get; set; }
        public Resolution Resolution { get; set; }

        public int Workratio { get; set; }

        public Issuerestriction Issuerestriction { get; set; }
        public Watches Watches { get; set; }

        public object Customfield_10020 { get; set; }
        public object Customfield_10021 { get; set; }
        public object Epic { get; set; }
        public object Customfield_10022 { get; set; }
        public Priority Priority { get; set; }
        public object Customfield_10023 { get; set; }
        public object Customfield_10024 { get; set; }
        public string Customfield_10025 { get; set; }
        public List<object> Labels { get; set; }
        public object Customfield_10016 { get; set; }
        public object Customfield_10017 { get; set; }
        public Customfield10018 Customfield_10018 { get; set; }
        public string Customfield_10019 { get; set; }
        public object Aggregatetimeoriginalestimate { get; set; }
        public object Timeestimate { get; set; }
        public List<object> Versions { get; set; }
        public List<object> Issuelinks { get; set; }
        public Assignee Assignee { get; set; }

        public Statu Status { get; set; }
        public List<object> Components { get; set; }
        public object Timeoriginalestimate { get; set; }
        public string Description { get; set; }
        public object Customfield_10010 { get; set; }
        public object Customfield_10014 { get; set; }
        public object Customfield_10015 { get; set; }
        public Timetracking Timetracking { get; set; }
        public object Customfield_10005 { get; set; }
        public object Customfield_10006 { get; set; }
        public object Security { get; set; }
        public object Customfield_10007 { get; set; }
        public object Customfield_10008 { get; set; }
        public List<object> Attachment { get; set; }
        public object Customfield_10009 { get; set; }
        public object Aggregatetimeestimate { get; set; }
        public bool Flagged { get; set; }
        public string Summary { get; set; }
        public Creator Creator { get; set; }
        public List<object> Subtasks { get; set; }
        public Reporter Reporter { get; set; }
        public Aggregateprogress Aggregateprogress { get; set; }
        public string Customfield_10000 { get; set; }
        public object Customfield_10001 { get; set; }
        public object Customfield_10002 { get; set; }
        public object Customfield_10003 { get; set; }
        public object Customfield_10004 { get; set; }
        public object Environment { get; set; }
        public object Duedate { get; set; }
        public Progress Progress { get; set; }
        public Comment Comment { get; set; }
        public Votes Votes { get; set; }
        public Worklog Worklog { get; set; }
    }

    public class Issue
    {
        public string Expand { get; set; }
        public string Id { get; set; }
        public string Self { get; set; }
        public string Key { get; set; }
        public Fields Fields { get; set; }
    }
}
