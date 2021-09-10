using System.Collections.Generic;
namespace DataModel.Common
{
   public  class ResponseModel<T>
    {
        public List<T>  Data { get; set; }
        public bool Sucess  { get; set; }
        public ErrorModel Error  { get; set; }
        public int  TotalCount  { get; set; }
        
    }
}
 