using System;
using System.Reflection;

namespace TypeSystemImprovements
{
    class Program
    {
        static void Main(string[] args)
        {
            object o = new {A = "test", B = "B"};

            // TODO: make it print all the properties. Do not modify this method, see below
            foreach (var kvp in o)
            {
                Console.WriteLine(kvp);
            }
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// A helper method getting all the properties of a specific object's type.
        /// </summary>
        static PropertyInfo[] GetAllProperties(object o) => o.GetType().GetProperties();

        /// <summary>
        /// A helper method for getting a value of a specific property.
        /// </summary>
        static object GetPropertyValue(object o, PropertyInfo property) => property.GetValue(o);
        
        // TODO: provide an extension method GetEnumerator for object type that returns pairs of ValueTuple<string, object> representing property name with its value. 
        // Use helper methods above or any other API that if you prefer to.
    }
}