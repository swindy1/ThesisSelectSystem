$(document).ready(function ()
{
    $('#subNewClass').click(function (event)
    {
        event.preventDefault();
        //alert("测试是否会提交数据");
        ////var url = $(this).attr('href');
        //var url = window.location.href;
        //$('#sub').load(url);
        var classes =
        {
            MajorName: $('#MajorName').find("option:selected").text(),
            ClassName: $('#ClassName').val(),
            HumanNumber: $('#Number').val(),
        };

        $.ajax(
            {
                type: "POST",
                url: "JsonClasses",
                data: classes,
                success: function(data) {
                    //var message = data.ClassName + "\n" + data.ClassId + "\n" + data.HumanNumber;
                    alert(data.ClassName);
                    self.location = '../Home/ShowClassInfo';
                   
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