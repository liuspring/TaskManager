using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXF.Db;
using XXF.ProjectTool;

namespace TaskManager.Node.Dal
{
    public class TempDataDal
    {
        public virtual int SaveTempData(DbConn pubConn, int taskid, string json)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_tempdata set tempdatajson=@tempdatajson where taskid=@taskid";
                ps.Add("taskid", taskid);
                ps.Add("tempdatajson", json);
                return pubConn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public virtual string GetTempData(DbConn pubConn, int taskid)
        {
            return SqlHelper.Visit(ps =>
            {
                var stringSql = new StringBuilder();
                stringSql.Append(@"select s.* from tb_tempdata s where s.taskid=@taskid");
                ps.Add("taskid", taskid);
                var ds = new DataSet();
                pubConn.SqlToDataSet(ds, stringSql.ToString(), ps.ToParameters());
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToString(ds.Tables[0].Rows[0]["tempdatajson"]);
                }
                return null;
            });
        }
    }
}
