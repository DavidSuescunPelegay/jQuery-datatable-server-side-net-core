var table;

$(document).ready(function () {
    $.fn.dataTable.moment("DD/MM/YYYY HH:mm:ss");
    $.fn.dataTable.moment("DD/MM/YYYY");

    table = $("#test-registers").DataTable({
        // Design Assets
        stateSave: true,
        autoWidth: true,
        // ServerSide Setups
        processing: true,
        serverSide: true,
        // Paging Setups
        paging: true,
        // Custom Export Buttons
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'Excel',
                action: function () {
                    exportToExcel();
                }
            },
            {
                text: 'CSV',
                action: function () {
                    exportToCsv();
                }
            },
            {
                text: 'HTML',
                action: function () {
                    exportToHtml();
                }
            },
            {
                text: 'JSON',
                action: function () {
                    exportToJson();
                }
            },
            {
                text: 'XML',
                action: function () {
                    exportToXml();
                }
            }
        ],
        // Searching Setups
        searching: { regex: true },
        // Ajax Filter
        ajax: {
            url: "/TestRegisters/LoadTable",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        // Columns Setups
        columns: [
            { data: "id" },
            { data: "name" },
            { data: "firstSurname" },
            { data: "secondSurname" },
            { data: "street" },
            { data: "phone" },
            { data: "zipCode" },
            { data: "country" },
            { data: "notes" },
            {
                data: "creationDate",
                render: function (data, type, row) {
                    // If display or filter data is requested, format the date
                    if (type === "display" || type === "filter") {
                        return moment(data).format("ddd DD/MM/YYYY HH:mm:ss");
                    }
                    // Otherwise the data type requested (`type`) is type detection or
                    // sorting data, for which we want to use the raw date value, so just return
                    // that, unaltered
                    return data;
                }
            }
        ],
        // Column Definitions
        columnDefs: [
            { targets: "no-sort", orderable: false },
            { targets: "no-search", searchable: false },
            {
                targets: "trim",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        data = strtrunc(data, 10);
                    }

                    return data;
                }
            },
            { targets: "date-type", type: "date-eu" },
            {
                targets: 10,
                data: null,
                defaultContent: "<a class='btn btn-link' role='button' href='#' onclick='edit(this)'>Edit</a>",
                orderable: false
            },
        ]
    });
});

function strtrunc(str, num) {
    if (str.length > num) {
        return str.slice(0, num) + "...";
    }
    else {
        return str;
    }
}

function edit(rowContext) {
    if (table) {
        var data = table.row($(rowContext).parents("tr")).data();
        alert("Example showing row edit with id: " + data["id"] + ", name: " + data["name"]);
    }
}

function renderDownloadForm(format) {
    $('#export-to-file-form').attr('action', '/TestRegisters/ExportTable?format=' + format);

    // Get jQuery DataTables AJAX params
    var datatableParams = $('#test-registers').DataTable().ajax.params();

    // If the input exists, replace value, if not create the input and append to form
    if ($("#export-to-file-form input[name=dtParametersJson]").val()) {
        $('#export-to-file-form input[name=dtParametersJson]').val(datatableParams);
    } else {
        var searchModelInput = $("<input>")
            .attr("type", "hidden")
            .attr("name", "dtParametersJson")
            .val(datatableParams);

        $('#export-to-file-form').append(searchModelInput);
    }
}

function exportToExcel() {
    renderDownloadForm("excel");

    $("#export-to-file-form").submit();
}

function exportToCsv() {
    renderDownloadForm("csv");

    $("#export-to-file-form").submit();
}

function exportToHtml() {
    renderDownloadForm("html");

    $("#export-to-file-form").submit();
}

function exportToJson() {
    renderDownloadForm("json");

    $("#export-to-file-form").submit();
}

function exportToXml() {
    renderDownloadForm("xml");

    $("#export-to-file-form").submit();
}