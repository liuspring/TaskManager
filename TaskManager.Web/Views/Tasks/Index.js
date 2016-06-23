$(function () {
    var grid = new initDataTables();
    grid.init({
        src: $("#tb_task"),
        dataTable: {
            "sAjaxSource": "/Tasks/AjaxTaskList", // get地址
            //"pageLength": 2,
            //向服务器传额外的参数
            "fnServerParams": function (aoData) {
                aoData.push(
                    //{ "name": "tab", "value": $("#navTab").val() }
                ); //查询内容
            },
            //配置列要显示的数据
            columns: [
                //对应上面thead里面的序列 ;字段名字和返回的json序列的key对应
                { "data": "id" },
                { "data": "taskName" },
                { "data": "state" },
                { "data": "categoryId" },
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
                        opt += '<a href="/User/CreateUser/' + data + '" class="btn default btn-xs green" title="编辑"><i class="fa fa-edit"></i>编辑</a>';
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
    $("#btnsearch").click(function () {
        grid.submitFilter();
    });

})