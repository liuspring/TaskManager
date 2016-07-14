using System;
using MySql.Data.MySqlClient;

namespace TaskManager.BaseService.SystemRuntime
{
    /// <summary>
    /// 任务运行时底层操作类
    /// 仅平台内部使用
    /// </summary>
    public class TaskSystemRuntimeOperator
    {
        /// <summary>
        /// 任务dll实例引用
        /// </summary>
        protected BaseDllTask DllTask = null;

        protected string TaskConnectString = string.Empty;
        protected int TaskId = 0,NodeId=0;
        protected string Localtempdatafilename = "localtempdata.json.txt";
        public TaskSystemRuntimeOperator(BaseDllTask dlltask)
        {
            DllTask = dlltask;
        }

        public void SaveLocalTempData(object obj)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + Localtempdatafilename;
            var json = JsonHelper.Serialize(obj);
            System.IO.File.WriteAllText(filename, json);
        }

        public T GetLocalTempData<T>() where T : class
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + Localtempdatafilename;
            if (!System.IO.File.Exists(filename))
                return null;
            string content = System.IO.File.ReadAllText(filename);
            var obj = JsonHelper.DeSerialize<T>(content);
            return obj;
        }
        public void SaveDataBaseTempData(object obj)
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            var dataJson = JsonHelper.Serialize(obj);
            const string sql =
                "INSERT INTO  qrtz_temp_data(task_id,data_json,creation_time,creator_user_id) " +
                "VALUES  (@task_id,@data_json,NOW(),1)";
            var parameters = new[]
            {
                new MySqlParameter("@task_id",MySqlDbType.DateTime),
                new MySqlParameter("@data_json",MySqlDbType.Int32,11)
            };
            parameters[0].Value = TaskId;
            parameters[1].Value = dataJson;
            MySqlHelper.ExecuteNonQuery(TaskConnectString, sql, parameters);
        }
        public T GetDataBaseTempData<T>() where T : class
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            const string sql = "SELECT data_json FROM qrtz_temp_data WHERE task_id=@task_id";
            var parameters = new[]
            {
                new MySqlParameter("@task_id",MySqlDbType.DateTime)
            };
            parameters[0].Value = TaskId;
            var dt = MySqlHelper.ExecuteDataset(TaskConnectString, sql, parameters).Tables[0];
            if (dt.Rows.Count == 1)
            {
                var json = dt.Rows[0][0].ToString();
                return JsonHelper.DeSerialize<T>(json);
            }
            return null;
        }

        public void UpdateLastStartTime()
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            const string sql = "UPDATE qrtz_task SET last_start_time=NOW(),last_modification_time=NOW(),last_modifier_user_id=1 WHERE id=@id ";
            var parameters = new[]
            {
                new MySqlParameter("@id",MySqlDbType.Int32,11)
            };
            parameters[0].Value = TaskId;
            MySqlHelper.ExecuteNonQuery(TaskConnectString, sql, parameters);
        }

        public void UpdateLastEndTime()
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            const string sql = "UPDATE qrtz_task SET last_end_time=NOW(),last_modification_time=NOW(),last_modifier_user_id=1 WHERE id=@id ";
            var parameters = new[]
            {
                new MySqlParameter("@id",MySqlDbType.Int32,11)
            };
            parameters[0].Value = TaskId;
            MySqlHelper.ExecuteNonQuery(TaskConnectString, sql, parameters);
        }


        public void UpdateTaskError()
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            const string sql = "UPDATE qrtz_task SET last_error_time=NOW(),last_modification_time=NOW(),last_modifier_user_id=1 WHERE id=@id ";
            var parameters = new[]
            {
                new MySqlParameter("@id",MySqlDbType.Int32,11)
            };
            parameters[0].Value = TaskId;
            MySqlHelper.ExecuteNonQuery(TaskConnectString, sql, parameters);
        }

        public void UpdateTaskSuccess()
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            const string sql = "UPDATE qrtz_task SET error_count=0,run_count=run_count+1,last_modification_time=NOW(),last_modifier_user_id=1 WHERE id=@id ";
            var parameters = new[]
            {
                new MySqlParameter("@id",MySqlDbType.Int32,11)
            };
            parameters[0].Value = TaskId;
            MySqlHelper.ExecuteNonQuery(TaskConnectString, sql, parameters);
        }

        /// <summary>
        /// 添加任务日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="logType"></param>
        public void AddTaskLog(string msg, byte logType = (byte)EnumTaskLogType.CommonLog)
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            NodeId = DllTask.SystemRuntimeInfo.TaskModel.NodeId;
            const string sql = "INSERT INTO qrtz_log (node_id,task_id,mgs,log_type,creation_time,creator_user_id) " +
                      "VALUES  (@node_id,@task_id,@mgs,@log_type,NOW(),1)";
            var parameters = new[]
            {
                new MySqlParameter("@node_id",MySqlDbType.Int32,11),
                new MySqlParameter("@task_id",MySqlDbType.Int32,11),
                new MySqlParameter("@mgs",MySqlDbType.VarChar,200),
                new MySqlParameter("@log_type",MySqlDbType.Byte)
            };
            parameters[0].Value = NodeId;
            parameters[1].Value = TaskId;
            parameters[2].Value = msg;
            parameters[3].Value = logType;
            MySqlHelper.ExecuteNonQuery(TaskConnectString, sql, parameters);
        }

        /// <summary>
        /// 添加任务错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        /// <param name="errorType"></param>
        public void AddTaskError(string msg,  Exception ex, byte errorType = (byte)EnumTaskLogType.CommonError)
        {
            TaskConnectString = DllTask.SystemRuntimeInfo.TaskConnectString;
            TaskId = DllTask.SystemRuntimeInfo.TaskModel.Id;
            NodeId = DllTask.SystemRuntimeInfo.TaskModel.NodeId;
            const string sql = "INSERT INTO qrtz_error (node_id,task_id,mgs,error_type,creation_time,creator_user_id) " +
                      "VALUES  (@node_id,@task_id,@mgs,@log_type,NOW(),1)";
            var parameters = new[]
            {
                new MySqlParameter("@node_id",MySqlDbType.Int32,11),
                new MySqlParameter("@task_id",MySqlDbType.Int32,11),
                new MySqlParameter("@mgs",MySqlDbType.VarChar,200),
                new MySqlParameter("@error_type",MySqlDbType.Byte)
            };
            parameters[0].Value = TaskId;
            MySqlHelper.ExecuteNonQuery(TaskConnectString, sql, parameters);
        }
    }
}
