var PostCall = function (url, data) {
    var json_data = JSON.stringify(data);
    return $.ajax({
        type: "POST",
        url: url,
        data: json_data,
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        headers: { "Authorization": 'Bearer ' + localStorage.getItem('token') }
    });
}
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

    // Save patient
    $("button.savepatient").click(function () {
        $(".error_message").html("");
        var patientname = $("#patientname").val();
        var phonenumber = $("#phonenumber").val();
        var dateofbirth = $("#dateofbirth").val();
        var title = $("#title").val();
        var gender = $("#gender").val();
        var contact = $("textarea#contact").val();
        var data = {};
        data.PatientName = patientname; data.ContactName = phonenumber; data.ContactTitle = title;
        data.Address = contact; data.City = ""; data.Region = ""; data.PostalCode = "";
        data.Country = "Nigeria"; data.Phone = phonenumber; data.Gender = gender; data.DateOfBirth = dateofbirth;
        console.log(data);
        if (patientname == "" || phonenumber == "" || dateofbirth == "" || gender == "" || contact == "") {
            $(".error_message").html("<font color='red'>All fields required</font>");
            return;
        }
        PostCall("https://healthassessmentapi.herokuapp.com/api/v1/Patient/", data)
            .success(function (data) {
                $("#patientname").val(""); $("#phonenumber").val(""); $("#dateofbirth").val("");
                $("#title").val(""); $("#gender").val(""); $("textarea#contact").val("");
                $(".error_message").html("<font color='green'>Save Successfully</font>");
            }).fail(function (sender, message, details) {
                $(".error_message").html("<font color=red>Error! not save</font>");
         });        
    });// Click new patient
});

