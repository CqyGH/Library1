function AddBookType() {
    var CaseName = $.trim($("#CaseName").val());
    if (CaseName == "") {
        alert("请输入书架名");
        return false;
    }
    alert(CaseName);

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/InsertCase.ashx',
        data: {
            CaseName: CaseName
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                alert("添加成功");
            }
            else {
                alert("添加失败，系统错误");
            }
        },
        error: function () {
            alert("请检查网络");
        }
    })
}

function exit() {
    if (confirm("是否确定退出添加图书类型？")) {
        window.location.href = "CaseList.htm";
    }
    else { }
}