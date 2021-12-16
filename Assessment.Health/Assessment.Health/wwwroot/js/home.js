$(function () {
    if (typeof (Storage) !== "undefined") {
        if (localStorage.getItem("token") === "undefined" || localStorage.getItem("token") == "") {
            document.location.href = "index.html";
            alert("Session Log out")
        } else {
            $("#usernamedetail").html(localStorage.getItem("username") + "(" + localStorage.getItem("email") + ")")
        }
        $("#usernamedetail").val(localStorage.getItem("token"))
    } else {
        document.location.href = "index.html";
        alert("Browser not supported")
    }
    $('#newPatient').on('shown.bs.modal', function () {
        //$('#myInput').trigger('focus')
    });
    $('#newPatientAppoint').on('shown.bs.modal', function () {
        //  $('#myInput').trigger('focus')
    });
});

