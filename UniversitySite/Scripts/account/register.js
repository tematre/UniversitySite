var studentRegisterViewModel;

function Professor(subjects) {
    var self = this;
    self.id = ko.observable();
    self.name = ko.observable();

    self.subjects = ko.observableArray(subjects);

}


// use as register student views view model
function Subject() {
    var self = this;
    self.id = ko.observable();
    self.name = ko.observable();
    self.professor = ko.observable();
}

// use as student list view's view model
function SubjectList(subjects) {

    var self = this;
    self.subjects = ko.observableArray(subjects);


    self.addSubject = function() {


        self.subjects.push(new Subject());


    };


    self.removeSubject = function(subject) {
        self.subjects.remove(subject);
    };


}


// create index view view model which contain two models for partial views


registerViewModel = {
    professors: ko.observableArray([]),
    selectedProfessorSubjects: ko.observableArray([]),
    selectedProfessor: ko.observable(),
    selectedSubject: ko.observable(),

    studentRegistSubjectsListViewModel: new SubjectList(),
    professorRegistrSubjectsListViewModel: new SubjectList(),


    professorSelectionChanged: function(event) {

        var val = $("#professorSelect").val();
        registerViewModel.selectedProfessor = ko.utils.arrayFirst(registerViewModel.professors(), function(item) {
            return val == item.id;
        });

        registerViewModel.selectedProfessorSubjects.removeAll();

        jQuery.each(registerViewModel.selectedProfessor.subjects, function(i, val) {
            registerViewModel.selectedProfessorSubjects.push(val);
        });


    },
    subjectSelectionChanged: function(event) {
        var val = $("#subjectSelect").val();
        registerViewModel.selectedSubject = ko.utils.arrayFirst(registerViewModel.selectedProfessorSubjects(), function(item) {
            return val == item.id;
        });
    },
    addSubjectForStudent: function(event) {

        var subject = new Subject();
        subject.id = registerViewModel.selectedSubject.id;
        subject.name = registerViewModel.selectedSubject.name;
        subject.professor = registerViewModel.selectedProfessor;

        registerViewModel.studentRegistSubjectsListViewModel.addSubject(subject);
    }
};
// on document ready
$(document).ready(function() {


    function getPeople(people) {

        jQuery.each(people, function(i, val) {
            registerViewModel.professors.push(val);
        });


    }

    $.ajax({
        type: "GET",
        url: "/Account/GetAllProfessors",
        success: function(jsonData) {
            getPeople(jsonData);
        }
    });

    // bind view model to referring view


    ko.applyBindings(registerViewModel);

});