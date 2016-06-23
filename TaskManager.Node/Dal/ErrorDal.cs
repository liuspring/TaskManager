using TaskManager.Node.Model;
using XXF.Db;
using XXF.ProjectTool;

namespace TaskManager.Node.Dal
{
    public class ErrorDal
    {
        public int Add(DbConn pubConn, ErrorModel model)
        {
            return SqlHelper.Visit(ps =>
            {
                ps.Add("@msg", model.Msg);
                ps.Add("@errortype", model.ErrorType);
                ps.Add("@errorcreatetime", model.CreationTime);
                ps.Add("@taskid", model.TaskId);
                ps.Add("@nodeid", model.NodeId);
                return pubConn.ExecuteSql(@"insert into tb_error(msg,errortype,errorcreatetime,taskid,nodeid)
										   values(@msg,@errortype,@errorcreatetime,@taskid,@nodeid)", ps.ToParameters());
            });
        }
    }
}
