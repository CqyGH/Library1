function readerUserInfo() {
var judge=0;
$.ajax({
    type: 'POST',
    dataType: 'json',
    data: {
        judge: judge
    },
    url: '../../Handlers/SelectManage.ashx',
    chache: false,
    success: function (json) {
        if (json.MessageCode == '0') {
            $("UserName").html(json.JsonResponse.ManagementName + ' 欢迎您! <a href=\'javascript:void;\' onclick=\'userLogout();\'>退出</a>');
        }
        else {
            alert(json.JsonResponse);
            window.location.href = '../../Login.htm';

        }

    },
    error: function () {
        alert('加载失败请检查您的网络!');
    }
    });
}

function userLogout() {
    var judge = 1;
    $.ajax({
        type: 'POST',
        dataType: 'json',
        data: {
        judge：judge
        },
        url: '../../Handlers/SelectManage.ashx',
        cache: false,
        success: function (json) {
            window.location.href = '../../Login.htm';
        },
        error: function () {
            alert('加载失败请检查您的网络!');
        }
    });
}