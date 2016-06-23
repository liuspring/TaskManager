using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XXF.Db;
using XXF.ProjectTool;

namespace TaskManager.Node.Dal
{
    public class TaskDal
    {

        public List<int> GetTaskIDsByState(DbConn PubConn, int taskstate, int nodeid)
        {
            return SqlHelper.Visit(ps =>
            {
                ps.Add("@state", taskstate);
                ps.Add("@node_id", nodeid);
                var stringSql = new StringBuilder();
                stringSql.Append(@"select * from qrtz_task s where s.state=@state and s.node_id=@node_id");
                var ds = new DataSet();
                PubConn.SqlToDataSet(ds, stringSql.ToString(), ps.ToParameters());
                var rs = new List<int>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        rs.Add(Convert.ToInt32(dr[0]));
                    }
                }
                return rs;
            });

        }

        public int UpdateLastStartTime(DbConn pubConn, int id, DateTime time)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set tasklaststarttime=@tasklaststarttime where id=@id";
                ps.Add("id", id);
                ps.Add("tasklaststarttime", time);
                return pubConn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public int UpdateLastEndTime(DbConn pubConn, int id, DateTime time)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set tasklastendtime=@tasklastendtime where id=@id";
                ps.Add("id", id);
                ps.Add("tasklastendtime", time);
                return pubConn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public int UpdateTaskError(DbConn pubConn, int id, DateTime time)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set taskerrorcount=taskerrorcount+1,tasklasterrortime=@tasklasterrortime where id=@id";
                ps.Add("id", id);
                ps.Add("tasklasterrortime", time);
                return pubConn.ExecuteSql(cmd, ps.ToParameters());
            });
        }

        public int UpdateTaskSuccess(DbConn pubConn, int id)
        {
            return SqlHelper.Visit(ps =>
            {
                string cmd = "update tb_task set taskerrorcount=0,taskruncount=taskruncount+1 where id=@id";
                ps.Add("id", id);
                return pubConn.ExecuteSql(cmd, ps.ToParameters());
            });
        }
    }
}
