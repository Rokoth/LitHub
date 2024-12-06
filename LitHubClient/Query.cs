using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitHubClient
{
    public class Query
    {
        public enum QueryType
        {
            SELECT, INSERT, UPDATE, DELETE
        }        
        
        public QueryType Querytype { get; }
        public string Table { get; }
        public Dictionary<string, string> Fields { get; }
        public WhereClause Whereclause { get; }

        public Query(QueryType qt, string table, Dictionary<string, string> fields, WhereClause whereclause)
        {            
            Querytype = qt;
            Table = table;
            Fields = fields;
            if (CheckWhere(whereclause))
            {
                Whereclause = whereclause;
            }
            else
            {
                throw new Exception("Invalid query parameters in whereclause");
            }
        }

        private bool CheckWhere(WhereClause w)
        {
            bool ok = true;
            if (w.Logictype == WhereClause.LogicType.SIMPLE)
            {
                if (((string)w.GetValue()) == null || ((string)w.GetValue())== string.Empty)
                {
                    ok = false;
                }
            }
            else
            {
                foreach (WhereClause wr in ((List<WhereClause>)w.GetValue()))
                {
                    if (!CheckWhere(wr)) ok = false;
                }
            }            

            return ok;
        }
    }

    public class WhereClause
    {
        public enum LogicType
        {
            AND, OR, SIMPLE, NONE
        }
        public LogicType Logictype { get; }
        private string Field;
        private List<WhereClause> Whereclauses;

        public WhereClause(LogicType lt, object val = null)
        {
            Logictype = lt;
            if (lt != LogicType.NONE)
            {
                if (lt == LogicType.SIMPLE)
                    Field = (string)val;
                else Whereclauses = (List<WhereClause>)val;
            }
        }

        public object GetValue()
        {
            if (Logictype == LogicType.NONE) return null;
            else if (Logictype == LogicType.SIMPLE) return Field;
            else return Whereclauses;
        }

        public string Get()
        {
            if (Logictype == LogicType.NONE) return "";
            else return " WHERE " + ParseWhere(this);
        }

        private string ParseWhere(WhereClause w)
        {
            string str = "";
            if (Logictype == LogicType.SIMPLE)
            {
                str += w.Field;
            }
            else if (Logictype == LogicType.AND)
            {
                str += " (" + String.Join(" AND ", w.Whereclauses.Select(s=>ParseWhere(s)).ToArray()) + ") ";
                
            }
            else if (Logictype == LogicType.OR)
            {
                str += " (" + String.Join(" OR ", w.Whereclauses.Select(s => ParseWhere(s)).ToArray()) + ") ";
            }
            return str;
        }
    }
}
