using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerClientOperations
{
    public class ClientOperation
    {
        public ClientOperation(OperationType type, string content)
        {
            this.Type = type;
            this.OperationContent = content;
        }

        public OperationType Type { get; set; }
        public string OperationContent { get; set; }
    }
}
