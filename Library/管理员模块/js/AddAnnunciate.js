function addAnnunciate() {
    var Title = $.trim($("#Title").val());
    var Information = $.trim($("#Information").val());
    if (Title == "" ) {
        alert("请输入标题");
        return false;
    }
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/InsertAnnunciate.ashx',
        data: {
            Title: Title,
            Information: Information
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                $("#Title").attr("value", '');
                $("#Information").attr("value", '');
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

function exit() {
    if (confirm("确定退出添加通告？")) {
        window.location.href = "AnnunciateList.htm";
    }
    else {

    }
}