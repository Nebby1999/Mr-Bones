using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebby.Serialization
{
    [Serializable]
    public struct SerializedFieldCollection
    {
        public SerializedField[] serializedFields;

        public ref SerializedField GetOrCreateField(string fieldName)
        {
            if (serializedFields == null)
                serializedFields = Array.Empty<SerializedField>();

            for(int i = 0; i < serializedFields.Length; i++)
            {
                ref SerializedField fld = ref serializedFields[i];
                if(fld.fieldName.Equals(fieldName, StringComparison.Ordinal))
                {
                    return ref fld;
                }
            }

            SerializedField newFld = default(SerializedField);
            newFld.fieldName = fieldName;
            ArrayUtils.Append(ref serializedFields, newFld);
            ref SerializedField reference = ref serializedFields[serializedFields.Length - 1];
            return ref reference;
        }

        public void PurgeUnityPseudoNull()
        {
            if (serializedFields == null)
                serializedFields = Array.Empty<SerializedField>();

            for(int i = 0; i < serializedFields.Length; i++)
            {
                serializedFields[i].serializedValue.PurgeUnityNull();
            }
        }
    }
}