using System.Collections.Generic;

namespace RepositoryT.RavenDb.Mvc4AutofacSample.Models
{
    public static class ExtensionMethods
    {
         public static string ToCommaSeparatedString(this List<string> list)
         {
             return list == null ? string.Empty : string.Join(", ", list);
         }
    }
}