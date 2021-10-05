using System.Collections.Generic;

namespace Model.Action
{
    public class ActionResult
    {
        public bool IsPositiveResult {  get; set; }
        public string Message {  get; set; }

    }
    public class DataResults<T> : ActionResult 
    {
        public IEnumerable<T> Results { get; set; }
    }
}
