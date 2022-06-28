/*
 * PxApi
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 2.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PxWeb.Models.Api2
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class TablesResponseAllOf : IEquatable<TablesResponseAllOf>
    {
        /// <summary>
        /// Gets or Sets Tables
        /// </summary>
        [DataMember(Name="tables", EmitDefaultValue=false)]
        public List<Table> Tables { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TablesResponseAllOf {\n");
            sb.Append("  Tables: ").Append(Tables).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TablesResponseAllOf)obj);
        }

        /// <summary>
        /// Returns true if TablesResponseAllOf instances are equal
        /// </summary>
        /// <param name="other">Instance of TablesResponseAllOf to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TablesResponseAllOf other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Tables == other.Tables ||
                    Tables != null &&
                    other.Tables != null &&
                    Tables.SequenceEqual(other.Tables)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Tables != null)
                    hashCode = hashCode * 59 + Tables.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(TablesResponseAllOf left, TablesResponseAllOf right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TablesResponseAllOf left, TablesResponseAllOf right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
