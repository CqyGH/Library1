function AddUser() {
    var StudentID = $.trim($("#StudentID").val());
    var UserName = $.trim($("#UserName").val());
    var Phone = $.trim($("#Phone").val());
    var Department = $.trim($("#Department").val());
    var Profession = $.trim($("#Profession").val());
    var Birthday = $.trim($("#Birthday").val());
    var Remark = $.trim($("#Remark").val());
    var Class = $("#Class").find("option:selected").text();
     var Sex = $("#Sex").find("option:selected").text();

     if (StudentID == "" || UserName == "" || Department == "" || Profession == "") {
         alert("请填写完整信息");
         return false;
     }
     else {
         $.ajax({
             type: 'POST',
             dataType: 'json',
             url: '../../Handlers/InsertUser.ashx',
             data: {
                 StudentID: StudentID,
                 UserName: UserName,
                 Phone: Phone,
                 Department: Department,
                 Profession: Profession,
                 Birthday: Birthday,
                 Remark: Remark,
                 Class: Class,
                 Sex: Sex
             },
             success: function (json) {
                 if (json.MessageCode == '0') {
                     $("#StudentID").attr("value", '');
                     $("#UserName").attr("value", '');
                     $("#Phone").attr("value", '');
                     $("#Department").attr("value", '');
                     $("#Profession").attr("value", '');
                     $("#Birthday").attr("value", '');
                     $("#Remark").attr("value", '');
                     $("#Class").attr("value", '2005级');
                     $("#Sex").attr("value", '男');
                     alert("增加成功");
                 }
                 else {
                     alert("增加失败，系统错误");
                 }
             },
             error: function () {
                 alert("请检查网络");
             }
         })
     }
 }

 function exit() {
     if (confirm("确定退出新增用户？")) {
         window.location.href = "ReaderList.htm";
     }
     else { 
     
     }
 }