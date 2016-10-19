$(document).ready(function ()
{
    $('#sub').click(function (event)
    {
        event.preventDefault();
        alert("测试是否会提交数据");
        var url = $(this).attr('href');
        $('#sub').load(url);
    });
});