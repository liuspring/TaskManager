$(function () {
    //初始化fileinput控件（第一次初始化）
    initFileInput("FileUpload", "/Upload/UploadFile", ["zip"]);

    //上传后触发的事件
    $("#FileUpload").on("fileuploaded", function (event, data, previewId, index) {
        if (data.response.result.ret) {
            $("#divFileZipName span b").html(data.response.result.filename);
            $("#FileZipName").val(data.response.result.filename);
            $("#FileZipPath").val(data.response.result.filesavename);
        } else {
            $("#FileUpload").on("fileclear");
            gDialog.fAlert("上传文件失败！");
        }
    }).on("filecleared", function (event, data, previewId, index) {
        $("#divFileZipName span b").html("");
        $("#FileZipName").val("");
        $("#FileZipPath").val("");

    });

    $("#categoryForm").bootstrapValidator({
        message: '该值不可以为空',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        }
    }).on('success.form.bv', function (e) { //点击提交之后
        if ($("#FileZipName").val() == "") {
            gDialog.fAlert("上传打包文件不可为空！");
            $("#btnSave").removeAttr("disabled");
            return false;
        }
        e.preventDefault();
        var $form = $(e.target);
        var bv = $form.data('bootstrapValidator');
        $.post($form.attr('action'), $form.serialize(), function (result) {
            if (result.ret) {
                message_box.show('<h4>保存成功！</h4>', 'success');
                window.location.href = "/tasks/index";
            } else {
                message_box.show('<h4>保存失败：</h4>' + result.msg + '！', 'danger');
                $("#btnSave").removeAttr("disabled");
            }
        });
    });
});