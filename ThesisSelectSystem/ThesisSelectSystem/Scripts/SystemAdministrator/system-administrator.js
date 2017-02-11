$(document).ready(function(){
	
	$(".grade-main").hide();
	$(".addAminstrator").hide();
	$(".deleteAminstrator").hide();
	$(".addAparment").hide();
	$(".deleteAparment").hide();
	$(".subject-setup").hide();
	$(".intersetup").hide();
	
	$("#system").click(function(){
            
            $("#system-setup").toggle(200);
	});
	
	$("#apartment").click(function(){
		    $("#apartment-setup").toggle(200);
	});
	
	$("#setGrade").click(function(){
		$(".anm-main").hide();
		$(".addAminstrator").hide();
		$(".addAparment").hide();
		$(".deleteAminstrator").hide();
		$(".deleteAparment").hide();
		$(".subject-setup").hide();
		$(".intersetup").hide();
		$(".grade-main").show();
	});
	
	$("#writrAnoucement").click(function(){
		$(".anm-main").show();
		$(".grade-main").hide();
		$(".addAminstrator").hide();
		$(".addAparment").hide();
		$(".deleteAminstrator").hide();
		$(".deleteAparment").hide();
		$(".subject-setup").hide();
		$(".intersetup").hide();
	});
	
	$("#addAparment").click(function(){
		$(".anm-main").hide();
		$(".grade-main").hide();
		$(".addAminstrator").hide();
		$(".deleteAminstrator").hide();
		$(".deleteAparment").hide();
		$(".subject-setup").hide();
		$(".intersetup").hide();
		$(".addAparment").show();
	});
	$("#deleteAparment").click(function(){
		$(".anm-main").hide();
		$(".grade-main").hide();
		$(".addAminstrator").hide();
		$(".deleteAminstrator").hide();
		$(".subject-setup").hide();
		$(".intersetup").hide();
		$(".addAparment").hide();
		$(".deleteAparment").show();
	});
	$("#addAminstrator").click(function(){
		$(".anm-main").hide();
		$(".grade-main").hide();
		$(".addAminstrator").show();
		$(".deleteAminstrator").hide();
		$(".deleteAparment").hide();
		$(".subject-setup").hide();
		$(".intersetup").hide();
		$(".addAparment").hide();
	});
	$("#deleteAminstrator").click(function(){
		$(".anm-main").hide();
		$(".grade-main").hide();
		$(".addAminstrator").hide();
		$(".deleteAminstrator").show();
		$(".deleteAparment").hide();
		$(".subject-setup").hide();
		$(".intersetup").hide();
		$(".addAparment").hide();
	});
	$("#subject-setup").click(function(){
		$(".anm-main").hide();
		$(".grade-main").hide();
		$(".addAminstrator").hide();
		$(".deleteAminstrator").hide();
		$(".deleteAparment").hide();
		$(".addAparment").hide();
		$(".intersetup").hide();
		$(".subject-setup").show();
	});
	$("#interface").click(function(){
		$(".anm-main").hide();
		$(".grade-main").hide();
		$(".addAminstrator").hide();
		$(".deleteAminstrator").hide();
		$(".deleteAparment").hide();
		$(".addAparment").hide();
		$(".intersetup").show();
		$(".subject-setup").hide();
	});
	$("#institution1 a").click(function(){
		var r = confirm("确定删除该学院？")
		
	})
	$("#adminstrator1 a").click(function(){
		var s = confirm("确定删除该管理员及其所有信息？")
	});
	
	$("#left").click(function(){
		$(".vertical").css("float","left");
		$(".contain").css("float","right");
		$(".search").css("float","right");
		
	});
	$("#right").click(function(){
		$(".vertical").css("float","right");
		$(".contain").css("float","left");
		$(".search").css("float","left");
	});
	
	$("#color1").click(function(){
		$(".contain").css("background-color","red");
	});
	$("#color2").click(function(){
		$(".contain").css("background-color","brown");
	});
	$("#color3").click(function(){
		$(".contain").css("background-color","blueviolet");
	});
	$("#color4").click(function(){
		$(".contain").css("background-color","#445588");
	})
	$("#moren").click(function(){
		$(".contain").css("background-color","#FFFFFF");
	});
	
	
	var va = document.getElementsByClassName("ch");
		var  vas = va.length;
		var i = 0;		
		$("#major-setup .delete").click(function(){
			while(va[i].checked==false&&i<vas){
			i++;
		};
			if(i<vas){
			var vad = confirm("确定删除所选专业？");
		}
			else{
				return 0;
			}
		});


		$("#addepart").click(function () {
		    var texts = $("#addDepartment").val();
		    if (texts!=null) {
		        $.ajax({
		            type: "POST",
		            url: "/SystemAdmin/AddDepartment",
		            data: {text:texts},
		            dataType: "json",
                    success: function(data) { 
                        $("<li></li>").text(texts).insertAfter($("#schoollist"));
                        alert(data.result);
                    },
                    error: function() {
                       alert("fail");
                    }
		        });
		    } else {
                alert("未填写任何二级学院名称");
		    }

		});

    //$("#teacherIdList").change(function() {
    //    //alert($("#teacherIdList option:selected").text());
    //    var tid = $("#teacherIdList option:selected").attr('id');
    //    var index = tid.substring(tid.length - 1, tid.length);
    //    //alert(tid + " 索引号为" + index);
    //    var selectId = "#tname" + index;
    //    $(selectId).attr("selected", "selected");
    //});
	//$("#teacherIdList").attr("disabled", "disabled");
	//$("#teacherNameList").change(function () {
	//    $("#teacherNameList option:selected").attr("selected", "selected");
	//    var text = $("#teacherNameList option:selected").innerText;
    //});
		function universalAjax(type, url, data, dataType, successFunction, errorFunction, completeFunction) {
        $.ajax({
            type: type,
            url: url,
            data: data,
            dataType: dataType,
            success: successFunction,
            error: errorFunction,
            complete:completeFunction
        });
		};

		function myUniversalAjax(type, url, data, dataType, successFunction, errorFunction) {
		    $.ajax({
		        type: type,
		        url: url,
		        data: data,
		        dataType: dataType,
		        success: successFunction,
		        error: errorFunction
		    });
		};


    $("#addAdminBtn").click(function() {
        var item = $("#teacherNameList option:selected");
        var id = item.attr('id');
        var name = item.val();
        var departmentName = $("#departmentNameSelect option:selected").val();
        alert("管理员id为：" + id + "  姓名为：" + name);

        function success(data) {
            alert(data.tip);
            var deleteItem = $("#teacherNameList option:selected");
            deleteItem.remove();
            alert("管理员id为：" + id + "  姓名为：" + name+" 系名为："+departmentName);
            var newRow = $("<tr></tr>").attr("id", id);
            var cell0 = $("<td></td>").text(id);
            var cell1 = $("<td></td>").text(name);
            var cell2 = $("<td></td>").text(departmentName);
            cell0.appendTo(newRow);
            cell1.appendTo(newRow);
            cell2.appendTo(newRow);
            newRow.appendTo($("#adminList"));

            var btn = $("<button></button>")
                .attr("class", "btn btn-default")
                .attr("type", "button")
                .attr("name", "deleteAdmin");
            btn.text("删除");
            var deleteRow = $("<tr></tr>");
            var column1 = $("<td></td>").text(id).attr("id", "dAdminId" + id);
            var column2 = $("<td></td>").text(name);
            var column3 = $("<td></td>").text(departmentName);
            btn.appendTo(deleteRow);
            column1.appendTo(deleteRow);
            column2.appendTo(deleteRow);
            column3.appendTo(deleteRow);
            deleteRow.appendTo($("#deleteAdminTb"));
        };

        var type = "post";
        var url = "/SystemAdmin/AddAdmin";
        var data = { id: id, name: name, departmentName: departmentName };
        var dataType = "json";
        function error(errorThrown) {
            alert(errorThrown);
        };
        myUniversalAjax(type, url, data, dataType, success, error);
        

    });

    $("button[name='deleteAdmin']").each(function() {
        $(this).click(function () {
            var deleteRow = $(this).parent().parent();
            var adminId = $(this).parent().parent().children().eq(1).text();
            var name = $(this).parent().parent().children().eq(2).text();
            alert(adminId);
            var data = { id: adminId };
            var type = "post";
            var url = "/SystemAdmin/DeleteAdmin";
            var dataType = "json";

            function success(data) {
                alert(data.tip);
                deleteRow.remove();
                var itemId = "#" + adminId;
                $(itemId).remove();
                var optionEle = $("<option>"+name+"</option>").attr("id", adminId);
                optionEle.appendTo($("#teacherNameList"));
            };
            function error(errorThrown) {
                alert(errorThrown);
            };
            myUniversalAjax(type, url, data, dataType, success, error);
        });
    });

});
	function openwin() {
	    window.open("page.html", "newwindow", "height=300,width=450,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no");
	}


       