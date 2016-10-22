$(document).ready(function ()
{
    $('#sub').click(function (event)
    {
        event.preventDefault();
        //alert("测试是否会提交数据");
        ////var url = $(this).attr('href');
        //var url = window.location.href;
        //$('#sub').load(url);
        var classes =
        {
            MajorId: null,
            GraduateYear: null,
            MonitorId: null,
            ClassName: $('#Name').val(),
            ClassId: $('#Id').val(),
            HumanNumber: $('#Num').val(),
        };

        $.ajax(
            {
                type: "POST",
                url: "JsonClasses",
                data: classes,
                success: function(data) {
                    //var message = data.ClassName + "\n" + data.ClassId + "\n" + data.HumanNumber;
                    alert(data.ClassName);
                    $('#NewId').val(data.ClassId);
                    $('#NewName').val(data.ClassName);
                    $('#NewNum').val(data.HumanNumber);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                     alert("error");
                     alert(XMLHttpRequest);
                     alert(errorThrown);
                     alert(textStatus);
                }


            }
        );
    });


});