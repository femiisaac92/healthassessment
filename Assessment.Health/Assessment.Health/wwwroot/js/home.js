
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
var GetCall = function (url) {    
    return $.ajax({
        type: "GET",
        url: url,       
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

    //  populate the table
    setTimeout(function () {
        GetCall("https://healthassessmentapi.herokuapp.com/api/Service")
            .success(function (data) {
                if (data != null) {
                    $.each(data, function (i, e) {
                        console.log(e);                       
                    });
                }
            }).fail(function (sender, message, details) {
                $(".error_message").html("<font color=red>Error! fetching records</font>");

            });       
    }, 2000);

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
        $(this).prop('disabled', true);
        PostCall("https://healthassessmentapi.herokuapp.com/api/v1/Patient/", data)
            .success(function (data) {
                $("#patientname").val(""); $("#phonenumber").val(""); $("#dateofbirth").val("");
                $("#title").val(""); $("#gender").val(""); $("textarea#contact").val("");
                $(".error_message").html("<font color='green'>Save Successfully</font>");
                $(this).prop('disabled', false);
            }).fail(function (sender, message, details) {
                $(".error_message").html("<font color=red>Error! not save</font>");
                $(this).prop('disabled', false);
         });        
    });// Click new patient

    // Submit Appointment
    $("form").submit(function (e) {
        e.preventDefault();
        if ($(this).attr("id") == "appointment-form") {
            var data = $(this).serialize();
            console.log(data);
            PostCall("https://healthassessmentapi.herokuapp.com/api/Service/", data)
                .success(function (data) {
                    $("#temp").val(""); $("#rrate").val(""); $("#prate").val(0);
                    $(".error_message").html("<font color='green'>Save Successfully</font>");
                    $(this).prop('disabled', false);
                }).fail(function (sender, message, details) {
                    $(".error_message").html("<font color=red>Error! not save</font>");
                    $(this).prop('disabled', false);
                });
        }
    });

    //$("button.saveappointment").click(function () {
    //    console.log($("#appointment").valid());
    //})

    $('#patient').select2({
        placeholder: 'Search term',   
        dropdownParent: $('#newPatientAppoint'),
        minimumInputLength: 1,
        ajax: {
            type: "GET",
            url: "https://healthassessmentapi.herokuapp.com/api/v1/Patient",          
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('token') },
            data: function (params) {
                var query = {
                    name: params.term                    
                }
                // Query parameters will be ?search=[term]&type=public
                return query;
            },
            cache: true,
            processResults: function (data) {
                var _data = [];
                for (var i = 0; i < data.length; i++) {
                    var obj = {};
                    obj.id = data[i].id;
                    obj.text = data[i].patientName + "/" + data[i].dateOfBirth.split("-")[0] + "/" + (data[i].gender==1 ? 'male' : 'female');
                    _data.push(obj);
                }
                return {
                    results: _data
                };
            }
        }
    });
    $('#symptoms').select2({
        placeholder: 'Search term',
        minimumInputLength: 1,
        dropdownParent: $('#newPatientAppoint'),
        ajax: {
            type: "GET",
            url: "https://healthassessmentapi.herokuapp.com/api/ApiMedic/Symptoms",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            headers: { "Authorization": 'Bearer ' + localStorage.getItem('token') },
            data: function (params) {
                var query = {
                    name: params.term
                }
                // Query parameters will be ?search=[term]&type=public
                return query;
            },
            cache: true,
            processResults: function (data) {
                var _data = [];
                for (var i = 0; i < data.data.length; i++) {
                    var obj = {};
                    obj.id = data.data[i].id;
                    obj.text = data.data[i].name;
                    _data.push(obj);
                }
                return {
                    results: _data
                };
            }
        }
    });

    // Onchange symptoms/patient
    $("#symptoms,#patient").change(function () {
        var value = $("#symptoms").val();
        var patient = $('#patient').find(':selected').text();
        console.log(patient);
        console.log("v",value);
        if (value != null && patient != "") {
            var patientSplit = patient.split("/");
            var gender = patientSplit[2];
            var year = patientSplit[1];
            GetCall("https://healthassessmentapi.herokuapp.com/api/ApiMedic/Diagnosis/" + value + "/" + gender + "/" + year)
                .success(function (data) {
                    if (data.data != null) {
                        $.each(data.data, function (i, e) {
                            console.log(e.issue);
                            CreateDiagnosisList(e.issue.id, e.issue.name, false);
                        });
                    }
                }).fail(function (sender, message, details) {
                    $(".error_message").html("<font color=red>Error! not save</font>");
                   
                });        
        }
    });

    function CreateDiagnosisList(id, name, clear = false) {
        var ele = '<div class="input-group mb-3"><div class="input-group-prepend"><div class="input-group-text"><input type="checkbox" name="DiagnosisIds[]" class="chkdiagnosis" value="' + id +'" aria-label="Checkbox for following text input"></div></div><input type="text" class="form-control valuediagnosis" name="DiagnosisName[]" value="' + name + '" aria-label="Text input with checkbox" readonly></div>';
        if (clear === true)
            $(".diagnosis").html("");
        $(".diagnosis").append(ele);
    }

});
$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});