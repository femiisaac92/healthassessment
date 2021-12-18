
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
var PostCallData = function (url, data) {
    //var json_data = JSON.stringify(data);
    return $.ajax({
        type: "POST",
        url: url,
        data: data,
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
        PopulateDataTable();      
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
            var data = {};
            $(this).serializeArray().map(function (x) { data[x.name] = x.value; }); 
            var SymptomName = $('#symptoms').find(':selected').text();
            var diagnosisId = [];
            var diagnosisName = [];
            $(".chkdiagnosis:checked").each(function (i, e) {
                diagnosisId[i] = $(e).val();
                diagnosisName[i] = $(e).data("name");
            });                        
            if (diagnosisId.length <= 0) {
                alert("No diagnosis selected")
                return;//stop here
            }          
            data.DiagnosisIds = diagnosisId;
            data.DiagnosisName = diagnosisName;
            data.SymptomName = SymptomName;
            console.log(data);
            PostCall("https://localhost:44356/api/Service/", data)
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
            var specialisations = [];
            GetCall("https://healthassessmentapi.herokuapp.com/api/ApiMedic/Diagnosis/" + value + "/" + gender + "/" + year)
                .success(function (data) {
                    if (data.data != null) {
                        $(".diagnosis").html("");
                        $(".specialisation").append("");
                        $.each(data.data, function (i, e) {
                            console.log(e.specialisation);
                            CreateDiagnosisList(e.issue.id, "(" + e.issue.icd + ")" + e.issue.name, i);
                            $.each(e.specialisation, function (y, f) {                                
                                specialisations[f.id] = f.name;
                            });                            
                        });
                        $.each(specialisations, function (x, z) {
                            if (specialisations[x] != null && specialisations[x] != 'undefined')                               
                                CreateSpecialtyList(x, z, x)
                            console.log(z);
                        });     
                    }
                }).fail(function (sender, message, details) {
                    $(".error_message").html("<font color=red>Error! not save</font>");
                   
                });        
        }
    });

    function CreateDiagnosisList(id, name,i) {
        var ele = '<div class="input-group mb-3"><div class="input-group-prepend"><div class="input-group-text"><input type="checkbox"  class="chkdiagnosis" value="' + id +'" aria-label="Checkbox for following text input" data-name="'+ name +'"></div></div><input type="text" class="form-control valuediagnosis"  value="' + name + '" aria-label="Text input with checkbox" readonly></div>';              
        $(".diagnosis").append(ele);
    }
    function CreateSpecialtyList(id, name, i) {
        var ele = '<div class="input-group mb-3"><div class="input-group-prepend"><div class="input-group-text"><input type="radio" name="Specialisation" class="chkspecialisation" value="' + name + '" aria-label="Checkbox for following text input" data-name="' + name + '" required></div></div><input type="text" class="form-control valuespecialisation"  value="' + name + '" aria-label="Text input with checkbox" readonly></div>';
        $(".specialisation").append(ele);
    }
    
});
function PopulateDataTable() {
    GetCall("https://healthassessmentapi.herokuapp.com/api/Service/appointment")
        .success(function (data) {
            if (data != null) {
                $.each(data, function (i, e) {
                    console.log(e);
                });
            }
        }).fail(function (sender, message, details) {
            $(".error_message").html("<font color=red>Error! fetching records</font>");

        }); 
}
$(document).on('select2:open', () => {
    document.querySelector('.select2-search__field').focus();
});
setInterval(function () { $(".error_message").html(""); }, 60000);