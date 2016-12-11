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
});
	function openwin(){
		window.open("page.html","newwindow","height=300,width=450,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no")
	}