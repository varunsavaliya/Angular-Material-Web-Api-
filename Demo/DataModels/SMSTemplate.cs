using System;

namespace Demo.DataModels
{
    public class SMSTemplate
    {
        public int SMSTemplateId { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }

        public User CreatedByUser { get; set; }
        public User UpdatedByUser { get; set; }
    }
}
