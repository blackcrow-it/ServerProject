// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(Document).ready(function () {
    $('.bthEdit').click(function () {
       
        var Id = $(this).parent().parent().find('td:eq(1)').text();
        
        
        $("#Id").val(Id);
       

    });
})

$(document).ready(function () {
    $("#bth_editFil").click(function () {
        if ($("#Id").val() != "") {


            $.ajax({
                url: "/Marks/EditMark?id=" + $('#Id').val(),
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