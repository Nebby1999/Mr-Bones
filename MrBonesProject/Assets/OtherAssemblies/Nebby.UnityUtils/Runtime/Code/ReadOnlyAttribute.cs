using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nebby
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
}
