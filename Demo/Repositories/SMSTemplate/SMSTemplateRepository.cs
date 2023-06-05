using Demo.Repositories.SMSTemplate;
using Demo.DataModels;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Demo.Constants;
using Demo.Models;
using System;
using Demo.Models.Common;
using Demo.Models.SMSTemplate;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Repositories
{
    public class SMSTemplateRepository : ISMSTemplateRepository
    {
        private readonly DapperContext _dapperContext;

        public SMSTemplateRepository(IConfiguration configuraion, DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public object GetAllSMSTemplate()
        {
            ResponseListModel<SMSTemplateModel> responseListModel = new();
            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    responseListModel.Items = connection.Query<SMSTemplateModel>(StoredProcedures.PR_GET_SMS_TEMPLATE_LIST, commandType: CommandType.StoredProcedure).AsList();
                }

                responseListModel.Success = true;
                responseListModel.Message = "success";
            }
            catch (Exception ex)
            {
                responseListModel.Success = false;
                responseListModel.Message = ex.Message;
            }
            return responseListModel;
        }

        public object CreateSMSTemplate(Demo.DataModels.SMSTemplate smsTemplate)
        {
            ResponseModel responseModel = new();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: int.MaxValue);
                parameters.Add("@Name", dbType: DbType.String, value: smsTemplate.Name, direction: ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@Template", dbType: DbType.String, value: smsTemplate.Template, direction: ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@CreatedOn", dbType: DbType.DateTime, value: smsTemplate.CreatedOn, direction: ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@CreatedBy", dbType: DbType.Int64, value: smsTemplate.CreatedBy, direction: ParameterDirection.Input, size: int.MaxValue);

                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Execute(StoredProcedures.PR_ADD_SMS_TEMPLATE, parameters, commandType: CommandType.StoredProcedure);
                    responseModel.Message = parameters.Get<string>("@Message");
                }
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }

        public object GetSMStemplateById(int smsTemplateId)
        {
            var spParameters = new
            {
                SMSTemplateId = smsTemplateId.ToString(),
            };
            ResponseDataModel<SMSTemplateModel> responseDataModel = new();
            try
            {
                using (var connection = _dapperContext.CreateConnection())
                {
                    responseDataModel.Data = connection.QueryFirstOrDefault<SMSTemplateModel>(StoredProcedures.PR_GET_SMS_TEMPLATE_BY_ID, spParameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                responseDataModel.Success = false;
                responseDataModel.Message = ex.Message;
            }
            return responseDataModel;
        }

        public object UpdateSMSTemplate(int smsTemplateId, Demo.DataModels.SMSTemplate smsTemplate)
        {
            ResponseModel responseModel = new();
            try
            {

                var parameters = new DynamicParameters();
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: int.MaxValue);
                parameters.Add("@SMSTemplateId", dbType: DbType.Int64, value: smsTemplateId, direction: ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@Name", dbType: DbType.String, value: smsTemplate.Name, direction: ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@Template", dbType: DbType.String, value: smsTemplate.Template, direction: ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@UpdatedOn", dbType: DbType.DateTime, value: smsTemplate.UpdatedOn, direction: ParameterDirection.Input, size: int.MaxValue);
                parameters.Add("@UpdatedBy", dbType: DbType.Int64, value: smsTemplate.UpdatedBy, direction: ParameterDirection.Input, size: int.MaxValue);
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Execute(StoredProcedures.PR_UPDATE_SMS_TEMPLATE, parameters, commandType: CommandType.StoredProcedure);
                    responseModel.Message = parameters.Get<string>("@Message");
                }
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }

            return responseModel;
        }

        public object DeleteSMSTemplate(int smsTemplateId)
        {
            var spParameters = new
            {
                SMSTemplateId = smsTemplateId,
                Message = string.Empty
            };

            ResponseModel responseModel = new();
            try
            {
                var parameters = new DynamicParameters(spParameters);
                parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: int.MaxValue);
                parameters.Add("@SMSTemplateId", dbType: DbType.Int64, value: smsTemplateId, direction: ParameterDirection.Input, size: int.MaxValue);
                using (var connection = _dapperContext.CreateConnection())
                {
                    connection.Execute(StoredProcedures.PR_DELETE_SMS_TEMPLATE, parameters, commandType: CommandType.StoredProcedure);
                    responseModel.Message = parameters.Get<string>("@Message");
                }
                responseModel.Success = true;
            }
            catch (Exception ex)
            {
                responseModel.Success = false;
                responseModel.Message = ex.Message;
            }
            return responseModel;
        }
    }
}
