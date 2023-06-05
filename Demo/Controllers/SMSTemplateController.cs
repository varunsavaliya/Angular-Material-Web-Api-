using Demo.DataModels;
using Demo.Repositories.SMSTemplate;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMSTemplateController : Controller
    {
        private readonly ISMSTemplateRepository _smsTemplate;

        public SMSTemplateController(ISMSTemplateRepository smsTemplate)
        {
            _smsTemplate = smsTemplate;
        }

        [HttpPost]
        public object AddSMSTemplate([FromBody] SMSTemplate smsTemplate)
        {
            return _smsTemplate.CreateSMSTemplate(smsTemplate);
        }

        [HttpGet]
        [Route("{id}")]
        public object GetSMSTemplateById(int id)
        {
            return _smsTemplate.GetSMStemplateById(id);
        }

        [HttpGet]
        public object GetAllSMSTemplate()
        {
            return _smsTemplate.GetAllSMSTemplate();
        }

        [HttpPatch]
        [Route("{id}")]
        public object UpdateSMSTemplate(int id, SMSTemplate smsTemplate)
        {
            return _smsTemplate.UpdateSMSTemplate(id, smsTemplate);
        }

        [HttpDelete]
        [Route("{id}")]
        public object DeleteSMSTemplate(int id)
        {
            return _smsTemplate.DeleteSMSTemplate(id);
        }
    }
}
