$(function () {
    $('#query').click(
        function (event) {
        event.preventDefault();
        alert("开始发送异步请求");
        $(".NewAdd").remove();
        $.ajax({
            url: "ShowClassInfo",
            type: "GET",
            dataType: "json",
            success: function(data) {
                alert("success");
                for (var i = 0, length = data.length; i < length; i++) {
                    var r = "row" + i;
                    var row = $("<tr></tr>").attr("id", r).attr("class","NewAdd");
                    $("#tab").append(row);
                    var k = 0;
                    for (var j in data[i]) {
                        var col = r + k;
                        var td = $("<td></td>").text(data[i][j]).attr("id",col);
                        td.appendTo(row);
                        k=k+1;
                    }

                    var edit = $("<td></td>");
                    edit.appendTo(row);
                    var alter = $("<button></button>")
                        .attr("class", "btn btn-primary")
                        .attr("type", "button").attr("id", r + k++)
                        .click(function() {
                            alert("update");
                            var id = $(this).parent().parent().children("td:eq(0)").text();
                            var num = $(this).parent().parent().children("td:eq(2)").text();
                            var name = $(this).parent().parent().children("td:eq(3)").text();
                            var year = $(this).parent().parent().children("td:eq(4)").text();
                            alert(num + " " + name + "  " + year + " " + id);
                            var url = "../Home/Alter?ClassId=" + id + "&ClassName=" + name + "&Number=" + num + "&GraduateYear=" + year;
                            window.location.href = url;
                        });
                    $("<span></span>")
                        .attr("class", "glyphicon glyphicon-edit")
                        .appendTo(alter);
                    alter.appendTo(edit);
                    

                    var del = $("<button></button>")
                        .attr("class", "btn btn-primary")
                        .attr("type", "button")
                        .attr("id", r + k)
                        .click(function() {
                            alert("delete");
                            var id = $(this).parent().parent().children("td:eq(0)").text();
                            var name = $(this).parent().parent().children("td:eq(3)").text();
                            alert(id);
                            var choose = confirm("您确定要删除 " + name + " 的记录?");
                            if (choose == true) {
                                var targetUrl = "../Home/DeleteClass?classId=" + id;
                                $.ajax({
                                    url: targetUrl,
                                    type: "get",
                                    dataType: "json",
                                    success: function (data) {
                                        alert(data);
                                        myAjax(event);
                                    },
                                    error: function(data) {
                                        alert(data);
                                    }
                                    //complete: function() {
                                    //    alert("I've been called");
                                    //}
                                });
                            } else {
                                return false;
                            }

                        });
                    $("<span></span>")
                        .attr("class", "glyphicon glyphicon-trash")
                        .appendTo(del);
                    del.appendTo(edit);
                }
            },
            error: function() {
                alert("发送Ajax请求失败");
            }
        });
    }
    );


    $('#addBtn').click(function () {
        self.location ='/Home/CreateClasses';
    });


    function myAjax(event){
        event.preventDefault();
        $(".NewAdd").remove();
        $.ajax({
            url: "ShowClassInfo",
            type: "GET",
            dataType: "json",
            success: function(data) {
                alert("success");
                for (var i = 0, length = data.length; i < length; i++) {
                    var r = "row" + i;
                    var row = $("<tr></tr>").attr("id", r).attr("class","NewAdd");
                    $("#tab").append(row);
                    var k = 0;
                    for (var j in data[i]) {
                        var col = r + k;
                        var td = $("<td></td>").text(data[i][j]).attr("id",col);
                        td.appendTo(row);
                        k=k+1;
                    }

                    var edit = $("<td></td>");
                    edit.appendTo(row);
                    var alter = $("<button></button>")
                        .attr("class", "btn btn-primary")
                        .attr("type", "button").attr("id", r + k++)
                        .click(function() {
                            alert("update");
                            var id = $(this).parent().parent().children("td:eq(0)").text();
                            var num = $(this).parent().parent().children("td:eq(2)").text();
                            var name = $(this).parent().parent().children("td:eq(3)").text();
                            var year = $(this).parent().parent().children("td:eq(4)").text();
                            //alert(num + " " + name + "  " + year + " " + id);
                            var url = "../Home/Alter?ClassId=" + id + "&ClassName=" + name + "&Number=" + num + "&GraduateYear=" + year;
                            window.location.href = url;
                        });
                    $("<span></span>")
                        .attr("class", "glyphicon glyphicon-edit")
                        .appendTo(alter);
                    alter.appendTo(edit);
                    

                    var del = $("<button></button>")
                        .attr("class", "btn btn-primary")
                        .attr("type", "button")
                        .attr("id", r + k)
                        .click(function() {
                            //alert("delete");
                            var id = $(this).parent().parent().children("td:eq(0)").text();
                            var name = $(this).parent().parent().children("td:eq(3)").text();
                            //alert(id);
                            var choose = confirm("您确定要删除 " + name + " 的记录?");
                            if (choose == true) {
                                var targetUrl = "../Home/DeleteClass?classId=" + id;
                                $.ajax({
                                    url: targetUrl,
                                    type: "get",
                                    dataType: "json",
                                    success: function (data) {
                                        alert(data);
                                        myAjax(event);
                                    },
                                    error: function(data) {
                                        alert(data);
                                    }
                                });
                            } else {
                                return false;
                            }

                        });
                    $("<span></span>")
                        .attr("class", "glyphicon glyphicon-trash")
                        .appendTo(del);
                    del.appendTo(edit);
                }
            },
            error: function() {
                alert("发送Ajax请求失败");
            }
        });
    }
   

});