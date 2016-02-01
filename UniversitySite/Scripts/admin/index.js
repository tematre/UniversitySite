function Subject() {
    var self = this;

    self.id = ko.observable();
    self.name = ko.observable();

}

function Professor() {
    var self = this;
    self.id = ko.observable();
    self.name = ko.observable();
    self.surname = ko.observable();
    self.subjects = ko.observableArray();
}

var isInit = false;
var viewModel =
{
    addSubjectVm: {
        professors: ko.observableArray(),
        selectedProfessorId: ko.observable(),
        subjectName: ko.observable()
    },
    professors: ko.observableArray()
};

function changeSubject(subject) {

    $.ajax({
        url: "/Admin/RenameSubject/",
        type: "POST",
        data: {
            id: subject.id,
            name: subject.name
        },
        success: function() {
            loadData();
        }
    });
}

function addNewSubject() {
    $.ajax({
        url: "/Admin/AddSubjectToProfessor/",
        type: "POST",
        data: {
            professorId: viewModel.addSubjectVm.selectedProfessorId(),
            subjectName: viewModel.addSubjectVm.subjectName()
        },
        success: function() {
            loadData();
        }
    });
}

function removeSubject(subject) {
    $.ajax({
        url: "/Admin/RemoveSubject/",
        type: "POST",
        data: {
            id: subject.id
        },
        success: function() {
            loadData();
        }
    });
}


function InitVmFromData(data) {
    viewModel.professors.removeAll();
    $.each(data.data, function(i, professorSource) {
        var professor = new Professor();

        professor.id = professorSource.professorId;
        professor.name = professorSource.name;
        professor.surname = professorSource.surname;

        $.each(professorSource.subjectIds, function(i, subjectId) {

            var subjectSource = data.map[subjectId];

            var subject = new Subject();

            subject.id = subjectSource.subjectId;
            subject.name = subjectSource.name;

            professor.subjects.push(subject);
        });

        viewModel.professors.push(professor);
    });
    viewModel.addSubjectVm.professors = viewModel.professors;


}

function loadData() {

    $.ajax({
        url: "/Account/GetAllProfessors/",
        type: "POST",
        success: function(data) {
            InitVmFromData(data);
            if (!isInit) {
                ko.applyBindings(viewModel);
                isInit = true;
            }
        }
    });
}


$(document).ready(function() {
    loadData();


});