using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebby.Serialization
{
    [Serializable]
    public struct SerializedField
    {
        public string fieldName;
        public SerializedValue serializedValue;
    }
}
