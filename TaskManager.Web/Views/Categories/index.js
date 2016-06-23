(function () {
    $(function () {
        var grid = new InitDataTables();
        var categoryService = abp.services.app.category;
        var $modal = $("#CategoryModal");
        var $form = $modal.find('form');
        //保存
        $form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            if (!$form.valid()) {
                return;
            }
            var category = $form.serializeFormToObject();
            abp.ui.setBusy($modal);
            if (category.Id == "") {
                categoryService.create(category).done(function() {
                    grid.submitFilter();
                    $modal.modal("hide");
                }).always(function() {
                    abp.ui.clearBusy($modal);
                });
            } else {
                categoryService.update(category).done(function () {
                    grid.submitFilter();
                    $modal.modal("hide");
                }).always(function () {
                    abp.ui.clearBusy($modal);
                });
            }

        });
        
        $modal.on('shown.bs.modal', function () {
            //$model.find('input:not([type=hidden])').val("");
            $modal.find('input:not([type=hidden]):first').focus();
        });

        //编辑
        $("#tb_category").on("click", "a.edit", function () {
            categoryService.getCategory($(this).data("value")).done(function (data) {
                $modal.modal("show");
                $("#Id").val(data.id);
                $("#CategoryName").val(data.categoryName);
                
            });
        });


        grid.init({
            src: $("#tb_category"),
            dataTable: {
                "sAjaxSource": "/Categories/AjaxCategoryList", // get地址
                //"pageLength": 2,
                //向服务器传额外的参数
                "fnServerParams": function (aoData) {
                    aoData.push(
                        { "name": "CategoryName", "value": $("#Content").val() }
                    ); //查询内容
                },
                //配置列要显示的数据
                columns: [
                    //对应上面thead里面的序列 ;字段名字和返回的json序列的key对应
                    { "data": "id" },
                    { "data": "categoryName" },
                    { "data": "sCreationTime" },
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