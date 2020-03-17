$(document).ready(function () {
    //$('.ConfirmDelete').off('click').on('click'), function (e) {
    //    e.preventDefault();

    //}
});
//Show popup Delete and get UserId
function ConfirmDelete(Id) {
    $("#hiddenUserId").val(Id);
    $("#myModal").modal('show');
}
//Call ajax and Delete User
function DeleteUser() {
    $('#loaderDiv').show();

    var userId = $('#hiddenUserId').val();

    $.ajax({
        url: "/Admin/User/DeleteUser",
        type: "POST",
        dataType: "json",
        data: {
            Id: userId
        },
        success: function (result) {
            if (result) {
                $('#loaderDiv').hide();
                $('#myModal').modal('hide');
                $('#row_' + userId).remove();
                $('#myModal_1').modal('show');
            } else {
                $('#myModal_2').modal('show');
            }
        }
    })
}