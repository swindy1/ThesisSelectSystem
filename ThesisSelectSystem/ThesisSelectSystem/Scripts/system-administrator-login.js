$(function() {

    $('#login-button').click(function (event) {
        //提交数据给数据库
        event.preventDefault();
        $.ajax({
            type: "post",
            url: "SystemAdminMakeLogin",
            data: { Account: $("#Account").val(), Password: $("#Password").val() },//
            //async:true,
            success: function (data) {

                event.preventDefault();
                $("form").fadeOut(50);//表格淡出
                $(".wrapper").addClass('form-success');
                if (data == "1") {
                   
                    window.location.href = "../SystemAdmin/SystemAdministrator";
                    
                    return false;


                }
                else {
                    alert("登陆失败!");
                    window.location.href = "SALogin";
                }


            }

        });
        //检查输入是否为空	    
        if ($("input[type=text]").val().length == 0 || $("input[type=password]").val().length == 0) {
            alert("请检查账号或密码是否为空");
        }
    });




})