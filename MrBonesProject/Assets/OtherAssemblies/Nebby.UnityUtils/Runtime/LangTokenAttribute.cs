using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Nebby
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class LangTokenAttribute : PropertyAttribute
    {

    }
}
