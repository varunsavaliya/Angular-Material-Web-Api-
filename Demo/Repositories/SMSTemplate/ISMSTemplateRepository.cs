namespace Demo.Repositories.SMSTemplate
{
    public interface ISMSTemplateRepository
    {
        public object GetAllSMSTemplate();
        public object CreateSMSTemplate(Demo.DataModels.SMSTemplate smsTemplate);
        public object GetSMStemplateById(int id);
        public object UpdateSMSTemplate(int smsTemplateId, Demo.DataModels.SMSTemplate smsTemplate);
        public object DeleteSMSTemplate(int smsTemplateId);
    }
}
