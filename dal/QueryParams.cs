using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework.dal
    {
    public class QueryParams
        {
        public QueryParams(string fieldName, string value, dataType dataType, int size, string io)
            {
            this.fieldName = fieldName;
            this.value = value;
            this.dataType = dataType;
            this.size = size;
            this.io = io;
            }
        public QueryParams(string fieldName, string value, dataType dataType, int size)
            {
            this.fieldName = fieldName;
            this.value = value;
            this.dataType = dataType;
            this.size = size;
            this.io = io;
            }

        public string fieldName { get; set; }
        public string value { get; set; }
        public dataType dataType { get; set; }
        public int size { get; set; }
        public string? io { get; set; }

        }
    public enum dataType
        {
        String,
        Integer,
        Boolean
        }
    }
