function TableRowData() {
    this.name = null;
    this.subject = null;
    this.studentsCount = null;
}



function IniTable(data) {

    var parsed = new Array;
    $.each(data.data, function(index, professor) {
        $.each(professor.subjectIds, function(index, subjectid) {
            var rowData = new TableRowData();
            rowData.name = professor.surname + professor.name;
            rowData.subject = data.map[subjectid].name;
            rowData.studentsCount = data.map[subjectid].studentIds.length;
            parsed.push(rowData);
        });
    });

    $('#dataTable').DataTable(
        {
            data: parsed,
            columns: [
                {
                    mData: 'name',
                    "fnRender":
                        function(oObj) {
                            return oObj;
                        }
                },
                {
                    mData: 'subject',
                    "fnRender": function(oObj) {

                        return oObj;
                    }
                },
                { mData: 'studentsCount', "fnRender": function(oObj) { return oObj } }
            ]
        }
    );
    return;
}


$(document).ready(function () {


    $.ajax({
        url: '/Reports/GetAllProfessors/',
        type: 'GET',

        success: function (data) {
            IniTable(data);
        }
    });


});