using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebby.CSharpUtils
{
    public static class Extensions
    {
        public static bool IsBetween(this float val, float bound1, float bound2)
        {
            return (val >= Math.Min(bound1, bound2) && val <= Math.Max(bound1, bound2));
        }
    }
}
