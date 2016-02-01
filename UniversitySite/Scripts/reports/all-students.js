function TableRowData() {
    this.name = null;
    this.mark = null;
}



function IniTable(data) {

    var parsed = new Array;
    $.each(data.data, function (index, student) {
        $.each(professor.subjectIds, function (index, subjectid) {
            var rowData = new TableRowData();
            rowData.name = student.surname + student.name;
            rowData.mark = student.comonAverageMark;
            parsed.push(rowData);
        });
    });

    $('#dataTable').DataTable({
        data: parsed,
        columns: [
                        { mData: 'name', "fnRender": function (oObj) { return oObj} },
                        { mData: 'mark', "fnRender": function (oObj) { return oObj } }
                       
        ]
    });
    
    return;
}



$(document).ready(function () {

    $.ajax({
        url: '/Reports/GetAllStudents/',
        type: 'GET',

        success: function (data) {
            IniTable(data);
        }
    });
});