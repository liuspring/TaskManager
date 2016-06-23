using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Node.Model;
using XXF.Db;
using XXF.ProjectTool;

namespace TaskManager.Node.Dal
{
    public class LogDal
    {
        public int Add(DbConn pubConn, LogModel model)
        {
            return SqlHelper.Visit(ps =>
            {
                ps.Add("@msg", model.Msg);
                ps.Add("@logtype", model.LogType);
                ps.Add("@logcreatetime", model.CreationTime);
                ps.Add("@taskid", model.TaskId);
                ps.Add("@nodeid", model.NodeId);
                return pubConn.ExecuteSql(@"insert into tb_log(msg,logtype,logcreatetime,taskid,nodeid)
										   values(@msg,@logtype,@logcreatetime,@taskid,@nodeid)", ps.ToParameters());
            });
        }
    }
}
