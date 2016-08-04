﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Nodes;
using TaskManager.Tasks;

namespace TaskManager.Performances
{
    [Table("qrtz_performance")]
    [Description("节点和任务性能记录表")]
    [Serializable]
    public class Performance : BaseEntity
    {

        [ForeignKey("NodeId")]
        public virtual Node Node { get; set; }

        [Required]
        [Column("node_id")]
        [Description("节点ID")]
        public int NodeId { get; set; }

        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }

        [Column("task_id")]
        [Description("任务ID")]
        public int TaskId { get; set; }

        [Column("cpu")]
        [Description("CPU(总cpu时间 s)")]
        public float Cpu { get; set; }

        [Column("memory")]
        [Description("内存（M）")]
        public float Memory { get; set; }

        [Column("install_dir_size")]
        [Description("应用大小")]
        public float InstallDirSize { get; set; }

        public static Performance Create(int nodeId, int taskId, float cpu, float memory, float installDirSize)
        {
            var performance = new Performance
            {
                NodeId = nodeId,
                TaskId = taskId,
                Cpu = cpu,
                Memory = memory,
                InstallDirSize = installDirSize
            };
            return performance;
        }
    }
}
