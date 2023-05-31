namespace Demo.Models.Common
{
    public class ResponseDataModel<T> : ResponseModel
    {
        public T Data { get; set; }
    }
}
