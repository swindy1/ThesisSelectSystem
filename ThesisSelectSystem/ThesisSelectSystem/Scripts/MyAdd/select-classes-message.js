$(function () {
    $('#query').click(function (event) {
        event.preventDefault();
        alert("开始发送异步请求");
        $.ajax({
            url: "ShowClassInfo",
            type: "GET",
            dataType: "json",
            success: function(data) {
                alert("success");
                for (var i = 0, length = data.length; i < length; i++) {
                    var r = "row" + i;
                    var row = $("<tr></tr>").attr("id", r);
                    $("#tab").append(row);

                    for (var j in data[i]) {
                        var td = $("<td></td>").text(data[i][j]);
                        td.appendTo(row);
                    }
                }
            },
            error: function() {
                alert("发送Ajax请求失败");
            }
        });
    });

    $('#test').click(function () {
        alert("test");
    });
});