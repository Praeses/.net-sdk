using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CTCT.Components
{
    /// <summary>
    /// Base class for components.
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class Component
    {
        /// <summary>
        /// Get the object from JSON.
        /// </summary>
        /// <typeparam name="T">The class type to be deserialized.</typeparam>
        /// <param name="json">The serialization string.</param>
        /// <returns>Returns the object deserialized from the JSON string.</returns>
        public static T FromJSON<T>(string json) where T : class
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        /// <summary>
        /// Serialize an object to JSON.
        /// </summary>
        /// <returns>Returns a string representing the serialized object.</returns>
        public virtual string ToJSON()
        {
            var json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
