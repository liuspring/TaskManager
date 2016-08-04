$(function () {
    var grid = new InitDataTables();
    grid.init({
        src: $("#tb_task"),
        dataTable: {
            "sAjaxSource": "/Tasks/AjaxTaskList", // get地址
            //"pageLength": 2,
            //向服务器传额外的参数
            "fnServerParams": function (aoData) {
                aoData.push(
                     { "name": "TaskName", "value": $("#Content").val() }
                ); //查询内容
            },
            //配置列要显示的数据
            columns: [
                //对应上面thead里面的序列 ;字段名字和返回的json序列的key对应
                { "data": "id" },
                { "data": "taskName" },
                { "data": "id" },
                { "data": "id" },
                { "data": "remark" },
                { "data": "id" }
            ],
            //按钮列
            "columnDefs": [
                {
                    "render": function (data, type, row, me) {
                        return me.settings._iDisplayStart + me.row + 1;
                    },
                    "targets": 0
                },
                {
                    "render": function (data, type, row, me) {
                        var opt = "";
                        opt += "<span>【最近开始时间】" + row.sLastStartTime + "</span></br>";
                        opt += "<span>【最近结束时间】" + row.sLastEndTime + "</span></br>";
                        opt += "<span>【任务运行状态】" + row.sState + "</span></br>";
                        opt += "<span>【运行成功次数】" + row.runCount + "</span></br>";
                        opt += "<span>【上次出错时间】" + row.sLastErrorTime + "</span></br>";
                        opt += "<span>【连续错误次数】" + row.errorCount + "</span></br>";
                        return opt;
                    },
                    "targets": 2
                },
                 {
                     "render": function (data, type, row, me) {
                         var opt = "";
                         opt += "<span>【分类】" + row.categoryName + "</span></br>";
                         opt += "<span>【节点】" + row.nodeName + "</span></br>";
                         opt += "<span>【cron】" + row.cron + "</span></br>";
                         opt += "<span>【版本】" + row.version + "</span></br>";
                         opt += "<span>【修改时间】" + row.sLastModificationTime + "</span></br>";
                         opt += "<span>【创建时间】" + row.sCreationTime + "</span></br>";
                         opt += "<span>【创建人】" + row.creatorUserId + "</span></br>";
                         return opt;
                     },
                     "targets": 3
                 },
                {
                    "render": function (data, type, row, me) {
                        var opt = "";
                        opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" title="启动"><i class="glyphicon glyphicon-play"></i>启动</a>';
                        opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" title="立即执行"><i class="glyphicon glyphicon-flash"></i>立即执行</a>';
                        opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" title="停止"><i class="glyphicon glyphicon-pause"></i>停止</a>';
                        opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" title="重启"><i class="glyphicon glyphicon-refresh"></i>重启</a>';
                        opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" title="卸载"><i class="glyphicon glyphicon-remove"></i>卸载</a>';
                        opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" title="编辑"><i class="glyphicon glyphicon-edit"></i>编辑</a>';
                        opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" title="查看日志"><i class="glyphicon glyphicon-eye-open"></i>查看日志</a>';
                        return opt;
                    },
                    'orderable': false,
                    'class': "hidden",
                    "targets": 5
                }
            ]
        }
    });
    //搜索
    $("#btnSearch").click(function () {
        grid.submitFilter();
    });

})