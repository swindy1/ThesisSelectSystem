/// <reference path="select-classes-message.js" />

$(document).onload= function() {
    $("#query").click = function() {
        alert("开始发送异步请求");
        $.ajax({
            url: "ShowClassInfo",
            type: "GET",
            dataType: "json",
            success: function(data) {
                alert("success");
                alert(data);
            },
            error: function() {
                alert("发送Ajax请求失败");
            }
        });
    };
    $('#test').click= function() {
        alert("test");
    }
}



//$(document).ready(function ()
//{
    
//});