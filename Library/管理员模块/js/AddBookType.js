function AddBookType() {
    var TypeName = $.trim($("#TypeName").val());
    if (TypeName == "") {
        alert("请输入书架名");
        return false;
    }
    alert(TypeName);

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/InsertBookType.ashx',
        data: {
            TypeName: TypeName
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
        window.location.href = "BookType.htm";
    }
    else { }
}