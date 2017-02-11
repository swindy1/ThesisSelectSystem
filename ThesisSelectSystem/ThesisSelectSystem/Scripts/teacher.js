$(document).ready(function(){
	//一开始将侧边栏的子菜单隐藏
	$("#student1").hide();
	$("#topic1").hide();
	$(".wode").hide();
	$(".situation").hide();
	$(".manage").hide();
	$(".protocol").hide();
	//当父菜单被点击的时候就显示或者隐藏
	$('.navigation button').eq(0).click(function(){
		$("#student1").toggle(200);
	});
	$('.navigation button').eq(1).click(function(){
		$(".selectstudent").show();
		$(".zini").show();
		$(".wode").hide();
		$(".manage").hide();
		$(".protocol").hide();
	});
	$('.navigation button').eq(2).click(function(){
		$(".selectstudent").show();
		$(".wode").show();
		$(".zini").hide();
		$(".manage").hide();
		$(".protocol").hide();
		
		
	});
	$('.navigation button').eq(3).click(function(){
		$("#topic1").toggle(200);
		
	});
	$('.navigation button').eq(4).click(function(){
		$(".manage").show()
		$(".situation").show();
		$(".allofthetopic").hide();
		$(".deletetopic").hide();
		$(".deletestudent").hide();
		$(".selectstudent").hide();
		$(".protocol").hide();
	});
	$('.navigation button').eq(5).click(function(){
		$(".manage").show()
		$(".allofthetopic").show();
		$(".situation").hide();
		$(".deletetopic").hide();
		$(".deletestudent").hide();
		$(".selectstudent").hide();
		$(".protocol").hide();
	});
	$('.navigation button').eq(6).click(function(){
		$(".manage").show()
		$(".deletestudent").show();
		$(".situation").hide();
		$(".allofthetopic").hide();
		$(".deletetopic").hide();
		$(".selectstudent").hide();
		$(".protocol").hide();
	});
	$('.navigation button').eq(7).click(function(){
		$(".manage").show()
		$(".deletetopic").show();
		$(".situation").hide();
		$(".allofthetopic").hide();
		$(".deletestudent").hide();
		$(".selectstudent").hide();
		$(".protocol").hide();
	});
	$('.navigation button').eq(8).click(function(){
		$(".selectstudent").hide();
		$(".manage").hide();
		$(".protocol").show();
	});
	//设置自动刷新时间函数每隔一秒就刷新
	setInterval("startRequest()",1000); 
	

});

function startRequest() 
{ 
	$(".realtime").text((new Date()).toLocaleString()); 
}

