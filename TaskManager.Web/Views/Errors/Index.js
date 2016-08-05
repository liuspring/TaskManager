(function () {
    $(function () {
        var grid = new InitDataTables();
        //var logService = abp.services.app.log;
        grid.init({
            src: $("#tb_error"),
            dataTable: {
                "sAjaxSource": "/Errors/AjaxList", // get地址
                //"pageLength": 2,
                //向服务器传额外的参数
                "fnServerParams": function (aoData) {
                    aoData.push(
                         { "name": "CommandName", "value": $("#Content").val() }
                    ); //查询内容
                },
                //配置列要显示的数据
                columns: [
                    //对应上面thead里面的序列 ;字段名字和返回的json序列的key对应
                    { "data": "id" },
                    { "data": "taskName" },
                    { "data": "nodeName" },
                    { "data": "creatorUserId" },
                    { "data": "msg" },
                    { "data": "sCreationTime" },
                    { "data": "sLogType" },
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
                            opt += '<a href="/Tasks/Edit/' + data + '" class="btn btn-primary btn-xs green" ' +
                                'title="查看详情"><i class="glyphicon glyphicon-eye-open"></i>查看详情</a>';
                            return opt;
                        },
                        'orderable': false,
                        'class': "hidden",
                        "targets": 7
                    }
                ]
            }
        });
        //搜索
        $("#btnSearch").click(function () {
            grid.submitFilter();
        });
    });
})();