
var studentVm = null;


function handleJs(data)
{
    studentVm = ko.observable(data);
}



$(document).ready(function () {

    $.ajax({
        type: 'GET',
        url: '/Account/GetStudentViewModel/',
        success: function (jsonData) {
            handleJs(jsonData);
        }
    }).done(function() {
        ko.applyBindings(studentVm);

    });
});


function OnEditedSucess(data) {
    
}