function InsertManage() {
    var ManagementName = $.trim($("#ManagementName").val());
    var Sex = $("#Sex").find("option:selected").text();
    var Education = $.trim($("#Education").val());
    var Birthday = $.trim($("#Birthday").val());
    var Phone = $.trim($("#Phone").val());
    var ManagementAddress = $.trim($("#ManagementAddress").val());
    var Remark = $.trim($("#Remark").val());
//    alert("ManagementName:" + ManagementName);
//    alert("Sex:" + Sex);
//    alert("Education:" + Education);
//    alert("Birthday:" + Birthday);
//    alert("Phone:" + Phone);
//    alert("ManagementAddress:" + ManagementAddress);
//    alert("Remark:" + Remark);

    if (ManagementName == "") {
        alert("请输入管理员名！");
        return false;
    }
    else {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/InsertManagement.ashx',
            data: {
                ManagementName: ManagementName,
                Sex: Sex,
                Education: Education,
                Birthday: Birthday,
                Phone: Phone,
                ManagementAddress: ManagementAddress,
                Remark: Remark
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    $("#ManagementName").attr("value", '');
                    $("#Education").attr("value", '');
                    $("#Birthday").attr("value", '');                
                    $("#Phone").attr("value", '');
                    $("#Remark").attr("value", '');
                    $("#ManagementAddress").attr("value", '');
                    $("#Sex").attr("value", '请选择');
                    alert("新增成功!");

                }
                else {
                    alert("新增失败，请检查网络!");
                }
            },
            error: function () {
                alert("系统错误");
            }
        })
    }
}


function exit() {
    if (confirm("确定退出添加管理员？")) {
        window.location.href = "ManagementList.htm";
    }
    else {

    }
}