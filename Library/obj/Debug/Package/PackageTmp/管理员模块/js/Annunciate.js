var AnnunciateID = "";

function SelectAnnunciate() {
    var tmpArr, QueryString;
    var URL = document.location.toString(); //获取带参URL
    if (URL.lastIndexOf("?") != -1) {
        QueryString = URL.substring(URL.lastIndexOf("?") + 1, URL.length);
        tmpArr = QueryString.split("&"); //分离参数
        for (i = 0; i <= tmpArr.length; i++) {
            try { eval(tmpArr[i]); }
            catch (e) {
                var re = new RegExp("(.*)=(.*)", "ig");
                re.exec(tmpArr[i]);
                try { eval(RegExp.$1 + "=" + "\"" + RegExp.$2 + "\""); }
                catch (e) { }
            }
        }
    }
    else {
        QueryString = "";
    }

    alert(AnnunciateID);

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectAnnunciate.ashx',
        data: {
            AnnunciateID: AnnunciateID
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                var lists = json.list;
                var Title = "";
                var Information = "";
                var PublishTime = "";
                var Display = "";

                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["Title"]) {
                        Title = lists[i]["Title"];
                    }
                    else {
                        Title = "";
                    }
                    if (lists[i]["PublishTime"]) {
                        PublishTime = lists[i]["PublishTime"];
                    }
                    else {
                        PublishTime = "";
                    }
                    if (lists[i]["Display"]) {
                        Display = lists[i]["Display"];
                    }
                    else {
                        Display = "";
                    }

                    if (lists[i]["Information"]) {
                        Information = lists[i]["Information"];
                    }
                    else {
                        Information = "";
                    }

                    $("#Title").attr("value", Title); //填充内容
                    $("#PublishTime").attr("value", PublishTime);
                    $("#Display").attr("value", Display) 
                    $("#Information").attr("value", Information);


                }
            }
            else {
                alert("查询失败，系统错误");

            }
        },
        error: function () {
            alert("请检查网络");
        }
    })
}



function exit() {
    if (confirm("是否确定退出通告详情？")) {
        window.location.href = "AnnunciateList.htm";
    }
}