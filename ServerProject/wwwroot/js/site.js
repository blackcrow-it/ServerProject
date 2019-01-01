// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(Document).ready(function () {
    $('.bthEdit').click(function () {
        var cId = $(this).parent().parent().find('td:eq(0)').text();
        var Id = $(this).parent().parent().find('td:eq(1)').text();
        var roll = $(this).parent().parent().find('td:eq(2)').text();
        
        $("#Id").val(Id);
        $("#rollN").val(roll);
        $("#cId").val(cId);
        
    });
})

$(document).ready(function () {
    $("#bth_editFil").click(function () {
        if ($("#Id").val() != "") {


            $.ajax({
                url: "/Accounts/EditMark?id=" + $('#Id').val(),
                type: 'POST',

                data: $('form').serialize(),

                success: function (data) {
                    alert("Update success");
                    location.reload();
                },
                error: function (data) {
                    alert("Update fail");
                }
            });
        }
    });
})
$(document).ready(function () {
    $("#ava").change(function () {
        var img = $('#ava').val();
        $('#imgs').attr('src', img);
    });
})
$(document).ready(function () {
    $('#avachange').click(function() {
      if ($('#ava').val()=="") {
    $('#ava').val("http://ssl.gstatic.com/accounts/ui/avatar_2x.png");
    }
    })
})
