namespace MAD.DataWarehouse.SignOnSite.Api
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    public partial class SiteAttendeesApiResponse
    {
        [JsonProperty("attendees")]
        public List<SiteAttendee> Attendees { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class SiteAttendee
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("has_active_enrolment")]
        public bool HasActiveEnrolment { get; set; }

        [JsonProperty("needs_to_do_site_induction")]
        public bool NeedsToDoSiteInduction { get; set; }

        [JsonProperty("site_induction")]
        public SiteAttendeeSiteInduction SiteInduction { get; set; }

        [JsonProperty("attendances")]
        public List<SiteAttendeeAttendance> Attendances { get; set; }

        [JsonProperty("worker_notes")]
        public JArray WorkerNotes { get; set; }
    }

    public partial class SiteAttendeeAttendance
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("signon_at")]
        public DateTimeOffset SignonAt { get; set; }

        [JsonProperty("signoff_at")]
        public DateTimeOffset? SignoffAt { get; set; }

        [JsonProperty("signon_channel")]
        public string SignonChannel { get; set; }

        [JsonProperty("briefing_status")]
        public string BriefingStatus { get; set; }

        [JsonProperty("company")]
        public SiteAttendeeAttendanceCompany Company { get; set; }
    }

    public partial class SiteAttendeeAttendanceCompany
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class SiteAttendeeSiteInduction
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
