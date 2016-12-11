$(function() {
    $('#addMajor').click(function() {
        alert("Please select major department");
        var majorName = $("#majorName").val();
        var departmentName = $('#loadDepartment option:selected').val();
        $.ajax({
            url: "/SystemAdmin/AddMajor",
            type: "POST",
            dataType: "json",
            data: { major: majorName, department: departmentName },
            success: function(data) {
                alert(data.major);
            },
            error: function() {
                alert("fail");
               
            }
        });
    });


    $('#delmajor').click(function () {
        
        var datas =[];
        var i = 0;
        $('input[name="delmajor"]:checked').each(function () {
            var temp = $(this).parent().parent();
            datas[i++] = $(this).val();
            temp.remove();
        });
        if (datas != null) {
            var choose = confirm("您确定要删除这专业？");
            var test = datas.join(',');
            alert(test);
            if (choose==true) {
                $.ajax({
                    url: "/SystemAdmin/DelMajor",
                    type: "POST",
                    dataType: "json",
                    data: { "test": test },
                    success: function (data) {
                        alert(data.message);
                    },
                    error: function () {
                        alert("删除失败！");
                    }

                });
            } else {
                return false;
            }
            
        }

    });
})