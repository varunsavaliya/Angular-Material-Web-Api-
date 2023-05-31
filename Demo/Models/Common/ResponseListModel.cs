using System.Collections.Generic;

namespace Demo.Models.Common
{
    public class ResponseListModel<T> : ResponseModel
    {
        public List<T> Items { get; set; }
    }
}
