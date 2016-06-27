(function () {
    $(function () {
        var grid = new InitDataTables();
        var commandService = abp.services.app.command;
        var $modal = $("#CommandModal");
        var $form = $modal.find('form');
        //保存
        $form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            if (!$form.valid()) {
                return;
            }
            var command = $form.serializeFormToObject();
            abp.ui.setBusy($modal);
            if (command.Id == "") {
                commandService.create(command).done(function () {
                    grid.submitFilter();
                    $modal.modal("hide");
                }).always(function () {
                    abp.ui.clearBusy($modal);
                });
            } else {
                commandService.update(command).done(function () {
                    grid.submitFilter();
                    $modal.modal("hide");
                }).always(function () {
                    abp.ui.clearBusy($modal);
                });
            }

        });

        $modal.on('shown.bs.modal', function () {
            $modal.find('input:not([type=hidden]):first').focus();
        });

        //编辑
        $("#tb_command").on("click", "a.edit", function () {
            commandService.getCommand($(this).data("value")).done(function (data) {
                $modal.modal("show");
                $("#Id").val(data.id);
                $("#CommandName").val(data.commandName);
                $("#CommandJson").val(data.commandJson);
                $("#TaskId").val(data.taskId);
                $("#NodeId").val(data.nodeId);

            });
        });


        grid.init({
            src: $("#tb_command"),
            dataTable: {
                "sAjaxSource": "/Commands/AjaxCommandList", // get地址
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
                    { "data": "sCommandName" },
                    { "data": "sCommandState" },
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
                            opt += '<a href="javascript:void(0)" data-value="' + data + '" ' +
                                'class="btn default btn-xs green edit" title="编辑"><i class="fa fa-edit"></i>编辑</a>';
                            return opt;
                        },
                        'orderable': false,
                        'class': "hidden",
                        "targets": 3
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