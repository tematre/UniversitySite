function StudentRecord() {
    var self = this;
    self.id = ko.observable();
    self.name = ko.observable();
    self.surname = ko.observable();
    self.markAverage = ko.observable(),
        self.marks = ko.observableArray();
    self.addMark = function(mark) {
        self.marks.push(mark);

        var sum = 0;
        var count = 0;

        $.each(self.marks(), function(index, value) {
            sum += value;
            count++;
        });
        self.markAverage = sum / count;

    };
}

function SubjectRecord() {

    var self = this;

    self.id = ko.observable();
    self.name = ko.observable();
    self.students = ko.observableArray();

    self.addStudent = function(student) {
        self.students.push(student);
    };
}


function SubjectList(subjects) {
    var self = this;
    self.subjects = ko.observableArray(subjects);

    self.addSubject = function(subject) {
        self.subjects.push(subject);
    };
}

function GetValue(obj) {
    return obj.id;
}

function removeStudent(data, parent) {

    $.ajax({
        type: "POST",
        url: "/Professor/RemoveStudentFromSubject/",
        data: {
            StudentId: data.id,
            SubjectId: parent.id
        },
        success: function(professorData) {
            loadData();
        }
    });
}

function addStudent(data, parent) {
    $.ajax({
        type: "POST",
        url: "/Professor/AddStudentToSubject/",
        data: {
            StudentId: $("#studentSelector").val(),
            SubjectId: $("#subjectSelector").val()
        },
        success: function(professorData) {

            loadData();
        }
    });

}

var pageVm = {
    professorSubjects: ko.observableArray(),
    allStudents: null,
    selectedStudentId: ko.observable,
    selectedSubjectId: ko.observable,
    removeStudentClickHandler: removeStudent,
    addStudentToSubject: addStudent,

    markVm:
    {
        mark: null,
        professorSubjects: null,
        currentSubjectStudents: ko.observableArray(),
        selectedSubjectId: null,
        selectedStudentId: null,
        selectedSubject: null,
        markValue: ko.observable(),
        onSubjectSelectedHandler: function(e) {
            pageVm.markVm.selectedSubject = ko.utils.arrayFirst(pageVm.markVm.professorSubjects(), function(item) {
                return pageVm.markVm.selectedSubjectId == item.id;
            });
            pageVm.markVm.currentSubjectStudents.removeAll();

            if (pageVm.markVm.selectedSubject != null) {
                $.each(pageVm.markVm.selectedSubject.students(), function(index, object) {
                    pageVm.markVm.currentSubjectStudents.push(object);
                });
            }

        },
        addMarkHandler: function() {
            $.ajax({
                type: "POST",
                url: "/Professor/AddMark/",
                data: {
                    StudentId: pageVm.markVm.selectedStudentId,
                    SubjectId: pageVm.markVm.selectedSubjectId,
                    Value: pageVm.markVm.markValue()
                },
                success: function(professorData) {

                    loadData();
                }
            });
        }
    }

};

var isInit = false;


function handleJs(data) {


    pageVm.professorVm = ko.observable(data.data);
    pageVm.professorSubjects.removeAll();

    $.each(data.data.subjectIds, function(i, subjectId) {

        var subject = data.map[subjectId];

        var subjectRecord = new SubjectRecord();
        subjectRecord.name = subject.name;
        subjectRecord.id = subjectId;
        $.each(subject.studentIds, function(j, studentId) {

            var student = data.map[studentId];
            var studentRecord = new StudentRecord();

            studentRecord.id = studentId;
            studentRecord.name = student.name;
            studentRecord.surname = student.surname;

            $.each(student.markIds, function(k, markId) {
                var mark = data.map[markId];
                studentRecord.addMark(mark.value);
            });

            subjectRecord.addStudent(studentRecord);
        });

        pageVm.professorSubjects.push(subjectRecord);
    });

    pageVm.markVm.professorSubjects = pageVm.professorSubjects;
    if (!isInit) {
        ko.applyBindings(pageVm);

        isInit = true;
    }
}

function loadData() {
    $.ajax({
        type: "GET",
        url: "/Account/GetAllStudents/",
        success:
            function(jsonData) {
                pageVm.allStudents = ko.observableArray(jsonData.data);
                $.ajax({
                    type: "POST",
                    url: "/Account/GetProfessorViewModel/",
                    success: function(professorData) {
                        handleJs(professorData);
                    }
                });
            }
    });
}

$(document).ready(function() {
    loadData();
});